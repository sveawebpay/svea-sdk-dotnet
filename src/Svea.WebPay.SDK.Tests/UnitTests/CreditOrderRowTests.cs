using System;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;
    using Svea.WebPay.SDK.PaymentAdminApi.Request;

    using Xunit;

    public class CreditOrderRowTests
    {
        [Theory]
        [InlineData("Name", 1000, 0)]
        [InlineData("N", 1000, 0)]
        public void CreateCreditOrderRow_DoesNotThrow_WhenGivenValidCreditOrderRow(string name, long unitPrice, int vatPercent)
        {
            //ACT
            var ex = Record.Exception(() => new CreditOrderRow(name,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent)));

            //ASSERT
            Assert.Null(ex);
        }


        [Theory]
        [InlineData("", 10000000000000, 0)]
        [InlineData("adsfasdfasdfffffffffffffffasdfasdsdfsdfsd", 10000000000000, 0)]
        [InlineData("Name", 10000000000000, 0)]
        public void ThrowsArgumentException_WhenGivenInvalidCreditOrderRow(string name, long unitPrice, int vatPercent)
        {
            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreditOrderRow(name,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent)));
        }

        [Theory]
        [InlineData("Name", 1000, 0)]
        public void ThrowsArgumentException_IfMinorUnitIsNull(string name, long unitPrice, int vatPercent)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new CreditOrderRow(name,
                null,
                new MinorUnit(vatPercent)));

            Assert.Throws<ArgumentNullException>(() => new CreditOrderRow(name,
                new MinorUnit(unitPrice),
                null));
        }

        [Theory]
        [InlineData(1000, 0)]
        public void ThrowsArgumentException_IfNameIsNull(long unitPrice, int vatPercent)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new CreditOrderRow(null,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent)));
        }
    }
}
