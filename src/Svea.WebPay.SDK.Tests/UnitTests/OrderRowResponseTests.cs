using System;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.CheckoutApi;

    using Xunit;

    public class OrderRowResponseTests
    {
        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("", "Name", 20, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("", "N", 20, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        public void CreateOrderRowResponse_DoesNotThrow_WhenGivenValidNewOrderRowResponse(string articleNumber, string name, int quantity, int unitPrice, int discountAmount, int discountPercent,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ACT
            var ex = Record.Exception(() => new OrderRowResponse(articleNumber, name, 
                new MinorUnit(quantity), 
                new MinorUnit(unitPrice), 
                new MinorUnit(discountPercent),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));

            //ASSERT
            Assert.Null(ex);
        }

        [Theory]
        [InlineData("adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdy", "Name", 20, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 1000000000, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1111", "Name", 20, 10000000000000, 100, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, -1, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, 20001, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, 0, "stttt", "ref2", 1, "data")]
        [InlineData("ref1", "", 20, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "adsfasdfasdfffffffffffffffasdfasdsdfsdfsd", 20, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, 0, "st", "ref2", 1, "adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdysdsd")]
        [InlineData("ref1", "Name", 20, 1000, 0, 101, 0, "st", "ref2", 1, "data")]
        public void ThrowsArgumentException_WhenGivenInvalidOrderRowResponse(string articleNumber, string name, int quantity, long unitPrice, int discountAmount, int discountPercent,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderRowResponse(articleNumber, name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(discountPercent),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));
        }

        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, 5, "st", "ref2", 1, "data")]
        public void ThrowsArgumentException_IfMinorUnitIsNull(string articleNumber, string name, int quantity, long unitPrice, int discountAmount, int discountPercent,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new OrderRowResponse(articleNumber, name,
                null,
                new MinorUnit(unitPrice),
                new MinorUnit(discountPercent),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));

            Assert.Throws<ArgumentNullException>(() => new OrderRowResponse(articleNumber, name,
                new MinorUnit(quantity),
                null,
                new MinorUnit(discountPercent),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));

            Assert.Throws<ArgumentNullException>(() => new OrderRowResponse(articleNumber, name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(discountPercent),
                new MinorUnit(discountAmount),
                null,
                unit, temporaryReference, rowNumber, merchantData));
        }

        [Theory]
        [InlineData("ref1", 20, 1000, 100, 0, 0, "st", "ref2", 1, "data")]
        public void ThrowsArgumentException_IfNameIsNull(string articleNumber, int quantity, long unitPrice, int discountAmount, int discountPercent,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new OrderRowResponse(articleNumber, null,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(discountPercent),
                new MinorUnit(discountAmount),
                new MinorUnit(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));
        }
    }
}
