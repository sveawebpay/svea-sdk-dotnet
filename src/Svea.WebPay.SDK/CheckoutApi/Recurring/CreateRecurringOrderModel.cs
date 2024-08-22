using System;
using System.Collections.Generic;
using System.Text;

namespace Svea.WebPay.SDK.CheckoutApi.Recurring
{
    public sealed class CreateRecurringOrderModel
    {
        public CreateRecurringOrderModel(string clientOrderNumber, MerchantSettings merchantSettings, Cart cart)
        {
            ClientOrderNumber = clientOrderNumber;
            MerchantSettings = merchantSettings;
            Cart = cart;
        }
        public CreateRecurringOrderModel()
        {
        }

        /// <summary>
        /// A string that identifies the order in the merchant’s systems. The ClientOrderNumber is unique per order.
        /// Attempting to create a new order with a previously used ClientOrderNumber will result in the create method returning the already existing order instead.
        /// </summary>
        /// <remarks>Max length: 32.</remarks>
        public string ClientOrderNumber { get; set; }

        /// <summary>
        /// At certain points in an order’s lifetime, we will attempt to call endpoints on your side. You provide the URI:s for these endpoints in the MerchantSettings object
        /// </summary>
        public MerchantSettings MerchantSettings { get; set; }
        /// <summary>
        /// The total cost of the order rows in the cart needs to be higher than 0 (i.e. cart can not be empty).
        /// </summary>
        public Cart Cart { get; set; }


    }
}
