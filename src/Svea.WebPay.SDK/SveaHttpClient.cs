using Microsoft.Extensions.Logging;

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
    using System.Diagnostics;

    public class SveaHttpClient : ISveaHttpClient
    {
        private readonly HttpClient _client;
        private readonly Credentials _credentials;
        private readonly ILogger _logger;

        internal SveaHttpClient(HttpClient client, Credentials credentials, ILogger logger)
        {
            this._client = client;
            this._credentials = credentials;
            this._logger = logger;
        }

        /// <summary>
        ///     Send a HttpGet and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="configureAwait"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpGet<TResponse>(Uri url, bool configureAwait) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Get, url);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage, configureAwait).ConfigureAwait(configureAwait);
        }

        /// <summary>
        ///     Send a HttpPatch and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <param name="configureAwait"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpPut
            <TResponse>(Uri url, object payload, bool configureAwait) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Put, url, payload);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage, configureAwait).ConfigureAwait(configureAwait);
        }

        /// <summary>
        ///     Send a HttpPatch and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <param name="configureAwait"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpPatch
            <TResponse>(Uri url, object payload, bool configureAwait) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(new HttpMethod("PATCH"), url, payload);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage, configureAwait).ConfigureAwait(configureAwait);
        }

        /// <summary>
        ///     Send a HttpPost and Process HttpResponse for a url
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <param name="configureAwait"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="HttpResponseException"></exception>
        internal async Task<TResponse> HttpPost
            <TResponse>(Uri url, object payload, bool configureAwait) where TResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, url, payload);
            return await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage, configureAwait).ConfigureAwait(configureAwait);
        }

        internal async Task<TResponse> HttpPost<TResponse, TResourceResponse>(Uri url, object payload, PollingTimeout pollingTimeout = null, bool configureAwait = false)
            where TResponse : ResourceResponseObject<TResourceResponse>, new()
            where TResourceResponse : new()
        {
            var httpRequestMessage = CreateHttpRequestMessage(HttpMethod.Post, url, payload);
            
            var resourceResponse = await ExecuteResourceRequest<TResponse, TResourceResponse>(httpRequestMessage, pollingTimeout, configureAwait).ConfigureAwait(configureAwait);

            if (resourceResponse?.ResourceUri != null)
            {
                resourceResponse.Resource = await HttpGet<TResourceResponse>(resourceResponse.ResourceUri, configureAwait).ConfigureAwait(configureAwait);
            }

            return resourceResponse;
        }

        private async Task<TResponse> ExecuteResourceRequest<TResponse, TResourceResponse>(HttpRequestMessage httpRequestMessage, PollingTimeout timeout = null, bool configureAwait = false)
            where TResponse : ResourceResponseObject<TResourceResponse>, new()
        {
            var polling = true;
            if (timeout == null)
            {
                timeout = new PollingTimeout();
                polling = false; 
            }

            using (var cancellationToken = new CancellationTokenSource(timeout.Timeout))
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogError("ExecuteResourceRequest timeout");
                        return default;
                    }

                    var response = await SendHttpRequestAndProcessHttpResponse<TResponse>(httpRequestMessage, configureAwait).ConfigureAwait(configureAwait);

                    response.TaskUri = response.ResourceUri;

                    try
                    {
                        PaymentAdminApi.Models.Task taskResponse = null;

                        do
                        {
                            if (taskResponse is object)
                            {
                                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(configureAwait);
                            }

                            taskResponse = await HttpGet<PaymentAdminApi.Models.Task>(response.ResourceUri, configureAwait).ConfigureAwait(configureAwait);

                        } while (taskResponse.Status == "InProgress" && taskResponse.ResourceUri == null && polling);

                        response.ResourceUri = taskResponse.ResourceUri;
                    }
                    catch (HttpRequestException e)
                    {
                        var ex = new HttpRequestException($"Resource object was not returned: {e.Message}");
                        _logger.LogError(ex, ex.Message);

                        throw ex;
                    }

                    return response;
                }
                catch (TaskCanceledException e)
                {
                    _logger.LogError("ExecuteResourceRequest timeout", e);
                    throw;
                }
            }
        }


        /// <summary>
        ///     Send the HttpRequest and Process HttpResponse. Sets ResourceUri header if TResponse is IResourceResponse
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="httpRequest"></param>
        /// <param name="configureAwait"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception> 
        /// <exception cref="HttpResponseException"></exception>
        /// <returns></returns>
        internal async Task<TResponse> SendHttpRequestAndProcessHttpResponse<TResponse>(HttpRequestMessage httpRequest, bool configureAwait) where TResponse : new()
        {
            await SetRequestHeaders(httpRequest, configureAwait);

            var requestBody = string.Empty;
            if (httpRequest.Content != null)
            {
                requestBody = await httpRequest.Content.ReadAsStringAsync().ConfigureAwait(configureAwait);
            }


            var httpResponse = await _client.SendAsync(httpRequest).ConfigureAwait(configureAwait);

            string BuildErrorMessage(string httpResponseBody)
            {
                return
                    $"{httpRequest.Method}: {httpRequest.RequestUri} failed with error code {httpResponse.StatusCode} using bearer token {httpRequest.Headers.Authorization?.Parameter}. Request body: {requestBody}. Response body: {httpResponseBody}";
            }

            try
            {
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(configureAwait);
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
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var httpResponseBody = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(configureAwait);
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

        private async Task SetRequestHeaders(HttpRequestMessage httpRequest, bool configureAwait)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            var token = CreateAuthenticationToken(httpRequest.Content != null ? await httpRequest.Content.ReadAsStringAsync().ConfigureAwait(configureAwait) : string.Empty, timestamp);
            httpRequest.Headers.Add("Authorization", "Svea" + " " + token);
            httpRequest.Headers.Add("Timestamp", timestamp);
        }

        private string CreateAuthenticationToken(string requestBody, string timestamp)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(requestBody, _credentials.Secret, timestamp)));
                var hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(_credentials.MerchantId + ":" + hashString));
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