using Atata;

using NUnit.Framework;

using Sample.AspNetCore.SystemTests.Services;
using Sample.AspNetCore.SystemTests.Test.Base;
using Sample.AspNetCore.SystemTests.Test.Helpers;

using Svea.WebPay.SDK.PaymentAdminApi;

using System.Linq;

namespace Sample.AspNetCore.SystemTests.Test.PaymentTests.Payment
{
	public class AccountCreditOrderTests : Base.PaymentTests
	{
		public AccountCreditOrderTests(string driverAlias)
			: base(driverAlias)
		{
		}

		[RetryWithException(2)]
		[Test(Description = "4473: Köp som privatperson(kontokredit)-> makulera delbetalning, 4474: Köp som privatperson(kontokredit) -> leverera delbetalning -> kreditera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false })]
		public async System.Threading.Tasks.Task CreateOrderWithAccountCreditAsPrivateAsync(Product[] products)
		{
			GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)
				
				.RefreshPageUntil(x => x.PageUri.Value.AbsoluteUri.Contains("Orders/Details"), 10, 3)

				.Orders.Last().Order.OrderId.StoreValue(out var orderId)

				// Validate order info
				.Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Open))
				.Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))
				.Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(false.ToString())
				.Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

				// Validate deliveries info
				.Orders.Last().Deliveries.Should.HaveCount(0);

			// Assert sdk/api response
			var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

			Assert.That(response.Currency, Is.EqualTo("SEK"));
			Assert.That(response.IsCompany, Is.False);
			Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
			Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
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
		}

		[RetryWithException(2)]
		[Test(Description = "4474: Köp som privatperson(kontokredit) -> leverera delbetalning -> kreditera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false })]
		public async System.Threading.Tasks.Task DeliverWithAccountCreditAsPrivateAsync(Product[] products)
		{
			GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)
				
				.RefreshPageUntil(x => x.PageUri.Value.AbsoluteUri.Contains("Orders/Details"), 10, 3)

				// Deliver
				.Orders.Last().Order.OrderId.StoreValue(out var orderId)
				.Orders.Last().Order.Table.Toggle.Click()
				.Orders.Last().Order.Table.DeliverOrder.ClickAndGo()

				// Validate order info
				.Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Delivered))
				.Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))

				// Validate order rows info
				.Orders.Last().OrderRows.Should.HaveCount(0)

				// Validate deliveries info
				.Orders.Last().Deliveries.Count.Should.Equal(1)
				.Orders.Last().Deliveries.First().Status.Should.BeNull();

			// Assert sdk/api response
			var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

			Assert.That(response.Currency, Is.EqualTo("SEK"));
			Assert.That(response.IsCompany, Is.False);
			Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
			Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
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
		}

		[RetryWithException(2)]
		[Test(Description = "4474: Köp som privatperson(kontokredit) -> leverera delbetalning -> kreditera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false })]
		public async System.Threading.Tasks.Task CreditWithAccountCreditAsPrivateAsync(Product[] products)
		{
			GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)

				.RefreshPageUntil(x => x.PageUri.Value.AbsoluteUri.Contains("Orders/Details"), 10, 3)

				// Deliver
				.Orders.Last().Order.OrderId.StoreValue(out var orderId)
				.Orders.Last().Order.Table.Toggle.Click()
				.Orders.Last().Order.Table.DeliverOrder.ClickAndGo()
				.Orders.Last().Deliveries.First().Table.Toggle.Click()
				.Orders.Last().Deliveries.First().Table.CreditOrderRows.ClickAndGo()

				// Validate order info
				.Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
				.Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))

				// Validate order rows info
				.Orders.Last().OrderRows.Should.HaveCount(0)

				// Validate deliveries info
				.Orders.Last().Deliveries.Count.Should.Equal(1)
				.Orders.Last().Deliveries.First().Status.Should.BeNull();

			// Assert sdk/api response
			var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

			Assert.That(response.Currency, Is.EqualTo("SEK"));
			Assert.That(response.IsCompany, Is.False);
			Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
			Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
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
		}

		[RetryWithException(2)]
		[Test(Description = "4473: Köp som privatperson(kontokredit)-> makulera delbetalning")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false })]
		public async System.Threading.Tasks.Task CancelWithAccountCreditAsPrivateAsync(Product[] products)
		{
			GoToOrdersPage(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit)

				.RefreshPageUntil(x => x.PageUri.Value.AbsoluteUri.Contains("Orders/Details"), 10, 3)

				// Cancel
				.Orders.Last().Order.OrderId.StoreValue(out var orderId)
				.Orders.Last().Order.Table.Toggle.Click()
				.Orders.Last().Order.Table.CancelOrder.ClickAndGo()

				// Validate order info
				.Orders.Last().Order.OrderStatus.Should.Equal(nameof(OrderStatus.Cancelled))
				.Orders.Last().Order.PaymentType.Should.Equal(nameof(PaymentType.AccountCredit))

				// Validate order row info
				.Orders.Last().OrderRows.Count.Should.Equal(1)
				.Orders.Last().OrderRows.First().IsCancelled.Should.EqualIgnoringCase(true.ToString())
				.Orders.Last().OrderRows.First().Name.Should.Equal(products.First().Name)

				// Validate deliveries info
				.Orders.Last().Deliveries.Should.HaveCount(0);

			// Assert sdk/api response
			var response = await _sveaClient.PaymentAdmin.GetOrder(long.Parse(orderId));

			Assert.That(response.Currency, Is.EqualTo("SEK"));
			Assert.That(response.IsCompany, Is.False);
			Assert.That(response.EmailAddress.ToString(), Is.EqualTo(TestDataService.Email));
			Assert.That(response.CancelledAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
			Assert.That(response.OrderAmount.InLowestMonetaryUnit, Is.EqualTo(products.Sum(x => x.Quantity * x.UnitPrice) * 100));
			Assert.That(response.PaymentType.ToString(), Is.EqualTo(nameof(PaymentType.AccountCredit)));
			Assert.That(response.OrderStatus.ToString(), Is.EqualTo(nameof(OrderStatus.Cancelled)));

			Assert.That(response.AvailableActions, Is.Empty);
			Assert.That(response.OrderRows.Count, Is.EqualTo(1));
			Assert.That(response.OrderRows.First().AvailableActions, Is.Empty);
			Assert.That(response.OrderRows.First().IsCancelled, Is.True);
			Assert.That(response.Deliveries.Count, Is.EqualTo(0));
		}

		[RetryWithException(2)]
		[Test(Description = "5702: RequireElectronicIdAuthentication] As a user I want to have a setting that will trigger BankId to be required on orders in the checkout")]
		[TestCaseSource(nameof(TestData), new object[] { true, false, false })]
		public void EnsureRequireIdAuthenticationShowUpWithAccountCredit(Product[] products)
		{
			GoToBankId(products, Checkout.Option.Identification, Entity.Option.Private, PaymentMethods.Option.AccountCredit);
		}

	}
}
