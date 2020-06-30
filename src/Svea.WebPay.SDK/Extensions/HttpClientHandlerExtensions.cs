namespace Svea.WebPay.SDK.Extensions
{
    using System.Net.Http;
    using System.Reflection;

    public static class HttpClientHandlerExtensions
    {
        private static readonly FieldInfo AllowAutoRedirectFieldInfo =
            typeof(HttpClientHandler).GetField("allowAutoRedirect",BindingFlags.Instance | BindingFlags.NonPublic);

        public static void SetAllowAutoRedirect(this HttpClientHandler handler, bool value)
        {
            AllowAutoRedirectFieldInfo.SetValue(handler, value);
        }
    }
}
