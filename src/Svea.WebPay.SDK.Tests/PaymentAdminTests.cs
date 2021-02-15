using Xunit;
using Moq;
using Svea.WebPay.SDK.PaymentAdminApi;
using System;
using System.Collections.Generic;
using System.Linq;
using Svea.WebPay.SDK.Tests.Helpers;
using Svea.WebPay.SDK.Json;
using Svea.WebPay.SDK.PaymentAdminApi.Response;
using Svea.WebPay.SDK.PaymentAdminApi.Models;
using Svea.WebPay.SDK.PaymentAdminApi.Request;

namespace Svea.WebPay.SDK.Tests
{
    using System.Text.Json;

    public class PaymentAdminTests : TestBase
    {
        [Fact]
        public async System.Threading.Tasks.Task GetOrderWithId_Should_Serialize_AsExpected()
        {
            // Arrange
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminGetOrder, JsonSerialization.Settings);
            var expectedOrder = new Order(orderResponseObject, null);
            var sveaClient = SveaClient(CreateHandlerMock(DataSample.AdminGetOrder));

            // Act
            var actualOrder = await sveaClient.PaymentAdmin.GetOrder(2291662);

            // Assert
            Assert.True(DataComparison.OrdersAreEqual(expectedOrder, actualOrder));
        }
        
        [Fact]
        public async System.Threading.Tasks.Task GetOrderWithUri_Should_Serialize_AsExpected()
        {
            // Arrange
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminGetOrder, JsonSerialization.Settings);
            var expectedOrder = new Order(orderResponseObject, null);
            var sveaClient = SveaClient(CreateHandlerMock(DataSample.AdminGetOrder));

            // Act
            var actualOrder = await sveaClient.PaymentAdmin.GetOrder(new Uri("http://www.test.com"));

            // Assert
            Assert.True(DataComparison.OrdersAreEqual(expectedOrder, actualOrder));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTaskWithId_Should_Serialize_AsExpected()
        {
            // Arrange
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var sveaClient = SveaClient(CreateHandlerMock(DataSample.TaskResponse, expectedTask.ResourceUri.OriginalString));

            // Act
            var actualTask = await sveaClient.PaymentAdmin.GetTask(1);

            // Assert
            Assert.True(DataComparison.TasksAreEqual(expectedTask, actualTask));
        }

        [Fact]
        public async System.Threading.Tasks.Task GetTaskWithUri_Should_Serialize_AsExpected()
        {
            // Arrange
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var sveaClient = SveaClient(CreateHandlerMock(DataSample.TaskResponse, expectedTask.ResourceUri.OriginalString));

            // Act
            var actualTask = await sveaClient.PaymentAdmin.GetTask(new Uri("http://www.test.com"));

            // Assert
            Mock.Verify();
            Assert.True(DataComparison.TasksAreEqual(expectedTask, actualTask));
        }

        [Fact]
        public async System.Threading.Tasks.Task Deliver_Should_Serialize_AsExpected()
        {
            // Arrange
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminDeliveredOrder, JsonSerialization.Settings);
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var expectedOrder = new Order(orderResponseObject, null);
            var sveaClient = SveaClient(CreateHandlerMockWithAction(DataSample.AdminGetOrder, "", expectedTask.ResourceUri.OriginalString, DataSample.TaskResponse, DataSample.AdminDeliveredOrder));

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var resourceResponse = await order.Actions.DeliverOrder(new DeliveryRequest(new List<long> { 1, 2 }));

            // Assert
            Assert.Equal(expectedTask.ResourceUri.OriginalString, resourceResponse.TaskUri.OriginalString);
            Assert.True(DataComparison.OrdersAreEqual(expectedOrder, resourceResponse.Resource));
        }

        [Fact]
        public async System.Threading.Tasks.Task DeliverOrderPartially_Should_Serialize_AsExpected()
        {
            // Arrange
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminPartiallyDeliveredOrder, JsonSerialization.Settings);
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var expectedOrder = new Order(orderResponseObject, null);
            var sveaClient = SveaClient(CreateHandlerMockWithAction(DataSample.AdminGetOrder, "", expectedTask.ResourceUri.OriginalString, DataSample.TaskResponse, DataSample.AdminPartiallyDeliveredOrder));

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var orderRow = new List<long> { order.OrderRows.First(x => x.AvailableActions.Contains(OrderRowActionType.CanDeliverRow)).OrderRowId };
            var deliverRequest = new DeliveryRequest(orderRow, null, TimeSpan.FromSeconds(15));
            var resourceResponse = await order.Actions.DeliverOrder(deliverRequest);

            // Assert
            Assert.Equal(expectedTask.ResourceUri.OriginalString, resourceResponse.TaskUri.OriginalString);
            Assert.True(DataComparison.OrdersAreEqual(expectedOrder, resourceResponse.Resource));
        }

        [Fact]
        public async System.Threading.Tasks.Task AddOrderRow_Should_Serialize_AsExpected()
        {
            // Arrange
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminGetOrder, JsonSerialization.Settings);
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var orderRowResponseObject = JsonSerializer.Deserialize<AddOrderRowResponseObject>(DataSample.AddOrderRowResponse);
            var expectedOrderRow = new AddOrderRowResponse(orderRowResponseObject);

            var sveaClient = SveaClient(CreateHandlerMockWithAction(DataSample.AdminGetOrder, "", expectedTask.ResourceUri.OriginalString, DataSample.TaskResponse, DataSample.AddOrderRowResponse));

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var resourceResponse = await order.Actions.AddOrderRow(
                new AddOrderRowRequest(
                    articleNumber: "1234567890",
                    name: "Slim Fit 512",
                    quantity: MinorUnit.FromInt(2),
                    unitPrice: MinorUnit.FromInt(1000),
                    discountAmount: MinorUnit.FromInt(0),
                    vatPercent: MinorUnit.FromInt(12),
                    unit: "SEK",
                    TimeSpan.FromSeconds(15)
                )
            );

            // Assert
            Assert.Equal(expectedOrderRow.OrderRowId, resourceResponse.Resource.OrderRowId);
        }

        [Fact]
        public async System.Threading.Tasks.Task AddOrderRows_Should_Serialize_AsExpected()
        {
            // Arrange
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminGetOrder, JsonSerialization.Settings);
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var expectedResponse = JsonSerializer.Deserialize<AddOrderRowsResponseObject>(DataSample.AddOrderRowsResponse);

            var sveaClient = SveaClient(CreateHandlerMockWithAction(DataSample.AdminGetOrder, DataSample.AddOrderRowsResponse));

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var resourceResponse = await order.Actions.AddOrderRows(
                new AddOrderRowsRequest(
                    new List<NewOrderRow> { 
                        new NewOrderRow(
                            name: "Slim Fit 512",
                            quantity: MinorUnit.FromInt(2),
                            unitPrice: MinorUnit.FromInt(1000),
                            vatPercent: MinorUnit.FromInt(12),
                            discountAmount: MinorUnit.FromInt(0),
                            rowId: 3,
                            unit: "SEK",
                            articleNumber: "1234567890"
                        ),
                        new NewOrderRow(
                            name: "Slim Fit 513",
                            quantity: MinorUnit.FromInt(3),
                            unitPrice: MinorUnit.FromInt(2000),
                            vatPercent: MinorUnit.FromInt(5),
                            discountAmount: MinorUnit.FromInt(0),
                            rowId: 4,
                            unit: "SEK",
                            articleNumber: "0987654321"
                        )
                    }
                )
            );

            // Assert
            // List of OrderRowId
            Assert.Equal(expectedResponse.OrderRowId, resourceResponse.Resource.OrderRowId);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreditOrderRows_Should_Serialize_AsExpected()
        {
            // Arrange
            var creditResponseObject = JsonSerializer.Deserialize<CreditResponseObject>(DataSample.CreditResponse, JsonSerialization.Settings);
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var expectedCreditResponse = new CreditResponse(creditResponseObject);
            var sveaClient = SveaClient(CreateHandlerMockWithAction(DataSample.AdminDeliveredOrder, "", expectedTask.ResourceUri.OriginalString, DataSample.TaskResponse, DataSample.CreditResponse));

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var delivery = order.Deliveries.FirstOrDefault(dlv => dlv.Id == 5588817);
            var orderRowIds = delivery.OrderRows.Where(row => row.AvailableActions.Contains(OrderRowActionType.CanCreditRow)).Select(row => (long)row.OrderRowId).ToList();
            var resourceResponse = await delivery.Actions.CreditOrderRows(new CreditOrderRowsRequest(orderRowIds));

            // Assert
            Assert.Equal(expectedTask.ResourceUri.OriginalString, resourceResponse.TaskUri.OriginalString);
            Assert.True(DataComparison.CreditResponsesAreEqual(expectedCreditResponse, resourceResponse.Resource));
        }

        [Fact]
        public async System.Threading.Tasks.Task CreditNewRow_Should_Serialize_AsExpected()
        {
            // Arrange
            var creditResponseObject = JsonSerializer.Deserialize<CreditResponseObject>(DataSample.CreditResponse, JsonSerialization.Settings);
            var expectedTask = JsonSerializer.Deserialize<Task>(DataSample.TaskResponse, JsonSerialization.Settings);
            var expectedCreditResponse = new CreditResponse(creditResponseObject);
            var sveaClient = SveaClient(CreateHandlerMockWithAction(DataSample.AdminDeliveredOrder, "", expectedTask.ResourceUri.OriginalString, DataSample.TaskResponse, DataSample.CreditResponse));

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var delivery = order.Deliveries.FirstOrDefault(dlv => dlv.Id == 5588817);
            var orderRowIds = delivery.OrderRows.Where(row => row.AvailableActions.Contains(OrderRowActionType.CanCreditRow)).Select(row => (long)row.OrderRowId).ToList();
            var resourceResponse = await delivery.Actions.CreditNewRow(new CreditNewOrderRowRequest(
                new CreditOrderRow(
                    name: "Slim Fit 512",
                    MinorUnit.FromDecimal(100),
                    MinorUnit.FromDecimal(12)
                )
            ));

            // Assert
            Assert.Equal(expectedTask.ResourceUri.OriginalString, resourceResponse.TaskUri.OriginalString);
            Assert.True(DataComparison.CreditResponsesAreEqual(expectedCreditResponse, resourceResponse.Resource));
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateOrderRow_Should_Serialize_AsExpected()
        {
            // Arrange
            var handlerMock = CreateHandlerMockWithAction(DataSample.AdminGetOrder);
            var sveaClient = SveaClient(handlerMock);

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var orderRow = order.OrderRows.FirstOrDefault(row => row.OrderRowId == 1);
            await orderRow.Actions.UpdateOrderRow(
                new UpdateOrderRowRequest(
                    orderRow.ArticleNumber,
                    orderRow.Name,
                    orderRow.Quantity,
                    orderRow.UnitPrice,
                    orderRow.DiscountAmount,
                    orderRow.VatPercent,
                    orderRow.Unit
                )
            );

            // Assert
            // No exeception thrown

        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateOrderRows_Should_Serialize_AsExpected()
        {
            // Arrange
            var handlerMock = CreateHandlerMockWithAction(DataSample.AdminGetOrder);
            var sveaClient = SveaClient(handlerMock);

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            await order.Actions.UpdateOrderRows(
                new UpdateOrderRowsRequest(
                    new List<NewOrderRow> {
                        new NewOrderRow(
                            name: order.OrderRows.ElementAt(0).Name,
                            quantity: order.OrderRows.ElementAt(0).Quantity,
                            unitPrice: order.OrderRows.ElementAt(0).UnitPrice,
                            vatPercent: order.OrderRows.ElementAt(0).VatPercent,
                            discountAmount: order.OrderRows.ElementAt(0).DiscountAmount,
                            rowId: order.OrderRows.ElementAt(0).OrderRowId,
                            unit: order.OrderRows.ElementAt(0).Unit,
                            articleNumber: order.OrderRows.ElementAt(0).ArticleNumber
                        ),
                        new NewOrderRow(
                            name: order.OrderRows.ElementAt(1).Name,
                            quantity: order.OrderRows.ElementAt(1).Quantity,
                            unitPrice: order.OrderRows.ElementAt(1).UnitPrice,
                            vatPercent: order.OrderRows.ElementAt(1).VatPercent,
                            discountAmount: order.OrderRows.ElementAt(1).DiscountAmount,
                            rowId: order.OrderRows.ElementAt(1).OrderRowId,
                            unit: order.OrderRows.ElementAt(1).Unit,
                            articleNumber: order.OrderRows.ElementAt(1).ArticleNumber
                        )
                    }
                )
            );

            // Assert
            // No exeception thrown

        }

        [Fact]
        public async System.Threading.Tasks.Task Cancel_Should_Serialize_AsExpected()
        {
            // Arrange
            var handlerMock = CreateHandlerMockWithAction(DataSample.AdminGetOrder);
            var sveaClient = SveaClient(handlerMock);

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            await order.Actions.Cancel(new CancelRequest(true));

            // Assert
            // No exeception thrown
        }

        [Fact]
        public async System.Threading.Tasks.Task CancelOrderRow_Should_Serialize_AsExpected()
        {
            // Arrange
            var handlerMock = CreateHandlerMockWithAction(DataSample.AdminGetOrder);
            var sveaClient = SveaClient(handlerMock);

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var orderRow = order.OrderRows.FirstOrDefault(row => row.OrderRowId == 1);
            await orderRow.Actions.CancelOrderRow(new CancelRequest(true));

            // Assert
            // No exeception thrown
        }

        [Fact]
        public async System.Threading.Tasks.Task CancelOrderRows_Should_Serialize_AsExpected()
        {
            // Arrange
            var handlerMock = CreateHandlerMockWithAction(DataSample.AdminGetOrder);
            var sveaClient = SveaClient(handlerMock);

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            var orderRow = order.OrderRows.FirstOrDefault(row => row.OrderRowId == 1);
            await order.Actions.CancelOrderRows(new CancelOrderRowsRequest(new long[] { 1, 2 }));

            // Assert
            // No exeception thrown
        }

        [Fact]
        public async System.Threading.Tasks.Task ReplaceOrderRows_Should_Serialize_AsExpected()
        {
            // Arrange
            var handlerMock = CreateHandlerMockWithAction(DataSample.AdminGetOrder);
            var sveaClient = SveaClient(handlerMock);

            // Act
            var order = await sveaClient.PaymentAdmin.GetOrder(2291662);
            await order.Actions.ReplaceOrderRows(
                new ReplaceOrderRowsRequest(
                    new List<NewOrderRow> {
                        new NewOrderRow(
                            name: "Slim Fit 512",
                            quantity: MinorUnit.FromInt(2),
                            unitPrice: MinorUnit.FromInt(1000),
                            vatPercent: MinorUnit.FromInt(12),
                            discountAmount: MinorUnit.FromInt(0),
                            rowId: 1,                            
                            unit: "SEK",
                            articleNumber: "1234567890"
                        ),
                        new NewOrderRow(
                            name: "Slim Fit 513",
                            quantity: MinorUnit.FromInt(3),
                            unitPrice: MinorUnit.FromInt(2000),
                            vatPercent: MinorUnit.FromInt(5),
                            discountAmount: MinorUnit.FromInt(0),
                            rowId: 2,
                            unit: "SEK",
                            articleNumber: "0987654321"
                        )
                    }
                )
            );

            // Assert
            // No exeception thrown
        }

        [Fact]
        public void CreatedOrder_Should_Serialize_AsExpected()
        {
            // Act
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminGetOrder, JsonSerialization.Settings);
            var order = new Order(orderResponseObject, null);

            // Assert
            Assert.Equal(2291662, order.Id);
            Assert.Equal("SEK", order.Currency);
            Assert.Equal("637254821997417753", order.MerchantOrderId);
            Assert.Equal(OrderStatus.Open, order.OrderStatus);
            Assert.Equal("tess.persson@mail.com", order.EmailAddress.ToString());
            Assert.Equal("08 111 111 11", order.PhoneNumber);
            Assert.Equal("", order.CustomerReference);
            Assert.Equal(PaymentType.Invoice, order.PaymentType);
            var sss = order.CreationDate.ToString("yyyy-MM-ddTHH:mm:ss");
            Assert.Equal("2020-06-23T20:21:17", order.CreationDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            Assert.Equal("194605092222", order.NationalId);
            Assert.False(order.IsCompany);
            Assert.Equal(0, order.CancelledAmount.Value);
            Assert.Equal(536800, order.OrderAmount.Value);
            Assert.True(order.SveaWillBuy);

            // Billing Address
            Assert.Equal("Persson Tess T", order.BillingAddress.FullName);
            Assert.Equal("Testgatan 1", order.BillingAddress.StreetAddress);
            Assert.Equal("c/o Eriksson, Erik", order.BillingAddress.CoAddress);
            Assert.Equal("99999", order.BillingAddress.PostalCode);
            Assert.Equal("Stan", order.BillingAddress.City);
            Assert.Equal("SE", order.BillingAddress.CountryCode);

            // Shipping Address
            Assert.Equal("Persson Tess T", order.ShippingAddress.FullName);
            Assert.Equal("Testgatan 1", order.ShippingAddress.StreetAddress);
            Assert.Equal("c/o Eriksson, Erik", order.ShippingAddress.CoAddress);
            Assert.Equal("99999", order.ShippingAddress.PostalCode);
            Assert.Equal("Stan", order.ShippingAddress.City);
            Assert.Equal("SE", order.ShippingAddress.CountryCode);

            // Order Actions
            Assert.Equal(6, order.AvailableActions.Count);
            Assert.Equal(
                new List<string> { "CanDeliverOrder", "CanDeliverPartially", "CanCancelOrder", "CanCancelOrderRow", "CanAddOrderRow", "CanUpdateOrderRow" },
                order.AvailableActions
            );

            // Deliveries
            Assert.Equal(0, order.Deliveries.Count);

            // Order Rows
            Assert.Equal(2, order.OrderRows.Count);

            // Order Row 1
            var orderRow1 = order.OrderRows.ElementAt(0);
            Assert.Equal(1, orderRow1.OrderRowId);
            Assert.Equal("Ref1", orderRow1.ArticleNumber);
            Assert.Equal("Levis 511 Slim Fit", orderRow1.Name);
            Assert.Equal(200, orderRow1.Quantity.Value);
            Assert.Equal(89900, orderRow1.UnitPrice.Value);
            Assert.Equal(0, orderRow1.DiscountAmount.Value);
            Assert.Equal(0, orderRow1.VatPercent.Value);
            Assert.Equal("SEK", orderRow1.Unit);
            Assert.False(orderRow1.IsCancelled);
            Assert.Equal(3, orderRow1.AvailableActions.Count);
            Assert.Equal(
                new List<string> { "CanDeliverRow", "CanCancelRow", "CanUpdateRow" },
                orderRow1.AvailableActions
            );
            Assert.NotNull(orderRow1.Actions.CancelOrderRow);
            Assert.NotNull(orderRow1.Actions.UpdateOrderRow);

            // Order Row 2
            var orderRow2 = order.OrderRows.ElementAt(1);
            Assert.Equal(2, orderRow2.OrderRowId);
            Assert.Equal("Ref2", orderRow2.ArticleNumber);
            Assert.Equal("Levis 501 Jeans", orderRow2.Name);
            Assert.Equal(300, orderRow2.Quantity.Value);
            Assert.Equal(119000, orderRow2.UnitPrice.Value);
            Assert.Equal(0, orderRow2.DiscountAmount.Value);
            Assert.Equal(0, orderRow2.VatPercent.Value);
            Assert.Equal("SEK", orderRow2.Unit);
            Assert.False(orderRow2.IsCancelled);
            Assert.Equal(3, orderRow2.AvailableActions.Count);
            Assert.Equal(
                new List<string> { "CanDeliverRow", "CanCancelRow", "CanUpdateRow" },
                orderRow2.AvailableActions
            );
            Assert.NotNull(orderRow2.Actions.CancelOrderRow);
            Assert.NotNull(orderRow2.Actions.UpdateOrderRow);
        }

        [Fact]
        public void DeliveredOrder_Should_Serialize_AsExpected()
        {
            // Act
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject > (DataSample.AdminDeliveredOrder, JsonSerialization.Settings);
            var order = new Order(orderResponseObject, null);

            // Assert
            Assert.Equal(2291662, order.Id);
            Assert.Equal("SEK", order.Currency);
            Assert.Equal("637254821997417753", order.MerchantOrderId);
            Assert.Equal(OrderStatus.Delivered, order.OrderStatus);
            Assert.Equal("tess.persson@mail.com", order.EmailAddress.ToString());
            Assert.Equal("08 111 111 11", order.PhoneNumber);
            Assert.Equal("", order.CustomerReference);
            Assert.Equal(PaymentType.Invoice, order.PaymentType);
            Assert.Equal("2020-06-23T20:49:02", order.CreationDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            Assert.Equal("194605092222", order.NationalId);
            Assert.False(order.IsCompany);
            Assert.Equal(0, order.CancelledAmount.Value);
            Assert.Equal(536800, order.OrderAmount.Value);
            Assert.True(order.SveaWillBuy);

            // Billing Address
            Assert.Equal("Persson Tess T", order.BillingAddress.FullName);
            Assert.Equal("Testgatan 1", order.BillingAddress.StreetAddress);
            Assert.Equal("c/o Eriksson, Erik", order.BillingAddress.CoAddress);
            Assert.Equal("99999", order.BillingAddress.PostalCode);
            Assert.Equal("Stan", order.BillingAddress.City);
            Assert.Equal("SE", order.BillingAddress.CountryCode);

            // Shipping Address
            Assert.Equal("Persson Tess T", order.ShippingAddress.FullName);
            Assert.Equal("Testgatan 1", order.ShippingAddress.StreetAddress);
            Assert.Equal("c/o Eriksson, Erik", order.ShippingAddress.CoAddress);
            Assert.Equal("99999", order.ShippingAddress.PostalCode);
            Assert.Equal("Stan", order.ShippingAddress.City);
            Assert.Equal("SE", order.ShippingAddress.CountryCode);

            // Order Actions
            Assert.Equal(0, order.AvailableActions.Count);

            // Deliveries
            var delivery = order.Deliveries.First();
            Assert.Equal(1, order.Deliveries.Count);
            Assert.Equal("2020-06-23 20:49:50", delivery.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(0, delivery.CreditedAmount);
            Assert.Equal(536800, delivery.DeliveryAmount);
            Assert.Equal("2020-07-23 00:00:00", delivery.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(5588817, delivery.Id);
            Assert.Equal(1111272, delivery.InvoiceId);
            Assert.Equal("Sent", delivery.Status);
            
            Assert.Equal(2, delivery.OrderRows.Count);

            var orderRow1 = delivery.OrderRows.ElementAt(0);
            Assert.Equal(1, orderRow1.OrderRowId);
            Assert.Equal("Levis 511 Slim Fit", orderRow1.Name);
            Assert.Equal("Ref1", orderRow1.ArticleNumber);
            Assert.Equal(200, orderRow1.Quantity.Value);
            Assert.Equal(89900, orderRow1.UnitPrice.Value);
            Assert.Equal(0, orderRow1.VatPercent.Value);
            Assert.Equal(0, orderRow1.DiscountAmount.Value);
            Assert.Equal("SEK", orderRow1.Unit);
            Assert.Equal(1, orderRow1.AvailableActions.Count);
            Assert.Equal(new List<string> { "CanCreditRow" }, orderRow1.AvailableActions);
            Assert.Null(orderRow1.Actions.CancelOrderRow);
            Assert.Null(orderRow1.Actions.UpdateOrderRow);

            var orderRow2 = delivery.OrderRows.ElementAt(1);
            Assert.Equal(2, orderRow2.OrderRowId);
            Assert.Equal("Levis 501 Jeans", orderRow2.Name);
            Assert.Equal("Ref2", orderRow2.ArticleNumber);
            Assert.Equal(300, orderRow2.Quantity.Value);
            Assert.Equal(119000, orderRow2.UnitPrice.Value);
            Assert.Equal(0, orderRow2.VatPercent.Value);
            Assert.Equal(0, orderRow2.DiscountAmount.Value);
            Assert.Equal("SEK", orderRow2.Unit);
            Assert.Equal(1, orderRow2.AvailableActions.Count);
            Assert.Equal(new List<string> { "CanCreditRow" }, orderRow2.AvailableActions);
            Assert.Null(orderRow2.Actions.CancelOrderRow);
            Assert.Null(orderRow2.Actions.UpdateOrderRow);

            Assert.Equal(2, delivery.AvailableActions.Count);
            Assert.Equal(new List<string> { "CanCreditNewRow", "CanCreditOrderRows" }, delivery.AvailableActions);
            Assert.NotNull(delivery.Actions.CreditNewRow);
            Assert.NotNull(delivery.Actions.CreditOrderRows);
            Assert.Null(delivery.Actions.CreditAmount);
            Assert.Equal(0, delivery.Credits.Count);

            // Order Rows
            Assert.Equal(0, order.OrderRows.Count);
        }

        [Fact]
        public void CreditedOrder_Should_Serialize_AsExpected()
        {
            // Act
            var orderResponseObject = JsonSerializer.Deserialize<OrderResponseObject>(DataSample.AdminOrderRowsCredited, JsonSerialization.Settings);
            var order = new Order(orderResponseObject, null);

            // Assert
            Assert.Equal(2291662, order.Id);
            Assert.Equal("SEK", order.Currency);
            Assert.Equal("637254821997417753", order.MerchantOrderId);
            Assert.Equal(OrderStatus.Delivered, order.OrderStatus);
            Assert.Equal("tess.persson@mail.com", order.EmailAddress.ToString());
            Assert.Equal("08 111 111 11", order.PhoneNumber);
            Assert.Equal("", order.CustomerReference);
            Assert.Equal(PaymentType.Invoice, order.PaymentType);
            Assert.Equal("2020-06-23T21:55:14", order.CreationDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            Assert.Equal("194605092222", order.NationalId);
            Assert.False(order.IsCompany);
            Assert.Equal(0, order.CancelledAmount.Value);
            Assert.Equal(536800, order.OrderAmount.Value);
            Assert.True(order.SveaWillBuy);

            // Billing Address
            Assert.Equal("Persson Tess T", order.BillingAddress.FullName);
            Assert.Equal("Testgatan 1", order.BillingAddress.StreetAddress);
            Assert.Equal("c/o Eriksson, Erik", order.BillingAddress.CoAddress);
            Assert.Equal("99999", order.BillingAddress.PostalCode);
            Assert.Equal("Stan", order.BillingAddress.City);
            Assert.Equal("SE", order.BillingAddress.CountryCode);

            // Shipping Address
            Assert.Equal("Persson Tess T", order.ShippingAddress.FullName);
            Assert.Equal("Testgatan 1", order.ShippingAddress.StreetAddress);
            Assert.Equal("c/o Eriksson, Erik", order.ShippingAddress.CoAddress);
            Assert.Equal("99999", order.ShippingAddress.PostalCode);
            Assert.Equal("Stan", order.ShippingAddress.City);
            Assert.Equal("SE", order.ShippingAddress.CountryCode);

            // Order Actions
            Assert.Equal(0, order.AvailableActions.Count);

            // Deliveries
            var delivery = order.Deliveries.First();
            Assert.Equal(1, order.Deliveries.Count);
            Assert.Equal("2020-06-23 21:55:42", delivery.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(536800, delivery.CreditedAmount);
            Assert.Equal(536800, delivery.DeliveryAmount);
            Assert.Equal("2020-07-23 00:00:00", delivery.DueDate?.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.Equal(5588835, delivery.Id);
            Assert.Equal(1111273, delivery.InvoiceId);
            Assert.Equal("Sent", delivery.Status);

            Assert.Equal(2, delivery.OrderRows.Count);

            var orderRow1 = delivery.OrderRows.ElementAt(0);
            Assert.Equal(1, orderRow1.OrderRowId);
            Assert.Equal("Levis 511 Slim Fit", orderRow1.Name);
            Assert.Equal("Ref1", orderRow1.ArticleNumber);
            Assert.Equal(200, orderRow1.Quantity.Value);
            Assert.Equal(89900, orderRow1.UnitPrice.Value);
            Assert.Equal(0, orderRow1.VatPercent.Value);
            Assert.Equal(0, orderRow1.DiscountAmount.Value);
            Assert.Equal("SEK", orderRow1.Unit);
            Assert.Equal(0, orderRow1.AvailableActions.Count);
            Assert.Null(orderRow1.Actions.CancelOrderRow);
            Assert.Null(orderRow1.Actions.UpdateOrderRow);

            var orderRow2 = delivery.OrderRows.ElementAt(1);
            Assert.Equal(2, orderRow2.OrderRowId);
            Assert.Equal("Levis 501 Jeans", orderRow2.Name);
            Assert.Equal("Ref2", orderRow2.ArticleNumber);
            Assert.Equal(300, orderRow2.Quantity.Value);
            Assert.Equal(119000, orderRow2.UnitPrice.Value);
            Assert.Equal(0, orderRow2.VatPercent.Value);
            Assert.Equal(0, orderRow2.DiscountAmount.Value);
            Assert.Equal("SEK", orderRow2.Unit);
            Assert.Equal(0, orderRow2.AvailableActions.Count);
            Assert.Null(orderRow2.Actions.CancelOrderRow);
            Assert.Null(orderRow2.Actions.UpdateOrderRow);

            Assert.Equal(0, delivery.AvailableActions.Count);
            Assert.Null(delivery.Actions.CreditNewRow);
            Assert.Null(delivery.Actions.CreditOrderRows);
            Assert.Null(delivery.Actions.CreditAmount);
            Assert.Equal(1, delivery.Credits.Count);

            var credit = delivery.Credits.First();
            Assert.Equal(-536800, credit.Amount);
            Assert.Equal(0, credit.Actions.Count);
            Assert.Equal(2, credit.OrderRows.Count);
            var creditedOrderRow1 = credit.OrderRows.ElementAt(0);
            Assert.Equal(1, creditedOrderRow1.OrderRowId);
            Assert.Equal("Levis 511 Slim Fit", creditedOrderRow1.Name);
            Assert.Equal("Ref1", creditedOrderRow1.ArticleNumber);
            Assert.Equal(-200, creditedOrderRow1.Quantity.Value);
            Assert.Equal(89900, creditedOrderRow1.UnitPrice.Value);
            Assert.Equal(0, creditedOrderRow1.VatPercent.Value);
            Assert.Equal(0, creditedOrderRow1.DiscountAmount.Value);
            Assert.Equal("SEK", creditedOrderRow1.Unit);

            var creditedOrderRow2 = credit.OrderRows.ElementAt(1);
            Assert.Equal(2, creditedOrderRow2.OrderRowId);
            Assert.Equal("Levis 501 Jeans", creditedOrderRow2.Name);
            Assert.Equal("Ref2", creditedOrderRow2.ArticleNumber);
            Assert.Equal(-300, creditedOrderRow2.Quantity.Value);
            Assert.Equal(119000, creditedOrderRow2.UnitPrice.Value);
            Assert.Equal(0, creditedOrderRow2.VatPercent.Value);
            Assert.Equal(0, creditedOrderRow2.DiscountAmount.Value);
            Assert.Equal("SEK", creditedOrderRow2.Unit);

            // Order Rows
            Assert.Equal(0, order.OrderRows.Count);
        }
    }
}
