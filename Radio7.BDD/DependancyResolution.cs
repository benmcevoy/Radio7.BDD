﻿using System;
using System.Configuration;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Radio7.BDD.Config;
using TechTalk.SpecFlow;

namespace Radio7.BDD
{
    [Binding]
    public class DependancyResolution
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ISeleniumConfig _seleniumConfig;

        protected DependancyResolution(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _seleniumConfig = ResolveSeleniumConfig();
        }

        private ISeleniumConfig ResolveSeleniumConfig()
        {
            var config = ConfigurationManager.GetSection("seleniumConfig") as ISeleniumConfig;

            return config ?? new SeleniumConfig();
        }

        [BeforeScenario]
        public virtual void InitializeDependancies()
        {
            var webDriver = GetWebDriver();

            webDriver.Manage()
                .Timeouts()
                .ImplicitlyWait(TimeSpan.FromMilliseconds(_seleniumConfig.ImplicitWaitMilliseconds));

            _objectContainer.RegisterInstanceAs(webDriver);
            _objectContainer.RegisterInstanceAs(_seleniumConfig);
        }

        private IWebDriver GetWebDriver()
        {
            switch (_seleniumConfig.WebDriverType)
            {
                case WebDriverType.Firefox:
                    return new FirefoxDriver();
                case WebDriverType.Chrome:
                    return new ChromeDriver();
                case WebDriverType.IE:
                    return new InternetExplorerDriver();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
