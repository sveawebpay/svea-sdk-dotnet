namespace Svea.WebPay.SDK.CheckoutApi
{
    public class Gui
    {
        public Gui(string layout, string snippet)
        {
            Layout = layout;
            Snippet = snippet;
        }

        /// <summary>
        /// Defines the orientation of the device, either “desktop” or “portrait”.
        /// </summary>
        public string Layout { get; }

        /// <summary>
        /// HTML-snippet including javascript to populate the iFrame.
        /// </summary>
        public string Snippet { get; }
    }
}