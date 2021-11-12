using System;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;
    using Svea.WebPay.SDK.PaymentAdminApi.Request;

    using Xunit;

    public class AddOrderRowRequestTests
    {
        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st")]
        [InlineData("", "Name", 20, 1000, 100, 0, "st")]
        [InlineData("", "N", 20, 1000, 100, 0, "st")]
        public void CreateAddOrderRowRequest_DoesNotThrow_WhenGivenValidAddOrderRowRequest(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit)
        {
            //ACT
            var ex = Record.Exception(() => new AddOrderRowRequest(articleNumber, name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent), unit));

            //ASSERT
            Assert.Null(ex);
        }


        [Theory]
        [InlineData("adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdy", "Name", 20, 1000, 100, 0, "st")]
        [InlineData("ref1", "Name", 1000000000, 1000, 100, 0, "st")]
        [InlineData("ref1111", "Name", 20, 10000000000000, 100, 0, "st")]
        [InlineData("ref1", "Name", 20, 1000, -1, 0, "st")]
        [InlineData("ref1", "Name", 20, 1000, 20001, 0, "st")]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "stttt")]
        [InlineData("ref1", "", 20, 1000, 100, 0, "st")]
        [InlineData("ref1", "adsfasdfasdfffffffffffffffasdfasdsdfsdfsd", 20, 1000, 100, 0, "st")]
        [InlineData("ref1", "Name", 20, 1000, 101, 0, "st", true)]
        public void ThrowsArgumentException_WhenGivenInvalidAddOrderRowRequest(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, bool useDiscountPercent = false)
        {
            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => new AddOrderRowRequest(articleNumber, name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent), unit, useDiscountPercent));
        }

        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st")]
        public void ThrowsArgumentException_IfMinorUnitIsNull(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new AddOrderRowRequest(articleNumber, name,
                null,
                new MinorUnit(unitPrice),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent), unit));

            Assert.Throws<ArgumentNullException>(() => new AddOrderRowRequest(articleNumber, name,
                new MinorUnit(quantity),
                null,
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent),
                unit));

            Assert.Throws<ArgumentNullException>(() => new AddOrderRowRequest(articleNumber, name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(discountAmount),
                null,
                unit));
        }

        [Theory]
        [InlineData("ref1", 20, 1000, 100, 0, "st")]
        public void ThrowsArgumentException_IfNameIsNull(string articleNumber, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new AddOrderRowRequest(articleNumber, null,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent),
                unit));
        }
    }
}
