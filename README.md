# C#/.Net Standard 2.0 SDK for Svea WebPay REST API

## Table of contents
* [1. Introduction]
* [2. Sample App]
* [3. Configuration]
* [4. Supported APIs]
* [5. Polling of Tasks]


## 1. Introduction

The CheckoutApi class methods are used to Create, Get and Update orders through Svea Checkout. (Link to official Svea documentation) [Svea Checkout API](https://checkoutapi.svea.com/docs)

The PaymentAdminApi class methods are used to administrate orders after they have been accepted by Svea. It includes functions to update, deliver, cancel and credit orders et.al. Note! This entrypoint is under construction and all functions not yet implemented. (Link to official Svea documentation) [Svea PaymentAdmin API](https://paymentadminapi.svea.com/documentation)

Any support questions should be sent to [support-webpay@sveaekonomi.se](mailto:support-webpay@sveaekonomi.se). If you find any issues with the SDK please open an issue on GitHub.

## 2. Sample App

Check the [the samples folder][samples].
To run the sample site. Make sure to add your MerchantId and SveaApiUrls to Checkout and PaymentAdmin api from Svea in appsettings.json

You will also need to add the secret token from Svea in secrets.json by running the following command in the project root folder.
`dotnet user-secrets set "Credentials.Secret" "{Your secret}" --project src/Samples/Sample.AspNetCore`

## 3. Configuration

*To configure SDK:*
Create settings file with the specified structure.

*NOTE:* This solution may change in future updates!

Step 1:

```json

{
  "SveaApiUrls": {
    "CheckoutApiUri": "https://checkoutapistage.svea.com",
    "PaymentAdminApiUri": "https://paymentadminapistage.svea.com"
  },
  "Credentials": {
    "MerchantId": "xxxxx",
    "Secret": "xxxxxxx"
  },
  "MerchantSettings": {
    "PushUri": "https://{Your domain}/push/{checkout.order.uri}",
    "TermsUri": "https://{Your domain}/terms",
    "CheckoutUri": "https://{Your domain}/CheckOut/LoadPaymentMenu",
    "ConfirmationUri": "https://{Your domain}/checkout/thankyou",
    "CheckoutValidationCallbackUri": "https://{Your domain}/validation/{checkout.order.uri}"
  }
}

```

Step 2:

Add SveaWebPayClient to services. Specify you merchantId and secret and the two clients for each api, along with there base urls.

*NOTE:* This solution may change in future updates!

```csharp

   services.AddHttpClient("checkoutApi", client => client.BaseAddress = checkoutUri)
        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(SleepDurations))
        .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

    services.AddHttpClient("paymentAdminApi", client => client.BaseAddress = paymentAdminUri)
        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(SleepDurations))
        .ConfigurePrimaryHttpMessageHandler(() => RedirectHandler);

    services.AddTransient(s =>
    {
        var httpClientFactory = s.GetService<IHttpClientFactory>();
        var checkoutApiHttpClient = httpClientFactory.CreateClient("checkoutApi");
        var paymentAdminApiHttpClient = httpClientFactory.CreateClient("paymentAdminApi");
        return new SveaWebPayClient(checkoutApiHttpClient, paymentAdminApiHttpClient, new Svea.WebPay.SDK.Credentials(merchantId, secret), s.GetService<ILogger>());
    });

```

Important to note is that AllowAutoRedirect needs to be set to false in your HttpClient message handlers, like so.

```csharp

    private static HttpClientHandler RedirectHandler => new HttpClientHandler { AllowAutoRedirect = false };

```

## 4. Supported APIs

-   **Checkout**
    -   Create order
    -   Get order
    -   Update order
-   **Payment Admin**
    -   Get order
    -   Get task
    -   **Order actions**
        -   Cancel
        -   Cancel amount
        -   Deliver order
        -   Add order row
        -   Add order rows
        -   Update order rows
        -   Replace order rows
        -   Cancel order rows   
    -   **Order row actions**
        -   Cancel order row
        -   Update order row       
    -   **Delivery actions**
        -   Credit order rows
        -   Credit order rows with fee
        -   Credit new row 
        -   Credit amount


## 5. Polling of Tasks

When executing theese actions **'Add order row'**, **'Add order rows'**, **'Deliver order'**, **'Credit order rows'** and **'Credit new row'** a Task is created in Svea. When you create the request object
for the specific action you have the option to specify a 'Polling timeout (TimeSpan)'. This will allow the SDK to poll the task until the timeout is reached or the task is done.
Then the resource object will be returned. If not specified the SDK will try once if the Task has not finished the Task Uri will be returned and the polling needs to be done by the user.

