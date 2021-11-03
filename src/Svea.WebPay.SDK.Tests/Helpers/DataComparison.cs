using Svea.WebPay.SDK.CheckoutApi;
using Svea.WebPay.SDK.PaymentAdminApi;
using System.Linq;
using Xunit;
using PaymentOrderRow = Svea.WebPay.SDK.PaymentAdminApi.Models.OrderRow;
using CheckoutOrderRow = Svea.WebPay.SDK.CheckoutApi.OrderRowResponse;

namespace Svea.WebPay.SDK.Tests.Helpers
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    public static class DataComparison
    {
        public static bool OrdersAreEqual(Order expectedOrder, Order actualOrder)
        {
            Assert.Equal(expectedOrder.Id, actualOrder.Id);
            Assert.Equal(expectedOrder.Currency, actualOrder.Currency);
            Assert.Equal(expectedOrder.CreationDate.ToString(), actualOrder.CreationDate.ToString());
            Assert.Equal(expectedOrder.CancelledAmount.ToString(), actualOrder.CancelledAmount.ToString());
            Assert.Equal(expectedOrder.CustomerReference, actualOrder.CustomerReference);
            Assert.Equal(expectedOrder.EmailAddress.ToString(), actualOrder.EmailAddress.ToString());
            Assert.Equal(expectedOrder.IsCompany, actualOrder.IsCompany);
            Assert.Equal(expectedOrder.MerchantOrderId, actualOrder.MerchantOrderId);
            Assert.Equal(expectedOrder.NationalId, actualOrder.NationalId);
            Assert.Equal(expectedOrder.OrderAmount.ToString(), actualOrder.OrderAmount.ToString());
            Assert.Equal(expectedOrder.OrderStatus, actualOrder.OrderStatus);
            Assert.Equal(expectedOrder.PaymentType, actualOrder.PaymentType);
            Assert.Equal(expectedOrder.PhoneNumber, actualOrder.PhoneNumber);
            Assert.Equal(expectedOrder.SveaWillBuy, actualOrder.SveaWillBuy);

            Assert.Equal(expectedOrder.ShippingAddress.FirstName, actualOrder.ShippingAddress.FirstName);
            Assert.Equal(expectedOrder.ShippingAddress.LastName, actualOrder.ShippingAddress.LastName);
            Assert.Equal(expectedOrder.ShippingAddress.StreetAddress, actualOrder.ShippingAddress.StreetAddress);
            Assert.Equal(expectedOrder.ShippingAddress.City, actualOrder.ShippingAddress.City);
            Assert.Equal(expectedOrder.ShippingAddress.CoAddress, actualOrder.ShippingAddress.CoAddress);
            Assert.Equal(expectedOrder.ShippingAddress.PostalCode, actualOrder.ShippingAddress.PostalCode);
            Assert.Equal(expectedOrder.ShippingAddress.CountryCode, actualOrder.ShippingAddress.CountryCode);

            Assert.Equal(expectedOrder.BillingAddress.FirstName, actualOrder.BillingAddress.FirstName);
            Assert.Equal(expectedOrder.BillingAddress.LastName, actualOrder.BillingAddress.LastName);
            Assert.Equal(expectedOrder.BillingAddress.StreetAddress, actualOrder.BillingAddress.StreetAddress);
            Assert.Equal(expectedOrder.BillingAddress.City, actualOrder.BillingAddress.City);
            Assert.Equal(expectedOrder.BillingAddress.CoAddress, actualOrder.BillingAddress.CoAddress);
            Assert.Equal(expectedOrder.BillingAddress.PostalCode, actualOrder.BillingAddress.PostalCode);
            Assert.Equal(expectedOrder.BillingAddress.CountryCode, actualOrder.BillingAddress.CountryCode);

            Assert.Equal(expectedOrder.AvailableActions.Count, actualOrder.AvailableActions.Count);
            Assert.All(expectedOrder.AvailableActions, (action) => actualOrder.AvailableActions.Contains(action));

            Assert.Equal(expectedOrder.OrderRows.Count, actualOrder.OrderRows.Count);
            foreach (var expected in expectedOrder.OrderRows)
            {
                var actual = actualOrder.OrderRows.FirstOrDefault(orderRow => orderRow.OrderRowId == expected.OrderRowId);
                PaymentOrderRowsAreEqual(actual, expected);
            }

            Assert.Equal(expectedOrder.Deliveries.Count, actualOrder.Deliveries.Count);
            foreach(var expected in expectedOrder.Deliveries)
            {
                var actual = actualOrder.Deliveries.FirstOrDefault(delivery => delivery.Id == expected.Id);
                DeliveriesAreEqual(actual, expected);
            }

            return true;
        }

        public static bool PaymentOrderRowsAreEqual(PaymentOrderRow expectedRow, PaymentOrderRow actualRow)
        {
            Assert.Equal(expectedRow.OrderRowId, actualRow.OrderRowId);
            Assert.Equal(expectedRow.ArticleNumber, actualRow.ArticleNumber);
            Assert.Equal(expectedRow.Name, actualRow.Name);
            Assert.Equal(expectedRow.Quantity.ToString(), actualRow.Quantity.ToString());
            Assert.Equal(expectedRow.UnitPrice.ToString(), actualRow.UnitPrice.ToString());
            Assert.Equal(expectedRow.DiscountAmount.ToString(), actualRow.DiscountAmount.ToString());
            Assert.Equal(expectedRow.VatPercent.ToString(), actualRow.VatPercent.ToString());
            Assert.Equal(expectedRow.Unit, actualRow.Unit);
            Assert.Equal(expectedRow.IsCancelled, actualRow.IsCancelled);

            Assert.Equal(expectedRow.AvailableActions.Count, actualRow.AvailableActions.Count);
            Assert.All(expectedRow.AvailableActions, (action) => actualRow.AvailableActions.Contains(action));

            return true;
        }

        public static bool DeliveriesAreEqual(Delivery expectedDelivery, Delivery actualDelivery)
        {
            Assert.Equal(expectedDelivery.CreationDate.ToString(), actualDelivery.CreationDate.ToString());
            Assert.Equal(expectedDelivery.CreditedAmount, actualDelivery.CreditedAmount);
            Assert.Equal(expectedDelivery.DeliveryAmount, actualDelivery.DeliveryAmount);
            Assert.Equal(expectedDelivery.DueDate, actualDelivery.DueDate);
            Assert.Equal(expectedDelivery.Id, actualDelivery.Id);
            Assert.Equal(expectedDelivery.InvoiceId, actualDelivery.InvoiceId);
            Assert.Equal(expectedDelivery.Status, actualDelivery.Status);

            Assert.Equal(expectedDelivery.OrderRows.Count, actualDelivery.OrderRows.Count);
            foreach(var expected in expectedDelivery.OrderRows)
            {
                var actual = actualDelivery.OrderRows.FirstOrDefault(orderRow => orderRow.OrderRowId == expected.OrderRowId);
                PaymentOrderRowsAreEqual(actual, expected);
            }

            Assert.Equal(expectedDelivery.AvailableActions.Count, actualDelivery.AvailableActions.Count);
            Assert.All(expectedDelivery.AvailableActions, (action) => actualDelivery.AvailableActions.Contains(action));

            Assert.Equal(expectedDelivery.Credits.Count, actualDelivery.Credits.Count);
            foreach (var expected in expectedDelivery.Credits)
            {
                var actual = actualDelivery.Credits.FirstOrDefault(credit => credit.Amount == expected.Amount);
                CreditsAreEqual(actual, expected);
            }

            return true;
        }

        public static bool CreditsAreEqual(Credit expectedCredit, Credit actualCredit)
        {
            Assert.Equal(expectedCredit.Amount, actualCredit.Amount);
            Assert.Equal(expectedCredit.Actions.Count, actualCredit.Actions.Count);
            Assert.All(expectedCredit.Actions, (action) => actualCredit.Actions.Contains(action));

            return true;
        }

        public static bool CreditResponsesAreEqual(CreditResponse expectedResponse, CreditResponse actualResponse)
        {
            Assert.Equal(expectedResponse.CreditId, actualResponse.CreditId);

            return true;
        }

        public static bool TasksAreEqual(Task expectedTask, Task actualTask)
        {
            Assert.Equal(expectedTask.Id, actualTask.Id);
            Assert.Equal(expectedTask.Status, actualTask.Status);
            Assert.Equal(expectedTask.ResourceUri.OriginalString, actualTask.ResourceUri.OriginalString);

            return true;

        }

        public static bool DataAreEqual(Data expectedData, Data actualData)
        {

            Assert.Equal(expectedData.Locale, actualData.Locale);
            Assert.Equal(expectedData.Currency, actualData.Currency);
            Assert.Equal(expectedData.CountryCode, actualData.CountryCode);
            Assert.Equal(expectedData.ClientOrderNumber, actualData.ClientOrderNumber);
            Assert.Equal(expectedData.OrderId, actualData.OrderId);
            Assert.Equal(expectedData.EmailAddress, actualData.EmailAddress);
            Assert.Equal(expectedData.PhoneNumber, actualData.PhoneNumber);
            Assert.Equal(expectedData.PaymentType, actualData.PaymentType);
            Assert.Equal(expectedData.Status, actualData.Status);
            Assert.Equal(expectedData.CustomerReference, actualData.CustomerReference);
            Assert.Equal(expectedData.SveaWillBuyOrder, actualData.SveaWillBuyOrder);
            Assert.Equal(expectedData.PeppolId, actualData.PeppolId);
            Assert.Equal(expectedData.MerchantData, actualData.MerchantData);

            if(expectedData.MerchantSettings != null || actualData.MerchantSettings != null)
            {
                Assert.Equal(expectedData.MerchantSettings.CheckoutUri, actualData.MerchantSettings.CheckoutUri);
                Assert.Equal(expectedData.MerchantSettings.CheckoutValidationCallBackUri, actualData.MerchantSettings.CheckoutValidationCallBackUri);
                Assert.Equal(expectedData.MerchantSettings.ConfirmationUri, actualData.MerchantSettings.ConfirmationUri);
                Assert.Equal(expectedData.MerchantSettings.PromotedPartPaymentCampaign, actualData.MerchantSettings.PromotedPartPaymentCampaign);
                Assert.Equal(expectedData.MerchantSettings.PushUri, actualData.MerchantSettings.PushUri);
                Assert.Equal(expectedData.MerchantSettings.TermsUri, actualData.MerchantSettings.TermsUri);
                Assert.Equal(expectedData.MerchantSettings.ActivePartPaymentCampaigns.Count, actualData.MerchantSettings.ActivePartPaymentCampaigns.Count);
                Assert.All(expectedData.MerchantSettings.ActivePartPaymentCampaigns, (part) => actualData.MerchantSettings.ActivePartPaymentCampaigns.Contains(part));
            }

            if (expectedData.Cart != null || actualData.Cart != null)
            {
                Assert.Equal(expectedData.Cart.Items.Count, actualData.Cart.Items.Count);
                foreach (var expected in expectedData.Cart.Items)
                {
                    var actual = actualData.Cart.Items.FirstOrDefault(orderRow => orderRow.RowNumber == expected.RowNumber);
                    CheckoutOrderRowsAreEqual(actual, expected);
                }
            }

            if(expectedData.Customer != null || actualData.Customer != null)
            {
                Assert.Equal(expectedData.Customer.Id, actualData.Customer.Id);
                Assert.Equal(expectedData.Customer.CountryCode, actualData.Customer.CountryCode);
                Assert.Equal(expectedData.Customer.NationalId, actualData.Customer.NationalId);
                Assert.Equal(expectedData.Customer.IsCompany, actualData.Customer.IsCompany);
            }

            if(expectedData.ShippingAddress != null || actualData.ShippingAddress != null)
            {
                Assert.Equal(expectedData.ShippingAddress.FirstName, actualData.ShippingAddress.FirstName);
                Assert.Equal(expectedData.ShippingAddress.LastName, actualData.ShippingAddress.LastName);
                Assert.Equal(expectedData.ShippingAddress.StreetAddress, actualData.ShippingAddress.StreetAddress);
                Assert.Equal(expectedData.ShippingAddress.City, actualData.ShippingAddress.City);
                Assert.Equal(expectedData.ShippingAddress.CoAddress, actualData.ShippingAddress.CoAddress);
                Assert.Equal(expectedData.ShippingAddress.PostalCode, actualData.ShippingAddress.PostalCode);
                Assert.Equal(expectedData.ShippingAddress.CountryCode, actualData.ShippingAddress.CountryCode);
            }

            if (expectedData.BillingAddress != null || actualData.BillingAddress != null)
            {
                Assert.Equal(expectedData.BillingAddress.FirstName, actualData.BillingAddress.FirstName);
                Assert.Equal(expectedData.BillingAddress.LastName, actualData.BillingAddress.LastName);
                Assert.Equal(expectedData.BillingAddress.StreetAddress, actualData.BillingAddress.StreetAddress);
                Assert.Equal(expectedData.BillingAddress.City, actualData.BillingAddress.City);
                Assert.Equal(expectedData.BillingAddress.CoAddress, actualData.BillingAddress.CoAddress);
                Assert.Equal(expectedData.BillingAddress.PostalCode, actualData.BillingAddress.PostalCode);
                Assert.Equal(expectedData.BillingAddress.CountryCode, actualData.BillingAddress.CountryCode);
            }

            if (expectedData.Gui != null || actualData.Gui != null)
            {
                Assert.Equal(expectedData.Gui.Layout, actualData.Gui.Layout);
                Assert.Equal(expectedData.Gui.Snippet, actualData.Gui.Snippet);
            }

            if (expectedData.Payment != null || actualData.Payment != null)
            {
                Assert.Equal(expectedData.Payment.CampaignCode, actualData.Payment.CampaignCode);
                Assert.Equal(expectedData.Payment.PaymentMethodType, actualData.Payment.PaymentMethodType);
            }

            if (expectedData.IdentityFlags != null || actualData.IdentityFlags != null)
            {
                Assert.Equal(expectedData.IdentityFlags.HideAnonymous, actualData.IdentityFlags.HideAnonymous);
                Assert.Equal(expectedData.IdentityFlags.HideChangeAddress, actualData.IdentityFlags.HideChangeAddress);
                Assert.Equal(expectedData.IdentityFlags.HideNotYou, actualData.IdentityFlags.HideNotYou);
            }

            if (expectedData.PresetValues != null || actualData.PresetValues != null)
            {
                Assert.Equal(expectedData.PresetValues.Count(), actualData.PresetValues.Count());
                foreach (var expected in expectedData.PresetValues)
                {
                    var actual = actualData.PresetValues.FirstOrDefault(presetValue => presetValue.Value == presetValue.Value);
                    PresetValuesAreEqual(actual, expected);
                }
            }

            return true;
        }

        public static bool CheckoutOrderRowsAreEqual(CheckoutOrderRow expectedRow, CheckoutOrderRow actualRow)
        {
            Assert.Equal(expectedRow.RowNumber, actualRow.RowNumber);
            Assert.Equal(expectedRow.TemporaryReference, actualRow.TemporaryReference);
            Assert.Equal(expectedRow.ArticleNumber, actualRow.ArticleNumber);
            Assert.Equal(expectedRow.Name, actualRow.Name);
            Assert.Equal(expectedRow.Quantity.ToString(), actualRow.Quantity.ToString());
            Assert.Equal(expectedRow.UnitPrice.ToString(), actualRow.UnitPrice.ToString());
            Assert.Equal(expectedRow.DiscountAmount.ToString(), actualRow.DiscountAmount.ToString());
            Assert.Equal(expectedRow.VatPercent.ToString(), actualRow.VatPercent.ToString());
            Assert.Equal(expectedRow.Unit, actualRow.Unit);
            Assert.Equal(expectedRow.MerchantData, actualRow.MerchantData);

            return true;
        }

        public static bool PresetValuesAreEqual(Presetvalue expected, Presetvalue actual)
        {
            Assert.Equal(expected.TypeName, actual.TypeName);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.IsReadonly, actual.IsReadonly);

            return true;
        }

    }
}
