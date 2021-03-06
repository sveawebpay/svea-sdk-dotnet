﻿using Microsoft.Extensions.Logging;

using Svea.WebPay.SDK.Exceptions;
using Svea.WebPay.SDK.Json;
using Svea.WebPay.SDK.PaymentAdminApi.Request;
using Svea.WebPay.SDK.PaymentAdminApi.Response;

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Task = System.Threading.Tasks.Task;

namespace Svea.WebPay.SDK
{
    using Svea.WebPay.SDK.PaymentAdminApi;

    public class SveaHttpClient : ISveaHttpClient
    {
        private readonly HttpClient client;
        private readonly Credentials credentials;
        private readonly ILogger logger;

        internal SveaHttpClient(HttpClient client, Credentials credentials, ILogger logger)
        {
            this.client = client;
            this.credentials = credentials;
            this.logger = logger;
        }

        /// <summary>
        ///     Send a HttpGet and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpGet<TResponse>(Uri url) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, url);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage);
        }

        /// <summary>
        ///     Send a HttpPatch and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpPut
            <TResponse>(Uri url, object payload) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Put, url, payload);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage);
        }

        /// <summary>
        ///     Send a HttpPatch and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpPatch
            <TResponse>(Uri url, object payload) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(new HttpMethod("PATCH"), url, payload);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage);
        }

        /// <summary>
        ///     Send a HttpPost and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpPost
            <TResponse>(Uri url, object payload) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, url, payload);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage);
        }

        internal async Task<TResponse> HttpPost<TResponse, TResourceResponse>(Uri url, object payload, PollingTimeout pollingTimeout = null)
            where TResponse : ResourceResponseObject<TResourceResponse>, new()
            where TResourceResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, url, payload);
            
            var resourceResponse = await ExecuteResourceRequest<TResponse, TResourceResponse>(httpRequestMessage, pollingTimeout);

            if (resourceResponse?.ResourceUri != null)
            {
                resourceResponse.Resource = await HttpGet<TResourceResponse>(resourceResponse.ResourceUri);
            }

            return resourceResponse;
        }

        private async Task<TResponse> ExecuteResourceRequest<TResponse, TResourceResponse>(HttpRequestMessage httpRequestMessage, PollingTimeout timeout = null)
            where TResponse : ResourceResponseObject<TResourceResponse>, new()
        {
            var polling = true;
            if (timeout == null)
            {
                timeout = new PollingTimeout(10);
                polling = false; 
            }

            using (var cancellationToken = new CancellationTokenSource(timeout.Timeout))
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        logger.LogError("ExecuteResourceRequest timeout");
                        return default;
                    }

                    var response = await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage);

                    response.TaskUri = response.ResourceUri;

                    try
                    {
                        PaymentAdminApi.Models.Task taskResponse;
                        do
                        {
                            taskResponse = await HttpGet<PaymentAdminApi.Models.Task>(response.ResourceUri);
                        } while (taskResponse.Status == "InProgress" && taskResponse.ResourceUri == null && polling);

                        response.ResourceUri = taskResponse.ResourceUri;
                    }
                    catch (HttpRequestException e)
                    {
                        var ex = new HttpRequestException($"Resource object was not returned: {e.Message}");
                        logger.LogError(ex, ex.Message);

                        throw ex;
                    }

                    return response;
                }
                catch (TaskCanceledException e)
                {
                    logger.LogError("ExecuteResourceRequest timeout", e);
                    throw;
                }
            }
        }


        /// <summary>
        ///     Send the HttpRequest and Process HttpResponse. Sets ResourceUri header if TResponse is IResourceResponse
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpRequest"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception> 
        /// <exception cref="HttpResponseException"></exception>
        /// <returns></returns>
        internal async Task<TResponse> SendHttpRequestAndProcessHttpResponse<TResponse>(HttpRequestMessage httpRequest) where TResponse : new()
        {
            await SetRequestHeaders(httpRequest);

            var requestBody = string.Empty;
            if (httpRequest.Content != null)
            {
                requestBody = await httpRequest.Content.ReadAsStringAsync();
            }


            var httpResponse = await client.SendAsync(httpRequest);

            string BuildErrorMessage(string httpResponseBody)
            {
                return
                    $"{httpRequest.Method}: {httpRequest.RequestUri} failed with error code {httpResponse.StatusCode} using bearer token {httpRequest.Headers.Authorization?.Parameter}. Request body: {requestBody}. Response body: {httpResponseBody}";
            }

            try
            {
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                if (!httpResponse.IsSuccessStatusCode && httpResponse.StatusCode != HttpStatusCode.SeeOther)
                {
                    throw new HttpResponseException(
                        httpResponse,
                        !string.IsNullOrWhiteSpace(httpResponseBody)
                            ? JsonSerializer.Deserialize<ErrorResponse>(httpResponseBody)
                            : null,
                        BuildErrorMessage(httpResponseBody));
                }

                var responsObj = new TResponse();
                if (httpResponse.StatusCode != HttpStatusCode.NoContent && !string.IsNullOrWhiteSpace(httpResponseBody))
                {
                    responsObj = JsonSerializer.Deserialize<TResponse>(httpResponseBody, JsonSerialization.Settings);
                    if (responsObj == null)
                    {
                        responsObj = new TResponse();
                    }
                }

                SetLocation(responsObj, httpResponse);

                return responsObj;
            }
            catch (HttpResponseException ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                throw new HttpResponseException(
                    httpResponse,
                    message: BuildErrorMessage(httpResponseBody),
                    innerException: ex);
            }
        }

        private void SetLocation<TResponse>(TResponse responseObject, HttpResponseMessage httpResponseMessage) where TResponse : new()
        {
            if (responseObject is IResourceResponse locationResponse)
            {
                locationResponse.ResourceUri = httpResponseMessage.Headers.Location;
            }
        }

        private async Task SetRequestHeaders(HttpRequestMessage httpRequest)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            var token = CreateAuthenticationToken(httpRequest.Content != null ? await httpRequest.Content.ReadAsStringAsync() : string.Empty, timestamp);
            httpRequest.Headers.Add("Authorization", "Svea" + " " + token);
            httpRequest.Headers.Add("Timestamp", timestamp);
        }

        private string CreateAuthenticationToken(string requestBody, string timestamp)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(requestBody, credentials.Secret, timestamp)));
                var hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials.MerchantId + ":" + hashString));
            }
        }

        private HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, Uri url, object payload = null)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, url);

            if (payload != null)
            {
                var content = JsonSerializer.Serialize(payload, JsonSerialization.Settings);
                httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            httpRequestMessage.Headers.Add("Accept", "application/json");

            return httpRequestMessage;
        }
    }
}