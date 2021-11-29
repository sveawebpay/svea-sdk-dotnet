namespace Svea.WebPay.SDK.Exceptions
{
    using System;
    using System.Net.Http;

    public class HttpResponseException : Exception
    {
        public HttpResponseException(HttpResponseMessage httpResponse,
            ErrorResponse problemResponse = null,
            string message = null,
            Exception innerException = null)
            : base(message, innerException)
        {
            HttpResponse = httpResponse ?? throw new ArgumentNullException(nameof(httpResponse));
            ProblemResponse = problemResponse;
        }

        public HttpResponseException() : base()
        {
        }

        public HttpResponseException(string message) : base(message)
        {
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public HttpResponseMessage HttpResponse { get; }

        public object ProblemResponse { get; }
    }
}