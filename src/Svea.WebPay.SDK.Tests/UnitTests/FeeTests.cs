using System;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using Xunit;

    public class FeeTests
    {
        [Theory]
        [InlineData("ref1", "Name", 1000, 25)]
        public void CreateCreditOrderRow_DoesNotThrow_WhenGivenValidCreditOrderRow(string articleNumber, string name, long unitPrice, int vatPercent)
        {
            //ACT
            var ex = Record.Exception(() => new Fee(articleNumber, name, new MinorUnit(unitPrice), new MinorUnit(vatPercent)));

            //ASSERT
            Assert.Null(ex);
        }


        [Theory]
        [InlineData("adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdy", "Name", 1000, 25)]
        [InlineData("ref1", "", 1000, 25)]
        [InlineData("ref1", "adsfasdfasdfffffffffffffffasdfasdsdfsdfsd", 1000, 25)]
        [InlineData("ref1", "Name", 100000000000000, 25)]
        public void ThrowsArgumentException_WhenGivenInvalidCreditOrderRow(string articleNumber, string name, long unitPrice, int vatPercent)
        {
            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => new Fee(articleNumber, name, new MinorUnit(unitPrice), new MinorUnit(vatPercent)));
        }

        [Theory]
        [InlineData("ref1", "Name")]
        public void ThrowsArgumentException_IfMinorUnitIsNull(string articleNumber, string name)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new Fee(articleNumber, name, null, new MinorUnit(25)));

            Assert.Throws<ArgumentNullException>(() => new Fee(articleNumber, name, new MinorUnit(1000), null));
        }

        [Theory]
        [InlineData("ref1", null, 1000, 25)]
        public void ThrowsArgumentException_IfNameIsNull(string articleNumber, string name, long unitPrice, int vatPercent)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new Fee(articleNumber, name, new MinorUnit(unitPrice), new MinorUnit(vatPercent)));
        }
    }
}
