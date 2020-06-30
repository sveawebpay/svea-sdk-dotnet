namespace Svea.WebPay.SDK.CheckoutApi
{
    public class CheckoutValidationCallbackResponse
    {
        public CheckoutValidationCallbackResponse(bool valid, string message = null, string clientOrderNumber = null)
        {
            Valid = valid;
            Message = message;
            ClientOrderNumber = clientOrderNumber;
        }

        public bool Valid { get; }
        public string Message { get; }
        public string ClientOrderNumber { get; }
    }
}
