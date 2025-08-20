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


        [Theory]
        [InlineData("Name",1000,0,120)]
        [InlineData("Name", 1000, 0, -5)]
        public void ThrowsArgumentException_IfDiscountPercentIsNotValidRange(string name,long unitPrice,int vatPercent,int discountPercent)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreditOrderRow(name,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                discountPercent: new MinorUnit(discountPercent)));
        }

        [Theory]
        [InlineData("Name", 1000, 0)]
        public void ThrowsArgumentOutOfRangeException_IfArticleNumberIsTooLong(string name, long unitPrice, int vatPercent)
        {
            var artNo = new string('a', 257);
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreditOrderRow(name,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                articleNumber: artNo));
        }


        [Theory]
        [InlineData("Name", 1000, 0, 2000, 100)]
        public void ThrowsInvalidOperationException_IfDiscountPercentAndDiscountAmountsAreBothPopulated(string name, long unitPrice, int vatPercent, int discountPercent,int discountAmount)
        {
            Assert.Throws<InvalidOperationException>(() => new CreditOrderRow(name,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                discountPercent: new MinorUnit(discountPercent),
                discountAmount : new MinorUnit(discountAmount)
                ));
        }


        [Theory]
        [InlineData("Name", 100, 0, 10 ,200000)]
        public void ThrowsArgumentOutOfRangeException_IfDiscountAmountExceedRowAmount(string name, long unitPrice, int vatPercent, int quantity ,int discountAmount)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CreditOrderRow(name,
                new MinorUnit(unitPrice),
                new MinorUnit(vatPercent),
                quantity : new MinorUnit(quantity),
                discountAmount: new MinorUnit(discountAmount)
                ));
        }
    }
}
