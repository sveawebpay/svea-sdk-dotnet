using System;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;
    using Svea.WebPay.SDK.PaymentAdminApi.Request;

    using Xunit;

    public class UpdateOrderRowRequestTests
    {
        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st")]
        [InlineData("", "Name", 20, 1000, 100, 0, "st")]
        [InlineData("", "N", 20, 1000, 100, 0, "st")]
        public void CreateUpdateOrderRowRequest_DoesNotThrow_WhenGivenValidUpdateOrderRowRequest(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit)
        {
            //ACT
            var ex = Record.Exception(() => new UpdateOrderRowRequest(articleNumber, name,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent), unit));

            //ASSERT
            Assert.Null(ex);
        }


        [Theory]
        [InlineData("adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdy", "Name", 20, 1000, 100, 0, "st")]
        [InlineData("ref1", "Name", 1000000000, 1000, 100, 0, "st")]
        [InlineData("ref1111", "Name", 20, 10000000000000, 100, 0, "st")]
        [InlineData("ref1", "Name", 20, 1000, -1, 0, "st")]
        [InlineData("ref1", "Name", 20, 1000, 1001, 0, "st")]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "stttt")]
        [InlineData("ref1", "", 20, 1000, 100, 0, "st")]
        [InlineData("ref1", "adsfasdfasdfffffffffffffffasdfasdsdfsdfsd", 20, 1000, 100, 0, "st")]
        public void ThrowsArgumentException_WhenGivenInvalidUpdateOrderRowRequest(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit)
        {
            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => new UpdateOrderRowRequest(articleNumber, name,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent), unit));
        }

        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st")]
        public void ThrowsArgumentException_IfMinorUnitIsNull(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new UpdateOrderRowRequest(articleNumber, name,
                null,
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent), unit));

            Assert.Throws<ArgumentNullException>(() => new UpdateOrderRowRequest(articleNumber, name,
                MinorUnit.FromInt(quantity),
                null,
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent), unit));

            Assert.Throws<ArgumentNullException>(() => new UpdateOrderRowRequest(articleNumber, name,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                null, unit));
        }

        [Theory]
        [InlineData("ref1", 20, 1000, 100, 0, "st")]
        public void ThrowsArgumentException_IfNameIsNull(string articleNumber, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new UpdateOrderRowRequest(articleNumber, null,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent), unit));
        }
    }
}
