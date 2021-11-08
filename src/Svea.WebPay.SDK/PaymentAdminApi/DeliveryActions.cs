using System;

namespace Svea.WebPay.SDK.PaymentAdminApi
{
    using Svea.WebPay.SDK.PaymentAdminApi.Request;
    using Svea.WebPay.SDK.PaymentAdminApi.Response;

    public class DeliveryActions
    {
        public DeliveryActions(long orderId, DeliveryResponseObject deliveryResponse, SveaHttpClient client)
        {
            if (deliveryResponse.Actions == null)
            {
                return;
            }

            foreach (var action in deliveryResponse.Actions)
            {
                switch (action)
                {
                    case DeliveryActionType.CanCreditOrderRows:
                        CreditOrderRows = async (payload, pollingTimeout) =>
                        {
                            var response = await client
                                .HttpPost<ResourceResponseObject<CreditResponseObject>, CreditResponseObject>(
                                    new Uri($"/api/v1/orders/{orderId}/deliveries/{deliveryResponse.Id}/credits",
                                        UriKind.Relative), payload, pollingTimeout, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);

                            var resource = new ResourceResponse<CreditResponseObject, CreditResponse>(response, () => new CreditResponse(response.Resource));

                            return resource;
                        };

                        CreditOrderRowsWithFee = async (payload, pollingTimeout) =>
                        {
                            var response = await client
                                .HttpPost<ResourceResponseObject<CreditResponseObject>, CreditResponseObject>(
                                    new Uri($"/api/v1/orders/{orderId}/deliveries/{deliveryResponse.Id}/credits/CreditWithFee",
                                        UriKind.Relative), payload, pollingTimeout, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);

                            var resource = new ResourceResponse<CreditResponseObject, CreditResponse>(response, () => new CreditResponse(response.Resource));

                            return resource;
                        };
                        break;
                    case DeliveryActionType.CanCreditNewRow:
                        CreditNewRow = async (payload, pollingTimeout) =>
                        {
                            var response = await client
                                .HttpPost<ResourceResponseObject<CreditResponseObject>, CreditResponseObject>(
                                    new Uri($"/api/v1/orders/{orderId}/deliveries/{deliveryResponse.Id}/credits",
                                        UriKind.Relative), payload, pollingTimeout, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);

                            var resource = new ResourceResponse<CreditResponseObject, CreditResponse>(response,  () => new CreditResponse(response.Resource));
                            
                            return resource;
                        };
                        break;
                    case DeliveryActionType.CanCreditAmount:
                        CreditAmount = async payload =>
                        {
                            var response = await client.HttpPatch<CreditResponseObject>(
                                new Uri($"/api/v1/orders/{orderId}/deliveries/{deliveryResponse.Id}", 
                                    UriKind.Relative), payload, payload.ConfigureAwait).ConfigureAwait(payload.ConfigureAwait);

                            return new CreditResponse(response);
                        };
                        break;
                }
            }
        }


        /// <summary>
        /// Creates a new credit on the specified delivery with specified order rows. Assuming the delivery
        /// has action “CanCreditOrderRows” and the specified order rows also has action “CanCreditRow”
        /// </summary>
        public Func<CreditOrderRowsRequest, PollingTimeout, System.Threading.Tasks.Task<ResourceResponse<CreditResponseObject, CreditResponse>>> CreditOrderRows { get; internal set; }

        /// <summary>
        /// By specifying a new credit row, a new credit row will be created on the delivery, assuming the
        /// delivery has action “CanCreditNewRow”.
        /// </summary>
        public Func<CreditNewOrderRowRequest, PollingTimeout, System.Threading.Tasks.Task<ResourceResponse<CreditResponseObject, CreditResponse>>> CreditNewRow { get; internal set; }

        /// <summary>
        /// Use this action to create a new credit with fee on a delivery with specified order rows. This works only for Invoice orders.
        /// To use the Credit order rows action, the delivery needs to have the action "CanCreditOrderRows" and the order rows need to have the action "CanCreditRow".
        /// It is optional to add a fee on the credit.´It is also possible to partially credit an order row using the RowCreditingOptions.
        /// </summary>
        public Func<CreditOrderRowWithFeeRequest, PollingTimeout, System.Threading.Tasks.Task<ResourceResponse<CreditResponseObject, CreditResponse>>> CreditOrderRowsWithFee { get; internal set; }

        /// <summary>
        /// By specifying a credited amount larger than the current credited amount. A credit is being made
        /// on the specified delivery.The credited amount cannot be lower than the current credited
        /// amount or larger than the delivered amount.
        /// This method requires “CanCreditAmount” on the delivery.
        /// </summary>
        public Func<CreditAmountRequest, System.Threading.Tasks.Task<CreditResponse>> CreditAmount { get; internal set; }
    }
}
