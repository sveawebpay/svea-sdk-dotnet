using System;
using System.Net.Http;

namespace Svea.WebPay.SDK.Tests
{
    using Microsoft.Extensions.DependencyInjection;

    using Moq;
    using Moq.Protected;

    using Svea.WebPay.SDK.Tests.Helpers;
    using Svea.WebPay.SDK.Tests.Models;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Credentials = Svea.WebPay.SDK.Credentials;

    public abstract class TestBase : IDisposable
    {
        protected SveaWebPayClient Sut;

        protected SveaConfiguration Configuration;

        private readonly ServiceProvider _serviceProvider;

        protected TestBase()
        {
            var appRoot = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin", StringComparison.Ordinal));

            Configuration = TestHelper.GetApplicationConfiguration(appRoot);

            var services = new ServiceCollection();
            _serviceProvider = services.BuildServiceProvider();

            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            var checkoutApihttpClient = new HttpClient(handler)
            {
                BaseAddress = Configuration.SveaApiUrls.CheckoutApiUri
            };
            var paymentAdminApiHttpClient = new HttpClient(handler)
            {
                BaseAddress = Configuration.SveaApiUrls.PaymentAdminApiUri
            };

            this.Sut = new SveaWebPayClient(checkoutApihttpClient, paymentAdminApiHttpClient, new Credentials(Configuration.Credentials.First(x => x.MarketId == "SE").MerchantId, Configuration.Credentials.First(x => x.MarketId == "SE").Secret));
        }

        #region Mock

        protected Mock<HttpMessageHandler> CreateHandlerMock(string responseMsg, string location = null)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = string.IsNullOrWhiteSpace(responseMsg) ? HttpStatusCode.NoContent : HttpStatusCode.OK,
                    Headers =
                    {
                        Location = location != null ? new Uri(location) : null
                    },
                    Content = new StringContent(responseMsg),
                })
                .Verifiable();

            return handlerMock;
        }

        protected Mock<HttpMessageHandler> CreateHandlerMockWithAction(string responseMsg, string actionResponseMsg = "")
        {
            var actionResponse = new HttpResponseMessage()
            {
                StatusCode = string.IsNullOrWhiteSpace(actionResponseMsg) ? HttpStatusCode.NoContent : HttpStatusCode.OK,
                Content = new StringContent(actionResponseMsg)
            };

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = string.IsNullOrWhiteSpace(responseMsg) ? HttpStatusCode.NoContent : HttpStatusCode.OK,
                    Content = new StringContent(responseMsg),
                })
                .ReturnsAsync(actionResponse);

            return handlerMock;
        }

        protected Mock<HttpMessageHandler> CreateHandlerMockWithAction(string responseMsg, string actionResponseMsg, string taskLocation, string taskResponseMsg = "", string resourceResponseMsg = "")
        {
            var actionResponse = new HttpResponseMessage()
            {
                StatusCode = string.IsNullOrWhiteSpace(actionResponseMsg) ? HttpStatusCode.NoContent : HttpStatusCode.OK,
                Content = new StringContent(actionResponseMsg),
                Headers =
                {
                    Location = new Uri(taskLocation)
                }
            };

            var taskResponse = new HttpResponseMessage()
            {
                StatusCode = string.IsNullOrWhiteSpace(taskResponseMsg) ? HttpStatusCode.NoContent : HttpStatusCode.OK,
                Content = new StringContent(taskResponseMsg),
                Headers =
                {
                    Location = new Uri(taskLocation)
                }
            };

            var resourceResponse = new HttpResponseMessage()
            {
                StatusCode = string.IsNullOrWhiteSpace(resourceResponseMsg) ? HttpStatusCode.NoContent : HttpStatusCode.OK,
                Content = new StringContent(resourceResponseMsg)
            };

            actionResponse.Headers.Location = new Uri(taskLocation);


            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = string.IsNullOrWhiteSpace(responseMsg) ? HttpStatusCode.NoContent : HttpStatusCode.OK,
                    Content = new StringContent(responseMsg),
                })
                .ReturnsAsync(actionResponse)
                .ReturnsAsync(taskResponse)
                .ReturnsAsync(resourceResponse);


            return handlerMock;
        }

        protected SveaWebPayClient SveaClient(Mock<HttpMessageHandler> mock)
        {
            var httpClient = new HttpClient(mock.Object)
            {
                BaseAddress = Configuration.SveaApiUrls.PaymentAdminApiUri
            };

            return new SveaWebPayClient(httpClient, httpClient, new Credentials(Configuration.Credentials.First(x => x.MarketId == "SE").MerchantId, Configuration.Credentials.First(x => x.MarketId == "SE").Secret));
        }

        #endregion

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
