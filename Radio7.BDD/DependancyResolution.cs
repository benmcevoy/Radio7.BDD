using System;
using System.Configuration;
using System.IO;
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
        private static IObjectContainer _objectContainer;
        private readonly ISeleniumConfig _seleniumConfig;
        private IWebDriver _webDriver;

        public static IObjectContainer Container => _objectContainer;

        protected DependancyResolution(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _seleniumConfig = ResolveSeleniumConfig();
        }

        private static ISeleniumConfig ResolveSeleniumConfig()
        {
            var config = ConfigurationManager.GetSection("seleniumConfig") as ISeleniumConfig;

            return config ?? new SeleniumConfig();
        }

        [BeforeScenario]
        public virtual void InitializeDependancies()
        {
            if (_webDriver != null) return;

            _webDriver = GetWebDriver();

            _webDriver.Manage()
                .Timeouts()
                .ImplicitlyWait(TimeSpan.FromMilliseconds(_seleniumConfig.ImplicitWaitMilliseconds));

            // clear cookies and set browser to a consistent state for each test run
            _webDriver.Manage().Cookies.DeleteAllCookies();
            _webDriver.Manage().Window.Maximize();

            _objectContainer.RegisterInstanceAs(_webDriver);
            _objectContainer.RegisterInstanceAs(_seleniumConfig);
        }

        [AfterScenario]
        public virtual void Close()
        {
            if (_webDriver == null) return;

            if (ScenarioContext.Current.TestError != null 
                && _webDriver is ITakesScreenshot)
            {
                TakeScreenshot(_seleniumConfig);
            }
            
            _webDriver.Close();
            _webDriver.Quit();
        }

        private void TakeScreenshot(ISeleniumConfig config)
        {
            var screenshot = (_webDriver as ITakesScreenshot).GetScreenshot();
            var filename = $"{ScenarioContext.Current.ScenarioInfo.Title}.png";

            EnsureScreenshotFolder(config);

            File.WriteAllBytes(filename, screenshot.AsByteArray);
        }

        private static void EnsureScreenshotFolder(ISeleniumConfig config)
        {
            if (!Directory.Exists(config.ScreenShotFolder)) Directory.CreateDirectory(config.ScreenShotFolder);
        }

        private IWebDriver GetWebDriver()
        {
            switch (_seleniumConfig.WebDriverType)
            {
                case WebDriverType.Firefox:
                    return CreateFirefoxDriver(_seleniumConfig);

                case WebDriverType.Chrome:
                    return CreateChromeDriver(_seleniumConfig);

                case WebDriverType.IE:
                    return new InternetExplorerDriver();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static IWebDriver CreateChromeDriver(ISeleniumConfig config)
        {
            var options = new ChromeOptions();
            // --no-sandbox allows the test to be run under VS2013 in some cases
            // --ignore-certificate-errors allow our self signed, untrusted certs
            // --enable-logging --v=1 will also log messages from chrome, including console.log() 
            // to chrome_debug.log in Chrome's user data directory (in the parent directory of Default/) 
            options.AddArgument("--no-sandbox --ignore-certificate-errors");

            if (!string.IsNullOrEmpty(config.BrowserPath)) options.BinaryLocation = config.BrowserPath;

            var driver = string.IsNullOrWhiteSpace(config.DriverDirectory)
                ? new ChromeDriver(options)
                : new ChromeDriver(config.DriverDirectory, options);

            return driver;
        }

        private static IWebDriver CreateFirefoxDriver(ISeleniumConfig config)
        {
            // firefox does not care about DriverDirectory
            return string.IsNullOrEmpty(config.BrowserPath)
                ? new FirefoxDriver()
                : new FirefoxDriver(new FirefoxBinary(config.BrowserPath), new FirefoxProfile());
        }
    }
}
