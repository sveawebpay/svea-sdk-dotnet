using Atata;

using NUnit.Framework;

using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;

using Svea.WebPay.SDK.PaymentAdminApi;
using System;
using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
	using NUnit.Framework.Legacy;

	public class AccountCreditOrderTests : Base.PaymentTests
	{
		public AccountCreditOrderTests(string driverAlias)
			: base(driverAlias)
		{
		}

		[RetryWithException(2)]
		[Test(Description = "4473: Köp som privatperson(kontokredit)-> makulera delbetalning, 4474: Köp som privatperson(kontokredit) -> leverera delbetalning -> kreditera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
		public void CreateOrderWithAccountCreditAsPrivateAsync(Product[] products)
		{
			Assert.DoesNotThrowAsync(async () =>
			{
				GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)

				.RefreshPageUntil(x => 
					x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
					x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
					x.Orders.Count() > 0 &&
                    x.Orders.First().GetContent(ContentSource.TextContent).Contains(nameof(OrderStatus.Delivered)), 30, 5)

                .Orders.First().Order.OrderId.StoreValue(out var orderId)

				// Validate order info
				.Orders.First().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
				.Orders.First().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))
				.Orders.First().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
				.Orders.First().OrderRows.First().Name.Should.Equal(products.First().Name)

				// Validate deliveries info
				.Orders.First().Deliveries.Should.HaveCount(0);

			// Assert sdk/api response
			var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

				Assert.That(response.Currency, Is.EqualTo("SEK"));
				Assert.That(response.IsCompany, Is.False);
				Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
				Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
				Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.AccountCredit)));
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
		[Test(Description = "4474: Köp som privatperson(kontokredit) -> leverera delbetalning -> kreditera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
		public void DeliverWithAccountCreditAsPrivateAsync(Product[] products)
		{
			Assert.DoesNotThrowAsync(async () =>
			{
				GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)

				.RefreshPageUntil(x => 
					x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
					x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
					x.Orders.Count() > 0 &&
                    x.Orders.First().GetContent(ContentSource.TextContent).Contains(nameof(OrderStatus.Delivered)), 30, 5)

                // Deliver
                .Orders.First().Order.OrderId.StoreValue(out var orderId)
				.Orders.First().Order.Table.Toggle.Click()
				.Orders.First().Order.Table.DeliverOrder.ClickAndGo()

				// Validate order info
				.RefreshPageUntil(x => x.Orders.First().Order.OrderStatus.Value == nameof(OrderStatus.Delivered), 10, 3)
				.Orders.First().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
				.Orders.First().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))

				// Validate order rows info
				.Orders.First().OrderRows.Should.HaveCount(0)

				// Validate deliveries info
				.Orders.First().Deliveries.Count.Should.Equal(1)
				.Orders.First().Deliveries.First().Status.Should.BeNullOrEmpty();

			// Assert sdk/api response
			var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

				Assert.That(response.Currency, Is.EqualTo("SEK"));
				Assert.That(response.IsCompany, Is.False);
				Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
				Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
				Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.AccountCredit)));
				Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Delivered)));

				Assert.That(response.AvailableActions, Is.Empty);
				Assert.That(response.OrderRows, Is.Empty);

				Assert.That(response.Deliveries.Count, Is.EqualTo(1));
				Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
				Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(0));
				Assert.That(response.Deliveries.First().Status, Is.Null);
				CollectionAssert.AreEquivalent(
					new string[] { DeliveryActionType.CanCreditNewRow, DeliveryActionType.CanCreditOrderRows },
					response.Deliveries.First().AvailableActions
				);
			});
		}

		[RetryWithException(2)]
		[Test(Description = "4474: Köp som privatperson(kontokredit) -> leverera delbetalning -> kreditera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
		public void CreditWithAccountCreditAsPrivateAsync(Product[] products)
		{
			Assert.DoesNotThrowAsync(async () =>
			{
				GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)

				.RefreshPageUntil(x => 
					x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
					x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
					x.Orders.Count() > 0 &&
                    x.Orders.First().GetContent(ContentSource.TextContent).Contains(nameof(OrderStatus.Delivered)), 30, 5)

                // Deliver
                .Orders.First().Order.OrderId.StoreValue(out var orderId)
				.Orders.First().Order.Table.Toggle.Click()
				.Orders.First().Order.Table.DeliverOrder.ClickAndGo()
				.Orders.First().Deliveries.First().Table.Toggle.Click()
				.Orders.First().Deliveries.First().Table.CreditOrderRows.ClickAndGo()


				// Validate order info
				.RefreshPageUntil(x => x.Orders.First().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
				.Orders.First().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
				.Orders.First().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))

				// Validate order rows info
				.Orders.First().OrderRows.Should.HaveCount(0)

				// Validate deliveries info
				.Orders.First().Deliveries.Count.Should.Equal(1)
				.Orders.First().Deliveries.First().Status.Should.BeNullOrEmpty();

			// Assert sdk/api response
			var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

				Assert.That(response.Currency, Is.EqualTo("SEK"));
				Assert.That(response.IsCompany, Is.False);
				Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
				Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
				Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.AccountCredit)));
				Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

				Assert.That(response.AvailableActions, Is.Empty);
				Assert.That(response.OrderRows, Is.Empty);

				Assert.That(response.Deliveries.Count, Is.EqualTo(1));
				Assert.That(response.Deliveries.First().DeliveryAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
				Assert.That(response.Deliveries.First().CreditedAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
				Assert.That(response.Deliveries.First().Credits.Count, Is.EqualTo(1));
				Assert.That(response.Deliveries.First().Status, Is.Null);

				Assert.That(response.Deliveries.First().AvailableActions, Is.Empty);
			});
		}

		[RetryWithException(2)]
		[Test(Description = "4473: Köp som privatperson(kontokredit)-> makulera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false, false })]
		public void CancelWithAccountCreditAsPrivateAsync(Product[] products)
		{
			Assert.DoesNotThrowAsync( async () =>
			{
			   GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)

				.RefreshPageUntil(x => 
					x.PageUri.Value.AbsoluteUri.Contains("Orders/Details") &&
					x.Details.Exists(new SearchOptions { IsSafely = true, Timeout = TimeSpan.FromSeconds(1) }) &&
					x.Orders.Count() > 0 &&
                    x.Orders.First().GetContent(ContentSource.TextContent).Contains(nameof(OrderStatus.Delivered)), 30, 5)

                // Cancel
                .Orders.First().Order.OrderId.StoreValue(out var orderId)
				.Orders.First().Order.Table.Toggle.Click()
				.Orders.First().Order.Table.CancelOrder.ClickAndGo()

				// Validate order info
				.RefreshPageUntil(x => x.Orders.First().Order.OrderStatus.Value == nameof(OrderStatus.Cancelled), 10, 3)
				.Orders.First().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
				.Orders.First().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))

				// Validate order row info
				.Orders.First().OrderRows.Count.Should.Equal(1)
				.Orders.First().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(true.ToString())
				.Orders.First().OrderRows.First().Name.Should.Equal(products.First().Name)

				// Validate deliveries info
				.Orders.First().Deliveries.Should.HaveCount(0);

			// Assert sdk/api response
			var response = await _sveaClientSweden.PaymentAdmin.GetOrder(long.Parse(orderId)).ConfigureAwait(false);

			   Assert.That(response.Currency, Is.EqualTo("SEK"));
			   Assert.That(response.IsCompany, Is.False);
			   Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
			   Assert.That(response.CancelledAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
			   Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(_amount * 100));
			   Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.AccountCredit)));
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
		public void EnsureRequireIdAuthenticationShowUpWithAccountCredit(Product[] products)
		{
			Assert.DoesNotThrow(() => 
			{ 
				GoToBankId(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit);
			});
		}

	}
}
