using System;
using OpenQA.Selenium;
using Radio7.BDD.Config;

namespace Radio7.BDD.SampleWebsite.Test
{
    public class SamplePage : Page
    {
        public SamplePage(IWebDriver webDriver, ISeleniumConfig seleniumConfig)
            : base(webDriver, seleniumConfig)
        {
        }

        public override Uri Url
        {
            get { return new Uri("/SamplePage.html", UriKind.Relative); }
        }

        public IWebElement TextElement
        {
            get { return WebDriver.FindElement(By.Id("textElement")); }
        }
    }
}
