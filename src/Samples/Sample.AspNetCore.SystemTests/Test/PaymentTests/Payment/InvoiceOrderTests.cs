using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.PaymentAdminApi;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class InvoiceOrderTests : Base.PaymentTests
    {
        public InvoiceOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(3)]
        [Test(Description = "4784: Köp som privatperson(faktura) -> leverera faktura -> kreditera faktura, 4783: Köp som privatperson(faktura) -> makulera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public async System.Threading.Tasks.Task CreateOrderWithInvoiceAsPrivateAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                // Validate order row info
                .Orders.Last().OrderRows.Count.Should.Equal(1)
                .Orders.Last().OrderRows.First().Name.Should.Equal(products[0].Name)
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);


            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.False);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

            CollectionAssert.AreEquivalent(
                new string[] { OrderActionType.CanDeliverOrder, OrderActionType.CanDeliverOrderPartially, OrderActionType.CanCancelOrder, OrderActionType.CanAddOrderRow, OrderActionType.CanCancelOrderRow, OrderActionType.CanUpdateOrderRow },
                response.AvailableActions
            );
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            CollectionAssert.AreEquivalent(
                new string[] { OrderRowActionType.CanCancelRow, OrderRowActionType.CanUpdateRow, OrderRowActionType.CanDeliverRow },
                response.OrderRows.First().AvailableActions
            );

            Assert.That(response.Deliveries.Count, Is.EqualTo(0));
        }

        [RetryWithException(3)]
        [Test(Description = "4784: Köp som privatperson(faktura) -> leverera faktura -> kreditera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public async System.Threading.Tasks.Task DeliverWithInvoiceAsPrivateAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                // Deliver
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                // Validate order rows info
                .Orders.Last().OrderRows.Should.HaveCount(0)

                // Validate deliveries info
                .Orders.Last().Deliveries.Count.Should.Equal(1)
                .Orders.Last().Deliveries.First().Status.Should.Equal("Sent");

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.False);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows, Is.Empty);

            Assert.That(response.Deliveries.Count, Is.EqualTo(1));
            Assert.That(response.Deliveries.First().DeliveryAmount, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.Deliveries.First().CreditedAmount, Is.EqualTo(0));
            Assert.That(response.Deliveries.First().Status, Is.EqualTo("Sent"));
            CollectionAssert.AreEquivalent(
                new string[] { DeliveryActionType.CanCreditNewRow, DeliveryActionType.CanCreditOrderRows },
                response.Deliveries.First().AvailableActions
            );
        }

        [RetryWithException(3)]
        [Test(Description = "4784: Köp som privatperson(faktura) -> leverera faktura -> kreditera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public async System.Threading.Tasks.Task CreditWithInvoiceAsPrivateAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                // Deliver -> Credit
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Deliveries.First().Table.Toggle.Click()
                .Orders.Last().Deliveries.First().Table.CreditOrderRows.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                // Validate order rows info
                .Orders.Last().OrderRows.Should.HaveCount(0)

                // Validate deliveries info
                .Orders.Last().Deliveries.Count.Should.Equal(1)
                .Orders.Last().Deliveries.First().Status.Should.Equal("Sent");

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.False);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows, Is.Empty);

            Assert.That(response.Deliveries.Count, Is.EqualTo(1));
            Assert.That(response.Deliveries.First().DeliveryAmount, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.Deliveries.First().CreditedAmount, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.Deliveries.First().Credits.Count, Is.EqualTo(1));
            Assert.That(response.Deliveries.First().Status, Is.EqualTo("Sent"));

            Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
        }

        [RetryWithException(3)]
        [Test(Description = "4776: Köp som företag(faktura) -> leverera faktura -> kreditera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public async System.Threading.Tasks.Task CreditWithInvoiceAsCompanyAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Invoice)

                // Deliver -> Credit
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Deliveries.First().Table.Toggle.Click()
                .Orders.Last().Deliveries.First().Table.CreditOrderRows.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                // Validate order rows info
                .Orders.Last().OrderRows.Should.HaveCount(0)

                // Validate deliveries info
                .Orders.Last().Deliveries.Count.Should.Equal(1)
                .Orders.Last().Deliveries.First().Status.Should.Equal("Sent");

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.True);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows, Is.Empty);

            Assert.That(response.Deliveries.Count, Is.EqualTo(1));
            Assert.That(response.Deliveries.First().DeliveryAmount, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.Deliveries.First().CreditedAmount, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.Deliveries.First().Credits.Count, Is.EqualTo(1));
            Assert.That(response.Deliveries.First().Status, Is.EqualTo("Sent"));

            Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
        }

        [RetryWithException(3)]
        [Test(Description = "4783: Köp som privatperson(faktura) -> makulera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public async System.Threading.Tasks.Task CancelWithInvoiceAsPrivateAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                // Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                // Validate order row info
                .Orders.Last().OrderRows.First().Name.Should.Equal(products[0].Name)
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(true.ToString())

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.False);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.CancelledAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.First().IsCancelled, Is.True);
            Assert.That(response.Deliveries.Count, Is.EqualTo(0));
        }

        [RetryWithException(3)]
        [Test(Description = "4775: Köp som företag(faktura) -> makulera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public async System.Threading.Tasks.Task CancelWithInvoiceAsCompanyAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Invoice)

                // Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                // Validate order rows info
                .Orders.Last().OrderRows.First().Name.Should.Equal(products[0].Name)
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(true.ToString())

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.True);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.First().IsCancelled, Is.True);
            Assert.That(response.Deliveries.Count, Is.EqualTo(0));
        }

        [RetryWithException(3)]
        [Test(Description = "5702: RequireElectronicIdAuthentication] As a user I want to have a setting that will trigger BankId to be required on orders in the checkout")]
        [TestCaseSource(nameof(TestData), new object[] { true, false })]
        public void EnsureRequireIdAuthenticationShowUpWithInvoice(Product[] products)
        {
            GoToBankId(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice);
        }

        [RetryWithException(3)]
        [Test(Description = "5738")]
        [TestCaseSource(nameof(TestData), new object[] { false, false })]
        public void UpdateOrderWithInvoiceAsPrivateAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().OrderRows.Last().Table.Toggle.Click()
                .Orders.Last().OrderRows.Last().Table.UpdateRow.ClickAndGo<OrdersPage>()
                .Orders.Last().OrderRows.Last().Table.Toggle.Click()
                .Orders.Last().OrderRows.Last().Name.Should.Contain("Updated")
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.UpdateRow.ClickAndGo<OrdersPage>()
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Name.Should.Contain("Updated");
        }
    }
}
