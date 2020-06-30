using Atata;

namespace Sample.AspNetCore.SystemTests.PageObjectModels.Base
{
    public class ControlDefinitionAutomationAttribute : ControlDefinitionAttribute
    {
        public ControlDefinitionAutomationAttribute(string automation) : base($"*[@automation='{automation}']")
        {
        }
    }

    public class ControlDefinitionPartialAutomationAttribute : ControlDefinitionAttribute
    {
        public ControlDefinitionPartialAutomationAttribute(string automation) : base($"*[contains(@automation,'{automation}')]")
        {
        }
    }
}