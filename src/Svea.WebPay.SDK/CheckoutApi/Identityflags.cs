namespace Svea.WebPay.SDK.CheckoutApi
{
    public class IdentityFlags
    {
        public IdentityFlags(bool hideNotYou, bool hideChangeAddress, bool hideAnonymous)
        {
            HideNotYou = hideNotYou;
            HideChangeAddress = hideChangeAddress;
            HideAnonymous = hideAnonymous;
        }

        public bool HideNotYou { get; }
        public bool HideChangeAddress { get; }
        public bool HideAnonymous { get; }
    }
}