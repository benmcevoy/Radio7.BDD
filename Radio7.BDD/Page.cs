using System;
using OpenQA.Selenium;
using Radio7.BDD.Config;
using Radio7.BDD.Extensions;

namespace Radio7.BDD
{
    public abstract class Page
    {
        private readonly IWebDriver _webDriver;
        private readonly ISeleniumConfig _seleniumConfig;

        protected Page(IWebDriver webDriver, ISeleniumConfig seleniumConfig)
        {
            _webDriver = webDriver;
            _seleniumConfig = seleniumConfig;
        }

        public virtual void NavigateTo()
        {
            _webDriver.NavigateTo(Url, _seleniumConfig.BaseUrl);
        }

        protected IWebDriver WebDriver { get { return _webDriver; }}

        protected ISeleniumConfig SeleniumConfig { get { return _seleniumConfig; } }

        public abstract Uri Url { get; }
    }
}
