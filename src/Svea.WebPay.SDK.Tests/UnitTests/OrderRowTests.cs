using System;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.CheckoutApi;

    using Xunit;

    public class OrderRowTests
    {
        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        [InlineData("", "Name", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        [InlineData("", "N", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        public void CreateOrderRow_DoesNotThrow_WhenGivenValidNewOrderRow(string articleNumber, string name, int quantity, int unitPrice, int discountAmount,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ACT
            var ex = Record.Exception(() => new OrderRow(articleNumber, name, 
                MinorUnit.FromInt(quantity), 
                MinorUnit.FromInt(unitPrice), 
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));

            //ASSERT
            Assert.Null(ex);
        }


        [Theory]
        [InlineData("adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdy", "Name", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 1000000000, 1000, 100, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1111", "Name", 20, 10000000000000, 100, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, -1, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, 1001, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "stttt", "ref2", 1, "data")]
        [InlineData("ref1", "", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "adsfasdfasdfffffffffffffffasdfasdsdfsdfsd", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st", "ref2", 1, "adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdysdsd")]
        public void ThrowsArgumentException_WhenGivenInvalidOrderRow(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => new OrderRow(articleNumber, name,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));
        }

        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        public void ThrowsArgumentException_IfMinorUnitIsNull(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new OrderRow(articleNumber, name,
                null,
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));

            Assert.Throws<ArgumentNullException>(() => new OrderRow(articleNumber, name,
                MinorUnit.FromInt(quantity),
                null,
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));

            Assert.Throws<ArgumentNullException>(() => new OrderRow(articleNumber, name,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                null,
                MinorUnit.FromInt(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));

            Assert.Throws<ArgumentNullException>(() => new OrderRow(articleNumber, name,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                null,
                unit, temporaryReference, rowNumber, merchantData));
        }

        [Theory]
        [InlineData("ref1", 20, 1000, 100, 0, "st", "ref2", 1, "data")]
        public void ThrowsArgumentException_IfNameIsNull(string articleNumber, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, string temporaryReference, int rowNumber, string merchantData = null)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new OrderRow(articleNumber, null,
                MinorUnit.FromInt(quantity),
                MinorUnit.FromInt(unitPrice),
                MinorUnit.FromInt(discountAmount),
                MinorUnit.FromInt(vatPercent),
                unit, temporaryReference, rowNumber, merchantData));
        }
    }
}
