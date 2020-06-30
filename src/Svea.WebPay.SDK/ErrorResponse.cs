using System.Collections.Generic;

namespace Svea.WebPay.SDK
{
    public class ErrorResponse
    {
        public ErrorResponse(long code, string message, IList<Error> errors)
        {
            Code = code;
            Message = message;
            Errors = errors;
        }

        public long Code { get; }
        public string Message { get; }
        public IList<Error> Errors { get; }

        public class Error
        {
            public Error(long code, string field, string errorMessage)
            {
                Code = code;
                Field = field;
                ErrorMessage = errorMessage;
            }

            public long Code { get; }
            public string Field { get; }
            public string ErrorMessage { get; }
        }
    }
}
