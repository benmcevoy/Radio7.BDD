using OpenQA.Selenium;

namespace Radio7.BDD
{
    public static class WebElementExtensions
    {
        public static string GetValue(this IWebElement webElement)
        {
            return webElement.GetAttribute("value");
        }
    }
}
