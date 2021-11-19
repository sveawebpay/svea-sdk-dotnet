namespace Svea.WebPay.SDK.Tests.Helpers
{
    public static class DataSample
    {
        public static string AdminGetOrder = @"
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753"",
            ""OrderStatus"":""Open"",
            ""SystemStatus"":""Active"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson@mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-23T20:21:17"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":0,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":[],
            ""OrderRows"":[
                {
                    ""OrderRowId"":1,
                    ""ArticleNumber"":""Ref1"",
                    ""Name"":""Levis 511 Slim Fit"",
                    ""Quantity"":200,
                    ""UnitPrice"":89900,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]},
                {
                    ""OrderRowId"":2,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
                }
            ],
            ""Actions"":[""CanDeliverOrder"",""CanDeliverPartially"",""CanCancelOrder"",""CanCancelOrderRow"",""CanAddOrderRow"",""CanUpdateOrderRow""],
            ""SveaWillBuy"":true
        }";

        public static string AdminDeliveredOrder = @"
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753"",
            ""OrderStatus"":""Delivered"",
            ""SystemStatus"":""Closed"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson@mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-23T20:49:02"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":0,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":
            [{
                ""Id"":5588817,
                ""CreationDate"":""2020-06-23T20:49:50"",
                ""InvoiceId"":1111272,
                ""DeliveryAmount"":536800,
                ""CreditedAmount"":0,
                ""Credits"":[],
                ""OrderRows"":
                [{
                    ""OrderRowId"":1,
                    ""ArticleNumber"":""Ref1"",
                    ""Name"":""Levis 511 Slim Fit"",
                    ""Quantity"":200,
                    ""UnitPrice"":89900,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanCreditRow""]
                },
                { 
                    ""OrderRowId"":2,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanCreditRow""]
                }],
                ""Actions"":[""CanCreditNewRow"",""CanCreditOrderRows""],
                ""Status"":""Sent"",
                ""DueDate"":""2020-07-23T00:00:00""
            }],
            ""OrderRows"":[],
            ""Actions"":[],
            ""SveaWillBuy"":true
        }";

        public static string AdminPartiallyDeliveredOrder = @"
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753"",
            ""OrderStatus"":""Open"",
            ""SystemStatus"":""Active"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson@mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-23T20:32:07"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":0,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":
            [{
                ""Id"":5588814,
                ""CreationDate"":""2020-06-23T20:33:57"",
                ""InvoiceId"":1111271,
                ""DeliveryAmount"":179800,
                ""CreditedAmount"":0,
                ""Credits"":[],
                ""OrderRows"":
                [{
                    ""OrderRowId"":1,
                    ""ArticleNumber"":""Ref1"",
                    ""Name"":""Levis 511 Slim Fit"",
                    ""Quantity"":200,
                    ""UnitPrice"":89900,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanCreditRow""]
                }],
                ""Actions"":[""CanCreditNewRow"",""CanCreditOrderRows""],
                ""Status"":""Sent"",
                ""DueDate"":""2020-07-23T00:00:00""
            }],
            ""OrderRows"":[{
                ""OrderRowId"":2,
                ""ArticleNumber"":""Ref2"",
                ""Name"":""Levis 501 Jeans"",
                ""Quantity"":300,
                ""UnitPrice"":119000,
                ""DiscountAmount"":0,
                ""VatPercent"":0,
                ""Unit"":""SEK"",
                ""IsCancelled"":false,
                ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
            }],
            ""Actions"":[""CanDeliverOrder"",""CanDeliverPartially"",""CanCancelOrder"",""CanCancelOrderRow"",""CanAddOrderRow"",""CanUpdateOrderRow""],
            ""SveaWillBuy"":true
        }";

        public static string AdminOrderRowsCredited = @"
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753"",
            ""OrderStatus"":""Delivered"",
            ""SystemStatus"":""Closed"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson@mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-23T21:55:14"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":0,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":
            [{
                ""Id"":5588835,
                ""CreationDate"":""2020-06-23T21:55:42"",
                ""InvoiceId"":1111273,
                ""DeliveryAmount"":536800,
                ""CreditedAmount"":536800,
                ""Credits"":
                [{
                    ""Amount"":-536800,
                    ""OrderRows"":
                    [{
                        ""OrderRowId"":1,
                        ""ArticleNumber"":""Ref1"",
                        ""Name"":""Levis 511 Slim Fit"",
                        ""Quantity"":-200,
                        ""UnitPrice"":89900,
                        ""DiscountAmount"":0,
                        ""VatPercent"":0,
                        ""Unit"":""SEK"",
                        ""IsCancelled"":false,
                        ""Actions"":[]
                    },
                    {
                        ""OrderRowId"":2,
                        ""ArticleNumber"":""Ref2"",
                        ""Name"":""Levis 501 Jeans"",
                        ""Quantity"":-300,
                        ""UnitPrice"":119000,
                        ""DiscountAmount"":0,
                        ""VatPercent"":0,
                        ""Unit"":""SEK"",
                        ""IsCancelled"":false,
                        ""Actions"":[]
                    }],
                    ""Actions"":[]
                }],
                ""OrderRows"":
                [{
                    ""OrderRowId"":1,
                    ""ArticleNumber"":""Ref1"",
                    ""Name"":""Levis 511 Slim Fit"",
                    ""Quantity"":200,
                    ""UnitPrice"":89900,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[]
                },
                {
                    ""OrderRowId"":2,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[]
                }],
                ""Actions"":[],
                ""Status"":""Sent"",
                ""DueDate"":""2020-07-23T00:00:00""
            }],
            ""OrderRows"":[],
            ""Actions"":[],
            ""SveaWillBuy"":true
        }";

        public static string AdminNewRowCredited = @"
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753"",
            ""OrderStatus"":""Open"",
            ""SystemStatus"":""Active"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson@mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-23T20:32:07"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":0,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":
            [{
                ""Id"":5588814,
                ""CreationDate"":""2020-06-23T20:33:57"",
                ""InvoiceId"":1111271,
                ""DeliveryAmount"":179800,
                ""CreditedAmount"":0,
                ""Credits"":[],
                ""OrderRows"":
                [{
                    ""OrderRowId"":1,
                    ""ArticleNumber"":""Ref1"",
                    ""Name"":""Levis 511 Slim Fit"",
                    ""Quantity"":200,
                    ""UnitPrice"":89900,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanCreditRow""]
                }],
                ""Actions"":[""CanCreditNewRow"",
                ""CanCreditOrderRows""],
                ""Status"":""Sent"",
                ""DueDate"":""2020-07-23T00:00:00""
            }],
            ""OrderRows"":
            [{
                ""OrderRowId"":2,
                ""ArticleNumber"":""Ref2"",
                ""Name"":""Levis 501 Jeans"",
                ""Quantity"":300,
                ""UnitPrice"":119000,
                ""DiscountAmount"":0,
                ""VatPercent"":0,
                ""Unit"":""SEK"",
                ""IsCancelled"":false,
                ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
            }],
            ""Actions"":[""CanDeliverOrder"",""CanDeliverPartially"",""CanCancelOrder"",""CanCancelOrderRow"",""CanAddOrderRow"",""CanUpdateOrderRow""],
            ""SveaWillBuy"":true
        }";

        public static string AdminOrderRowCancelled = @"
        {
            ""Id"":2461913,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637285954306434043"",
            ""OrderStatus"":""Open"",
            ""SystemStatus"":""Active"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson @mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-24T09:37:20"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":179800,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":[],
            ""OrderRows"":
            [{
                ""OrderRowId"":1,
                ""ArticleNumber"":""Ref1"",
                ""Name"":""Levis 511 Slim Fit"",
                ""Quantity"":200,
                ""UnitPrice"":89900,
                ""DiscountAmount"":0,
                ""VatPercent"":0,
                ""Unit"":""SEK"",
                ""IsCancelled"":true,
                ""Actions"":[]
            },
            {
                ""OrderRowId"":2,
                ""ArticleNumber"":""Ref2"",
                ""Name"":""Levis 501 Jeans"",
                ""Quantity"":300,
                ""UnitPrice"":119000,
                ""DiscountAmount"":0,
                ""VatPercent"":0,
                ""Unit"":""SEK"",
                ""IsCancelled"":false,
                ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
            }],
            ""Actions"":[""CanDeliverOrder"",""CanDeliverPartially"",""CanCancelOrder"",""CanCancelOrderRow"",""CanAddOrderRow"",""CanUpdateOrderRow""],
            ""SveaWillBuy"":true
        }";

        public static string ActionResponse = @"
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753""
        }";

        public static string TaskResponse = @"
        {
            ""Id"":2291662,
            ""Status"":""Completed"",
            ""ResourceUri"":""https://paymentadminapi.svea.com/api/v1/orders/1""
        }";

        public static string AddOrderRowResponse = @"
        
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753"",
            ""OrderStatus"":""Open"",
            ""SystemStatus"":""Active"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson@mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-23T20:21:17"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":0,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":[],
            ""OrderRows"":[
                {
                    ""OrderRowId"":1,
                    ""ArticleNumber"":""Ref1"",
                    ""Name"":""Levis 511 Slim Fit"",
                    ""Quantity"":200,
                    ""UnitPrice"":89900,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]},
                {
                    ""OrderRowId"":2,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
                },
				   {
                    ""OrderRowId"":3,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
                }
            ],
            ""Actions"":[""CanDeliverOrder"",""CanDeliverPartially"",""CanCancelOrder"",""CanCancelOrderRow"",""CanAddOrderRow"",""CanUpdateOrderRow""],
            ""SveaWillBuy"":true
        }";

        public static string AddOrderRowsResponse = @"
        
        {
            ""Id"":2291662,
            ""Currency"":""SEK"",
            ""MerchantOrderId"":""637254821997417753"",
            ""OrderStatus"":""Open"",
            ""SystemStatus"":""Active"",
            ""SystemStatusMessage"":null,
            ""PaymentCreditStatus"":null,
            ""EmailAddress"":""tess.persson@mail.com"",
            ""PhoneNumber"":""08 111 111 11"",
            ""CustomerReference"":"""",
            ""PeppolId"":null,
            ""PaymentType"":""Invoice"",
            ""CreationDate"":""2020-06-23T20:21:17"",
            ""NationalId"":""194605092222"",
            ""IsCompany"":false,
            ""CancelledAmount"":0,
            ""OrderAmount"":536800,
            ""BillingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""ShippingAddress"":
            {
                ""FullName"":""Persson Tess T"",
                ""StreetAddress"":""Testgatan 1"",
                ""CoAddress"":""c/o Eriksson, Erik"",
                ""PostalCode"":""99999"",
                ""City"":""Stan"",
                ""CountryCode"":""SE""
            },
            ""Deliveries"":[],
            ""OrderRows"":[
                {
                    ""OrderRowId"":1,
                    ""ArticleNumber"":""Ref1"",
                    ""Name"":""Levis 511 Slim Fit"",
                    ""Quantity"":200,
                    ""UnitPrice"":89900,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]},
                {
                    ""OrderRowId"":2,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
                },
				   {
                    ""OrderRowId"":3,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
                },
				   {
                    ""OrderRowId"":4,
                    ""ArticleNumber"":""Ref2"",
                    ""Name"":""Levis 501 Jeans"",
                    ""Quantity"":300,
                    ""UnitPrice"":119000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":0,
                    ""Unit"":""SEK"",
                    ""IsCancelled"":false,
                    ""Actions"":[""CanDeliverRow"",""CanCancelRow"",""CanUpdateRow""]
                }
            ],
            ""Actions"":[""CanDeliverOrder"",""CanDeliverPartially"",""CanCancelOrder"",""CanCancelOrderRow"",""CanAddOrderRow"",""CanUpdateOrderRow""],
            ""SveaWillBuy"":true
        }";

        public static string CreditResponse = @"
        {
            ""CreditId"":""1783""
        }";

        public static string CheckoutCreateOrderResponse = @"
        {
            ""MerchantSettings"":
            {
                ""CheckoutValidationCallBackUri"":null,
                ""PushUri"":""https://svea.com/push.aspx?sid=123&svea_order=123"",
                ""TermsUri"":""http://localhost:51898/terms"",
                ""CheckoutUri"":""http://localhost:8080/php-checkout/examples/create-order.php"",
                ""ConfirmationUri"":""http://localhost/php-checkout/examples/get-order.php"",
                ""ActivePartPaymentCampaigns"":[],
                ""PromotedPartPaymentCampaign"":0
            },
            ""Cart"":
            {
                ""Items"":
                [{
                    ""ArticleNumber"":""ABC80"",
                    ""Name"":""Computer"",
                    ""Quantity"":1000,
                    ""UnitPrice"":500000,
                    ""DiscountAmount"":1000,
                    ""VatPercent"":2500,
                    ""Unit"":""SEK"",
                    ""TemporaryReference"":null,
                    ""RowNumber"":1,
                    ""MerchantData"":null
                },
		         {
		            ""ArticleNumber"":""7c7068f1-5223-4043-9be2-7adfe4fe9856"",
			        ""Name"":""Order Discount"",
			        ""Quantity"":100,
			        ""UnitPrice"":-107060,
			        ""DiscountPercent"":0,
			        ""DiscountAmount"":0,
			        ""VatPercent"":0,
			        ""Unit"":"""",
			        ""TemporaryReference"":null,
			        ""RowNumber"":3,
			        ""MerchantData"":null
			    }]
            },
            ""Customer"":null,
            ""ShippingAddress"":null,
            ""BillingAddress"":null,
            ""Gui"":
            {
                ""Layout"":""desktop"",
                ""Snippet"":""\r\n<div id=\""svea-checkout-container\"" data-sco-sveacheckout=\""\"" data-sco-sveacheckout-locale=\""sv-SE\"" style=\""overflow-x: hidden; overflow-y: hidden;\"">\r\n    <noscript> Please <a href=\""http://enable-javascript.com\"">enable JavaScript</a>. </noscript>\r\n    <iframe id=\""svea-checkout-iframe\"" name=\""svea-checkout-iframe\"" data-sco-sveacheckout-iframeSrc=\""https://checkoutapistage.svea.com/b/index.html?orderId=2461925&authToken=SveaCheckout+aLs4Zd%2bOBynWBr%2bNGpSVsNE7Sts%3d&token=9D5B49C2B8553149868937DB288B3C7C&locale=sv-SE\"" scrolling=\""no\"" frameborder=\""0\"" style=\""display: none; width: 1px; min-width: 100%; max-width: 100%;\""></iframe>\r\n</div>\r\n\r\n<script type=\""text/javascript\"" src=\""https://checkoutapistage.svea.com/merchantscript/build/index.js?v=ce8e0b83c1ae01bae5769711c9a3f857\""></script>\r\n<script type=\""text/javascript\"">!function(e){var t=e.document,n=t.querySelectorAll(\""[data-sco-sveacheckout]\"");if(!n.length)throw new Error(\""No Svea checkout container exists on page\"");function c(){return!!e.scoInitializeInjectedInstances&&(e.scoInitializeInjectedInstances(),!0)}if(!c()){var i=0,o=function(){i+=1,c()||(i<150?e.setTimeout(o,20):[].slice.call(n).forEach(function(e){var n=t.createElement(\""div\"");n.innerHTML=\""Something went wrong, please refresh the page\"",e.appendChild(n)}))};e.setTimeout(o,20)}}(window);</script>\r\n\r\n""
            },
            ""Locale"":""sv-SE"",
            ""Currency"":""SEK"",
            ""CountryCode"":""SE"",
            ""ClientOrderNumber"":""637285981136373230"",
            ""OrderId"":2461925,
            ""EmailAddress"":null,
            ""PhoneNumber"":null,
            ""PaymentType"":null,
            ""Payment"":null,
            ""Status"":""Created"",
            ""CustomerReference"":null,
            ""SveaWillBuyOrder"":null,
            ""IdentityFlags"":null,
            ""MerchantData"":null,
            ""PeppolId"":null
        }";

        public static string CheckoutGetOrderResponse = @"
        {
            ""MerchantSettings"":
            {
                ""CheckoutValidationCallBackUri"":null,
                ""PushUri"":""https://svea.com/push.aspx?sid=123&svea_order=123"",
                ""TermsUri"":""http://localhost:51898/terms"",
                ""CheckoutUri"":""http://localhost:8080/php-checkout/examples/create-order.php"",
                ""ConfirmationUri"":""http://localhost/php-checkout/examples/get-order.php"",
                ""ActivePartPaymentCampaigns"":[],""PromotedPartPaymentCampaign"":0
            },
            ""Cart"":
            {
                ""Items"":
                [{
                    ""ArticleNumber"":""ABC80"",
                    ""Name"":""Computer"",
                    ""Quantity"":1000,
                    ""UnitPrice"":500000,
                    ""DiscountAmount"":1000,
                    ""VatPercent"":2500,
                    ""Unit"":""SEK"",
                    ""TemporaryReference"":null,
                    ""RowNumber"":1,
                    ""MerchantData"":null
                }]
            },
            ""Customer"":null,
            ""ShippingAddress"":null,
            ""BillingAddress"":null,
            ""Gui"":
            {
                ""Layout"":""desktop"",
                ""Snippet"":""\r\n<div id=\""svea-checkout-container\"" data-sco-sveacheckout=\""\"" data-sco-sveacheckout-locale=\""sv-SE\"" style=\""overflow-x: hidden; overflow-y: hidden;\"">\r\n    <noscript> Please <a href=\""http://enable-javascript.com\"">enable JavaScript</a>. </noscript>\r\n    <iframe id=\""svea-checkout-iframe\"" name=\""svea-checkout-iframe\"" data-sco-sveacheckout-iframeSrc=\""https://checkoutapistage.svea.com/b/index.html?orderId=2461925&authToken=SveaCheckout+aLs4Zd%2bOBynWBr%2bNGpSVsNE7Sts%3d&token=9D5B49C2B8553149868937DB288B3C7C&locale=sv-SE\"" scrolling=\""no\"" frameborder=\""0\"" style=\""display: none; width: 1px; min-width: 100%; max-width: 100%;\""></iframe>\r\n</div>\r\n\r\n<script type=\""text/javascript\"" src=\""https://checkoutapistage.svea.com/merchantscript/build/index.js?v=ce8e0b83c1ae01bae5769711c9a3f857\""></script>\r\n<script type=\""text/javascript\"">!function(e){var t=e.document,n=t.querySelectorAll(\""[data-sco-sveacheckout]\"");if(!n.length)throw new Error(\""No Svea checkout container exists on page\"");function c(){return!!e.scoInitializeInjectedInstances&&(e.scoInitializeInjectedInstances(),!0)}if(!c()){var i=0,o=function(){i+=1,c()||(i<150?e.setTimeout(o,20):[].slice.call(n).forEach(function(e){var n=t.createElement(\""div\"");n.innerHTML=\""Something went wrong, please refresh the page\"",e.appendChild(n)}))};e.setTimeout(o,20)}}(window);</script>\r\n\r\n""
            },
            ""Locale"":""sv-SE"",
            ""Currency"":""SEK"",
            ""CountryCode"":""SE"",
            ""ClientOrderNumber"":""637285981136373230"",
            ""OrderId"":2461925,
            ""EmailAddress"":null,
            ""PhoneNumber"":null,
            ""PaymentType"":null,
            ""Payment"":null,
            ""Status"":""Created"",
            ""CustomerReference"":null,
            ""SveaWillBuyOrder"":null,
            ""IdentityFlags"":null,
            ""MerchantData"":null,
            ""PeppolId"":null
        }";

        public static string CheckoutUpdateOrderResponse = @"
        {
            ""MerchantSettings"":
            {
                ""CheckoutValidationCallBackUri"":null,
                ""PushUri"":""https://svea.com/push.aspx?sid=123&svea_order=123"",
                ""TermsUri"":""http://localhost:51898/terms"",
                ""CheckoutUri"":""http://localhost:8080/php-checkout/examples/create-order.php"",
                ""ConfirmationUri"":""http://localhost/php-checkout/examples/get-order.php"",
                ""ActivePartPaymentCampaigns"":[],
                ""PromotedPartPaymentCampaign"":0
            },
            ""Cart"":
            {
                ""Items"":
                [{
                    ""ArticleNumber"":""ABC80"",
                    ""Name"":""Computer"",
                    ""Quantity"":400,
                    ""UnitPrice"":200000,
                    ""DiscountAmount"":0,
                    ""VatPercent"":600,
                    ""Unit"":""SEK"",
                    ""TemporaryReference"":null,
                    ""RowNumber"":1,
                    ""MerchantData"":null
                }]
            },
            ""Customer"":null,
            ""ShippingAddress"":null,
            ""BillingAddress"":null,
            ""Gui"":
            {
                ""Layout"":""desktop"",
                ""Snippet"":""\r\n<div id=\""svea-checkout-container\"" data-sco-sveacheckout=\""\"" data-sco-sveacheckout-locale=\""sv-SE\"" style=\""overflow-x: hidden; overflow-y: hidden;\"">\r\n    <noscript> Please <a href=\""http://enable-javascript.com\"">enable JavaScript</a>. </noscript>\r\n    <iframe id=\""svea-checkout-iframe\"" name=\""svea-checkout-iframe\"" data-sco-sveacheckout-iframeSrc=\""https://checkoutapistage.svea.com/b/index.html?orderId=2461957&authToken=SveaCheckout+neyPwnlfua384OFILa6PS0%2fmXp0%3d&token=F4BF9A4F8AF7D04597BC53ECAD96D9C5&locale=sv-SE\"" scrolling=\""no\"" frameborder=\""0\"" style=\""display: none; width: 1px; min-width: 100%; max-width: 100%;\""></iframe>\r\n</div>\r\n\r\n<script type=\""text/javascript\"" src=\""https://checkoutapistage.svea.com/merchantscript/build/index.js?v=ce8e0b83c1ae01bae5769711c9a3f857\""></script>\r\n<script type=\""text/javascript\"">!function(e){var t=e.document,n=t.querySelectorAll(\""[data-sco-sveacheckout]\"");if(!n.length)throw new Error(\""No Svea checkout container exists on page\"");function c(){return!!e.scoInitializeInjectedInstances&&(e.scoInitializeInjectedInstances(),!0)}if(!c()){var i=0,o=function(){i+=1,c()||(i<150?e.setTimeout(o,20):[].slice.call(n).forEach(function(e){var n=t.createElement(\""div\"");n.innerHTML=\""Something went wrong, please refresh the page\"",e.appendChild(n)}))};e.setTimeout(o,20)}}(window);</script>\r\n\r\n""
            },
            ""Locale"":""sv-SE"",
            ""Currency"":""SEK"",
            ""CountryCode"":""SE"",
            ""ClientOrderNumber"":""637286028859296527"",
            ""OrderId"":2461957,
            ""EmailAddress"":null,
            ""PhoneNumber"":null,
            ""PaymentType"":null,
            ""Payment"":null,
            ""Status"":""Created"",
            ""CustomerReference"":null,
            ""SveaWillBuyOrder"":null,
            ""IdentityFlags"":null,
            ""MerchantData"":null,
            ""PeppolId"":null
        }";
    }
}
