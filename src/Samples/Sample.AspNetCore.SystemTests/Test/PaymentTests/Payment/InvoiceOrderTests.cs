using Atata;
using NUnit.Framework;
using Sample.AspNetCore.SystemTests.PageObjectModels.Orders;
using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;
using Svea.WebPay.SDK.PaymentAdminApi;
using System;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
    public class InvoiceOrderTests : Base.PaymentTests
    {
        public InvoiceOrderTests(string driverAlias)
            : base(driverAlias)
        {
        }

        [RetryWithException(2)]
        [Test(Description = "4784: Köp som privatperson(faktura) -> leverera faktura -> kreditera faktura, 4783: Köp som privatperson(faktura) -> makulera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreateOrderWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

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
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
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
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4784: Köp som privatperson(faktura) -> leverera faktura -> kreditera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void DeliverWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

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
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows, Is.Empty);

                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(0));
                Assert.That(response.Deliveries.First().Status, Is.EqualTo("Sent"));
                CollectionAssert.AreEquivalent(
                    new string[] { DeliveryActionType.CanCreditNewRow, DeliveryActionType.CanCreditOrderRows },
                    response.Deliveries.First().AvailableActions
                );
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4784: Köp som privatperson(faktura) -> leverera faktura -> kreditera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

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
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows, Is.Empty);

                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().Credits.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().Status, Is.EqualTo("Sent"));

                Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4776: Köp som företag(faktura) -> leverera faktura -> kreditera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CreditWithInvoiceAsCompanyAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Invoice)

                    .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)


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
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.True);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows, Is.Empty);

                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.Deliveries.First().Credits.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().Status, Is.EqualTo("Sent"));

                Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4783: Köp som privatperson(faktura) -> makulera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CancelWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                // Cancel
                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().Order.Table.Toggle.Click()
                .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                // Validate order info
                .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
                .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                // Validate order row info
                .Orders.Last().OrderRows.First().Name.Should.Equal(products[0].Name)
                .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(true.ToString())

                // Validate deliveries info
                .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.CancelledAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.First().IsCancelled, Is.True);
                Assert.That(response.Deliveries.Count, Is.EqualTo(0));
            });
        }

        [RetryWithException(2)]
        [Test(Description = "4775: Köp som företag(faktura) -> makulera faktura")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void CancelWithInvoiceAsCompanyAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Company, PaymentMethods.Option.Invoice)

                    .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    // Cancel
                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.CancelOrder.ClickAndGo()

                    // Validate order info
                    .RefreshPageUntil(x => x.Orders.Last().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
                    .Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.Invoice))

                    // Validate order rows info
                    .Orders.Last().OrderRows.First().Name.Should.Equal(products[0].Name)
                    .Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(true.ToString())

                    // Validate deliveries info
                    .Orders.Last().Deliveries.Should.HaveCount(0);

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.True);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.CompanyEmail));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

                Assert.That(response.AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
                Assert.That(response.OrderRows.First().IsCancelled, Is.True);
                Assert.That(response.Deliveries.Count, Is.EqualTo(0));
            });
        }

        [RetryWithException(2)]
        [Test(Description = "5702: RequireElectronicIdAuthentication] As a user I want to have a setting that will trigger BankId to be required on orders in the checkout")]
        [TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
        public void EnsureRequireIdAuthenticationShowUpWithInvoice(Product[] products)
        {
            Assert.DoesNotThrow(() => 
            { 
                GoToBankId(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice); 
            });
        }

        [RetryWithException(2)]
        [Test(Description = "5738")]
        [TestCaseSource(nameof(TestData), new object[] { false, false, false, false })]
        public void UpdateOrderWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrow(() => 
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                .RefreshPageUntil(x =>
                    x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                    x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                    x.Orders.Count() > 0, 15, 3)

                .Orders.Last().Order.OrderId.StoreValue(out var orderId)
                .Orders.Last().OrderRows.Last().Table.Toggle.Click()
                .Orders.Last().OrderRows.Last().Table.UpdateRow.ClickAndGo<OrdersPage>()
                .RefreshPageUntil(x => x.Orders.Last().OrderRows.Last().Name.Value.Contains("Updated"), 10, 3)
                .Orders.Last().OrderRows.Last().Table.Toggle.Click()
                .Orders.Last().OrderRows.Last().Name.Should.Contain("Updated")
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Table.UpdateRow.ClickAndGo<OrdersPage>()
                .RefreshPageUntil(x => x.Orders.First().OrderRows.Last().Name.Value.Contains("Updated"), 10, 3)
                .Orders.Last().OrderRows.First().Table.Toggle.Click()
                .Orders.Last().OrderRows.First().Name.Should.Contain("Updated");
            });
        }

        [RetryWithException(2)]
        [Test(Description = "?")]
        [TestCaseSource(nameof(TestData), new object[] { false, false, false, true })]
        public void CreditOrderRowWithFeeWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () => 
            {
                var fee = 200;

                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                    .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Deliver 1
                    .Orders.Last().OrderRows.First().Table.Toggle.Click()
                    .Orders.Last().OrderRows.First().Table.DeliverOrderRowQuantity.Set(2)
                    .Orders.Last().OrderRows.First().Table.DeliverOrderRow.ClickAndGo()

                    // Credit Order Row With Fee
                    .Orders.Last().Deliveries.First().Table.Toggle.Click()
                    .Orders.Last().Deliveries.First().Table.CreditOrderRowsWithFee.ClickAndGo()

                    // Validate order rows info
                    .Orders.Last().OrderRows.Should.HaveCount(1)
                    .Orders.Last().OrderRows.First().Quantity.Should.Equal("6.00")

                    //// Validate deliveries info
                    .Orders.Last().Deliveries.Count.Should.Equal(1)
                    .Orders.Last().Deliveries.First().Status.Should.Equal("Sent")
                    .Orders.Last().Deliveries.First().DeliveryAmount.Should.Equal(int.Parse((products.First().UnitPrice * 2).ToString()).ToString())
                    .Orders.Last().Deliveries.First().CreditedAmount.Should.Equal((int.Parse((products.First().UnitPrice * 2).ToString()) - 200).ToString());

            // Assert sdk/api response
            var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.CancelledAmount.InLowestMonetaryUnit, Is.EqualTo(0));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

                Assert.That(response.AvailableActions.Count, Is.EqualTo(6));
                Assert.That(response.OrderRows.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().AvailableActions.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo((products.First().UnitPrice * 100 * 2) - (fee * 100)));

                Assert.That(response.Deliveries.First().Credits.Count, Is.EqualTo(1));
                Assert.That(response.Deliveries.First().Credits.First().Actions, Is.Empty);
                Assert.That(response.Deliveries.First().Credits.First().Amount.InLowestMonetaryUnit, Is.EqualTo(-(products.First().UnitPrice * 100 * 2) + (fee * 100)));
                Assert.That(response.Deliveries.First().Credits.First().OrderRows.Count, Is.EqualTo(2));
            });
        }

        [RetryWithException(2)]
        [Test(Description = "?")]
        [TestCaseSource(nameof(TestData), new object[] { false, true, true, false })]
        public void AddOrderRowWithDiscountWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                    .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Add order row 1
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.AddOrderRowPercentDiscount.Set(20)
                    .Orders.Last().Order.Table.AddOrderRow.ClickAndGo()

                    // Validate order rows info
                    .Orders.Last().OrderRows.Should.HaveCount(3)

                    // Add order row 2
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.AddOrderRowAmountDiscount.Set(100)
                    .Orders.Last().Order.Table.AddOrderRow.ClickAndGo()

                    // Validate order rows info
                    .Orders.Last().OrderRows.Should.HaveCount(4);

                // Assert sdk/api response
                var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var orderWithAmountDiscount = 1000 * 2 - 100;
                var orderWithPercentDiscount = (1000 * 2) - (1000 * 2 * 20 / 100);

                _amount += orderWithAmountDiscount + orderWithPercentDiscount;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.CancelledAmount.InLowestMonetaryUnit, Is.EqualTo(0));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

                Assert.That(response.AvailableActions.Count, Is.EqualTo(6));
                Assert.That(response.OrderRows.Count, Is.EqualTo(4));

                Assert.That(response.OrderRows.ElementAt(2).Name, Is.EqualTo("Slim Fit 512"));
                Assert.That(response.OrderRows.ElementAt(2).OrderRowId, Is.EqualTo(3));
                Assert.That(response.OrderRows.ElementAt(2).Quantity.InLowestMonetaryUnit, Is.EqualTo(2 * 100));
                Assert.That(response.OrderRows.ElementAt(2).UnitPrice.InLowestMonetaryUnit, Is.EqualTo(1000 * 100));
                Assert.That(response.OrderRows.ElementAt(2).DiscountPercent.InLowestMonetaryUnit, Is.EqualTo(20 * 100));
                Assert.That(response.OrderRows.ElementAt(2).DiscountAmount.InLowestMonetaryUnit, Is.EqualTo(0));

                Assert.That(response.OrderRows.ElementAt(3).Name, Is.EqualTo("Slim Fit 512"));
                Assert.That(response.OrderRows.ElementAt(3).OrderRowId, Is.EqualTo(4));
                Assert.That(response.OrderRows.ElementAt(3).Quantity.InLowestMonetaryUnit, Is.EqualTo(2 * 100));
                Assert.That(response.OrderRows.ElementAt(3).UnitPrice.InLowestMonetaryUnit, Is.EqualTo(1000 * 100));
                Assert.That(response.OrderRows.ElementAt(3).DiscountAmount.InLowestMonetaryUnit, Is.EqualTo(100 * 100));
                Assert.That(response.OrderRows.ElementAt(3).DiscountPercent.InLowestMonetaryUnit, Is.EqualTo(0));
            });
        }

        [RetryWithException(2)]
        [Test(Description = "?")]
        [TestCaseSource(nameof(TestData), new object[] { false, true, true, false })]
        public void AddOrderRowsWithDiscountWithInvoiceAsPrivateAsync(Product[] products)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.Invoice)

                    .RefreshPageUntil(x =>
                        x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
                        x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
                        x.Orders.Count() > 0, 15, 3)

                    .Orders.Last().Order.OrderId.StoreValue(out var orderId)

                    // Add order row 1
                    .Orders.Last().Order.Table.Toggle.Click()
                    .Orders.Last().Order.Table.AddOrderRowsPercentDiscount.Set(20)
                    .Orders.Last().Order.Table.AddOrderRowsAmountDiscount.Set(100)
                    .Orders.Last().Order.Table.AddOrderRows.ClickAndGo()

                    // Validate order rows info
                    .Orders.Last().OrderRows.Should.HaveCount(4);

                // Assert sdk/api response
                var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

                var orderWithAmountDiscount = 1000 * 2 - 100;
                var orderWithPercentDiscount = (1000 * 2) - (1000 * 2 * 20 / 100);

                _amount += orderWithAmountDiscount + orderWithPercentDiscount;

                Assert.That(response.Currency, Is.EqualTo("SEK"));
                Assert.That(response.IsCompany, Is.False);
                Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
                Assert.That(response.CancelledAmount.InLowestMonetaryUnit, Is.EqualTo(0));
                Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
                Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.Invoice)));
                Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Open)));

                Assert.That(response.AvailableActions.Count, Is.EqualTo(6));
                Assert.That(response.OrderRows.Count, Is.EqualTo(4));

                Assert.That(response.OrderRows.ElementAt(2).Name, Is.EqualTo("Slim Fit 512"));
                Assert.That(response.OrderRows.ElementAt(2).OrderRowId, Is.EqualTo(3));
                Assert.That(response.OrderRows.ElementAt(2).Quantity.InLowestMonetaryUnit, Is.EqualTo(2 * 100));
                Assert.That(response.OrderRows.ElementAt(2).UnitPrice.InLowestMonetaryUnit, Is.EqualTo(1000 * 100));
                Assert.That(response.OrderRows.ElementAt(2).DiscountPercent.InLowestMonetaryUnit, Is.EqualTo(20 * 100));
                Assert.That(response.OrderRows.ElementAt(2).DiscountAmount.InLowestMonetaryUnit, Is.EqualTo(0));

                Assert.That(response.OrderRows.ElementAt(3).Name, Is.EqualTo("Slim Fit 513"));
                Assert.That(response.OrderRows.ElementAt(3).OrderRowId, Is.EqualTo(4));
                Assert.That(response.OrderRows.ElementAt(3).Quantity.InLowestMonetaryUnit, Is.EqualTo(2 * 100));
                Assert.That(response.OrderRows.ElementAt(3).UnitPrice.InLowestMonetaryUnit, Is.EqualTo(1000 * 100));
                Assert.That(response.OrderRows.ElementAt(3).DiscountAmount.InLowestMonetaryUnit, Is.EqualTo(100 * 100));
                Assert.That(response.OrderRows.ElementAt(3).DiscountPercent.InLowestMonetaryUnit, Is.EqualTo(0));
            });
        }

    }
}
