using System;
using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Base
{
    /// <summary>
    /// Specifies that a control should be found by CSS selector with the automation attribute.
    /// Finds the descendant or self control in the scope of the element found by the specified CSS selector.
    /// </summary>
    public class FindByAutomationAttribute : FindAttribute, ITermFindAttribute
    {
        public FindByAutomationAttribute(string automationAttribute, string type = "*")
        {
            Values = new string[] { string.Format("{0}[automation='{1}']", type, automationAttribute) };
        }

        public FindByAutomationAttribute(TermMatch termMatch, string automationAttribute, string type = "*")
        {
            switch (termMatch)
            {
                case TermMatch.Contains:
                    Values = new string[] { string.Format("{0}[contains(@automation, '{1}')]", type, automationAttribute) };
                    break;

                case TermMatch.EndsWith:
                    Values = new string[] { string.Format("{0}[ends-with(@automation, '{1}')]", type, automationAttribute) };
                    break;

                case TermMatch.Equals:
                    Values = new string[] { string.Format("{0}[automation='{1}']", type, automationAttribute) };
                    break;

                case TermMatch.StartsWith:
                    break;
            }
        }

        /// <summary>
        /// Gets the CSS selector values.
        /// </summary>
        public string[] Values { get; private set; }

        protected override Type DefaultStrategy
        {
            get { return typeof(FindByCssStrategy); }
        }

        public string[] GetTerms(UIComponentMetadata metadata)
        {
            return Values;
        }
    }
}