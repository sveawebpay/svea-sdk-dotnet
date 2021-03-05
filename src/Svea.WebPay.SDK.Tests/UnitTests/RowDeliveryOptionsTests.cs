using Svea.WebPay.SDK.PaymentAdminApi.Models;
using System;
using Xunit;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    public class RowDeliveryOptionsTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(10, 10)]
        [InlineData(1, 0)]
        public void CreateRowDeliveryOptions_DoesNotThrow_WhenGivenValidParameters(long orderRowId, long quantity)
        {
            //ACT
            var ex = Record.Exception(() => new RowDeliveryOptions(orderRowId, quantity));

            //ASSERT
            Assert.Null(ex);
        }

        [Theory]
        [InlineData(1, -1)]
        public void CreateRowDeliveryOptions_Throws_WhenGivenInvalidParameters(long orderRowId, long quantity)
        {
            //ACT / ASSERT
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new RowDeliveryOptions(orderRowId, quantity));
        }
    }
}
