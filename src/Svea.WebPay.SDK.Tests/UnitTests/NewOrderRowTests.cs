using System;

namespace Svea.WebPay.SDK.Tests.UnitTests
{
    using Svea.WebPay.SDK.PaymentAdminApi.Models;

    using Xunit;

    public class NewOrderRowTests
    {
        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st", 1)]
        [InlineData("", "Name", 20, 1000, 100, 0, "st", 1)]
        [InlineData("", "N", 20, 1000, 100, 0, "st", 1)]
        public void CreateNewOrderRow_DoesNotThrow_WhenGivenValidNewOrderRow(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, long? rowid)
        {
            //ACT
            var ex = Record.Exception(() => new NewOrderRow(name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                new MinorUnit(discountAmount),
                rowid, unit, articleNumber));

            //ASSERT
            Assert.Null(ex);
        }


        [Theory]
        [InlineData("adsfasdfasdfffffffffffffffasdfasdfasdfasdfasdfaasdfoiuashdfiuasdhbbffiasydbfuaysdfvbvbuyasdfgvgvuaysdfgasudyfgasudyfgasuydfgasuyidfgasiuydffgasudyfgasuydfgasuiydfgasuydfgasoydfgaosydfgasddfsuygasdfyuagagysdfdfausyduaysdfguasdyfgausydfguyasdfggasudyfgusdyfdy", "Name", 20, 1000, 100, 0, "st", 1)]
        [InlineData("ref1", "Name", 1000000000, 1000, 100, 0, "st", 1)]
        [InlineData("ref1111", "Name", 20, 10000000000000, 100, 0, "st", 1)]
        [InlineData("ref1", "Name", 20, 1000, -1, 0, "st", 1)]
        [InlineData("ref1", "Name", 20, 1000, 20001, 0, "st", 1)]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "stttt", 1)]
        [InlineData("ref1", "", 20, 1000, 100, 0, "st", 1)]
        [InlineData("ref1", "adsfasdfasdfffffffffffffffasdfasdsdfsdfsd", 20, 1000, 100, 0, "st", 1)]
        [InlineData("ref1", "Name", 20, 1000, 101, 0, "st", 1, true)]
        public void ThrowsArgumentException_WhenGivenInvalidNewOrderRow(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, long? rowid, bool useDiscountPercent = false)
        {
            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => new NewOrderRow(name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                new MinorUnit(discountAmount),
                rowid, unit, articleNumber, useDiscountPercent));
        }


        [Theory]
        [InlineData("ref1", "Name", 20, 1000, 100, 0, "st", 1)]
        public void ThrowsArgumentException_IfMinorUnitIsNull(string articleNumber, string name, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, long? rowid)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new NewOrderRow(name,
                null,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                new MinorUnit(discountAmount),
                rowid, unit, articleNumber));

            Assert.Throws<ArgumentNullException>(() => new NewOrderRow(name,
                new MinorUnit(quantity),
                null,
                new MinorUnit(vatPercent),
                new MinorUnit(discountAmount),
                rowid, unit, articleNumber));

            Assert.Throws<ArgumentNullException>(() => new NewOrderRow(name,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                null,
                new MinorUnit(discountAmount),
                rowid, unit, articleNumber));
        }

        [Theory]
        [InlineData("ref1", 20, 1000, 100, 0, "st", 1)]
        public void ThrowsArgumentException_IfNameIsNull(string articleNumber, int quantity, long unitPrice, int discountAmount,
            int vatPercent, string unit, long? rowid)
        {
            //ASSERT
            Assert.Throws<ArgumentNullException>(() => new NewOrderRow(null,
                new MinorUnit(quantity),
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                new MinorUnit(discountAmount),
                rowid, unit, articleNumber));
        }
    }
}
