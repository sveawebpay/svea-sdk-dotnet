using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.PaymentAdminApi;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class CardOrderTests : Base.PaymentTests
    {
        public CardOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [Test(Description = "4782: Köp som företag(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true })]
        public async System.Threading.Tasks.Task CreateOrderWithCardAsCompanyAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Card)
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.Rows.Should.HaveCount(3)
                .Orders.Last().Order.Table.DeliverOrder.Should.Exist()
                .Orders.Last().Order.Table.CancelOrder.Should.Exist()
                .Orders.Last().Order.Table.CancelOrderAmount.Should.Exist()

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.Rows.Should.BeEmpty()

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.True);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

            CollectionAssert.AreEquivalent(
                new string[] { OrderActionType.CanDeliverOrder, OrderActionType.CanCancelOrder, OrderActionType.CanCancelAmount },
                response.AvailableActions
            );
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
            Assert.That(response.Deliveries, Is.Null);
        }

        [Test(Description = "4782: Köp som företag(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true })]
        public async System.Threading.Tasks.Task DeliverWithCardAsCompanyAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Card)

                // Deliver
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.Rows.Should.HaveCount(2)
                .Orders.Last().Order.Table.CancelOrder.Should.Exist()
                .Orders.Last().Order.Table.CancelOrderAmount.Should.Exist()

                // Validate order rows info
                .Orders.Last().OrderRows.Should.HaveCount(0)

                // Validate deliveries info
                .Orders.Last().Deliveries.First().Status.Should.BeNull()
                .Orders.Last().Deliveries.First().Table.Toggle.Click()
                .Orders.Last().Deliveries.First().Table.Rows.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.True);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

            CollectionAssert.AreEquivalent(
                new string[] { OrderActionType.CanCancelOrder, OrderActionType.CanCancelAmount },
                response.AvailableActions
            );
            Assert.That(response.OrderRows, Is.Null);

            Assert.That(response.Deliveries.Count, Is.EqualTo(1));
            Assert.That(response.Deliveries.First().DeliveryAmount, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.Deliveries.First().CreditedAmount, Is.EqualTo(0));
            Assert.That(response.Deliveries.First().Status, Is.Null);
            Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
        }

        [Test(Description = "4772: Köp som privatperson i anonyma flödet(kort) -> makulera transaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true })]
        public async System.Threading.Tasks.Task CreditWithCardAsPrivateAnonymousAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Anonymous, Entity.Option.Private, PaymentMethods.Option.Card)

                // Deliver -> Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.Rows.Should.BeEmpty()

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.Rows.Should.BeEmpty()

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.False);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
            Assert.That(response.Deliveries, Is.Null);
        }

        [Test(Description = "4771: Köp som företag i anonyma flödet(kort) -> makulera transaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true })]
        public async System.Threading.Tasks.Task CreditWithCardAsCompanyAnonymousAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Anonymous, Entity.Option.Company, PaymentMethods.Option.Card)

                // Deliver -> Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.Rows.Should.BeEmpty()

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.Rows.Should.BeEmpty()

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.True);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
            Assert.That(response.Deliveries, Is.Null);
        }

        [Test(Description = "4782: Köp som företag(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true })]
        public async System.Threading.Tasks.Task CancelWithCardAsCompanyAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Card)

                // Deliver -> Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.Rows.Should.BeEmpty()

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.Rows.Should.BeEmpty()

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.True);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
            Assert.That(response.Deliveries, Is.Null);
        }

        [Test(Description = "4777: Köp som privatperson(kort)-> leverera korttransaktion -> makulera korttransaktion")]
        [TestCaseSource(nameof(TestData), new object[] { true })]
        public async System.Threading.Tasks.Task CancelWithCardAsPrivateAsync(Product[] products)
        {
            GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Card)

                // Deliver -> Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Card))
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.Rows.Should.BeEmpty()

                // Validate order rows info
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
                .Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.Rows.Should.BeEmpty()

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

            Assert.That(response.Currency, Is.EqualTo("SEK"));
            Assert.That(response.IsCompany, Is.False);
            Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
            Assert.That(response.OrderAmount.Value, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
            Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Card)));
            Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

            Assert.That(response.AvailableActions, Is.Empty);
            Assert.That(response.OrderRows.Count, Is.EqualTo(1));
            Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
            Assert.That(response.Deliveries, Is.Null);
        }

    }
}
