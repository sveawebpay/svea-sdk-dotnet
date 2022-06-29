using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Atata;
using NUnit.Framework;

namespace Sample.AspNetCore.SystemTests.Services
{
    public static class FluentExtensions
    {
        private static readonly Regex RegexAmount = new(@"(\d+[,.]*[\d]*) *(\w+)");
        private static readonly Regex RegexNumericalValue = new(@"\d+[,.]*[\d]*");

        public static TOwner StoreAmount<TOwner>(this UIComponent<TOwner> component, out string amount, string characterToRemove = null)
            where TOwner : PageObject<TOwner>
        {
            var content = characterToRemove != null ? component.Content.Value.Replace(characterToRemove, "") : component.Content.Value; 

            amount = RegexAmount.Match(content).Value;
            return component.Owner;
        }

        public static TOwner StoreNumericalValue<TOwner>(this UIComponent<TOwner> component, out double value, string characterToRemove = null)
            where TOwner : PageObject<TOwner>
        {
            if (string.IsNullOrEmpty(component.Content.Value))
            {
                value = 0;
                return component.Owner;
            }

            var content = characterToRemove != null ? component.Content.Value.Replace(characterToRemove, "") : component.Content.Value;

            var extractedDecimalNumber = RegexNumericalValue.Match(content).Value.Replace(",", ".");

            value = double.Parse(extractedDecimalNumber, CultureInfo.InvariantCulture);
            return component.Owner;
        }

        public static TOwner StoreCurrency<TOwner>(this UIComponent<TOwner> component, out string currency, string characterToRemove = null)
            where TOwner : PageObject<TOwner>
        {
            var content = characterToRemove != null ? component.Content.Value.Replace(characterToRemove, "") : component.Content.Value;

            currency = RegexAmount.Match(content).Groups[2].Value;
            return component.Owner;
        }

        public static TOwner StoreUri<TOwner>(this UIComponent<TOwner> component, out Uri value)
            where TOwner : PageObject<TOwner>
        {
            var val = component.Content.Value;
            value = new Uri(val, UriKind.RelativeOrAbsolute);
            return component.Owner;
        }

        public static TOwner StoreValue<TOwner>(this UIComponent<TOwner> component, out string value)
            where TOwner : PageObject<TOwner>
        {
            value = component.Content.Value;
            return component.Owner;
        }

        //public static TOwner ContainAmount<TOwner>(this IDataVerificationProvider<string, TOwner> should, string expected, string format = "{0:N2}") where TOwner : PageObject<TOwner>
        //{
        //    var actual = should.DataProvider.Value.Replace(" ", "");

        //    var actualResult = RegexAmount.Match(actual);
        //    string actualValue = actualResult.Groups[1].Value;
        //    string actualCurrency = actualResult.Groups[2].Value;

        //    var actualAmount = $"{string.Format(format, Convert.ToDecimal(actualValue.Replace(",", "."), new CultureInfo("en-US")))} {actualCurrency}";

        //    var expectedResult = RegexAmount.Match(expected);
        //    string expectedValue = expectedResult.Groups[1].Value;
        //    string expectedCurrency = expectedResult.Groups[2].Value;

        //    var expectedAmount = $"{string.Format(format, Convert.ToDecimal(expectedValue.Replace(",", "."), new CultureInfo("en-US")))} {expectedCurrency}";

        //    Assert.AreEqual(expectedAmount, actualAmount);

        //    return should.Owner;
        //}
    }
}