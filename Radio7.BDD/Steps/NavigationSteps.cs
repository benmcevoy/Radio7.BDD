using System;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Radio7.BDD.Config;
using Radio7.BDD.Extensions;
using TechTalk.SpecFlow;

namespace Radio7.BDD.Steps
{
    [Binding]
    public class NavigationSteps
    {
        private readonly IWebDriver _webDriver;
        private readonly ISeleniumConfig _config;

        public NavigationSteps(IWebDriver webDriver, ISeleniumConfig config)
        {
            _webDriver = webDriver;
            _config = config;
        }

        [Given(@"I navigate to ""(.*)""")]
        [When(@"I navigate to ""(.*)""")]
        public void NavigateTo(string url)
        {
            _webDriver.NavigateTo(new Uri(url, UriKind.RelativeOrAbsolute), _config.BaseUrl);
        }

        [Given(@"the current url is ""(.*)""")]
        [Then(@"the current url is ""(.*)""")]
        public void ThenTheCurrentUrlIs(string url)
        {
            try
            {
                var currentUrl = new Uri(_webDriver.Url);
                var expectedUrl = CreateUri(url);

                Assert.IsTrue(currentUrl.PathAndQuery.Equals(expectedUrl.PathAndQuery, StringComparison.OrdinalIgnoreCase));
            }
            catch (UriFormatException)
            {
                Assert.Fail("The url '{0}' was not valid.", url);
            }
        }

        [Then(@"the link ""(.*)"" opened in a new tab")]
        public void ThenTheLinkOpenedInANewTab(string expectedUrl)
        {
            var browserTabs = _webDriver.WindowHandles;
            _webDriver.SwitchTo().Window(browserTabs[1]);

            // check is it correct page opened or not (e.g. check page's title or url, etc.)
            Assert.IsTrue(CreateUri(expectedUrl).Equals(CreateUri(_webDriver.Url)));

            // close tab and get back
            _webDriver.Close();
            _webDriver.SwitchTo().Window(browserTabs[0]);
        }

        [Then(@"the browser title is ""(.*)""")]
        public void ThenTheBrowserTitleIs(string expectedBrowserTitle)
        {
            Assert.AreEqual(expectedBrowserTitle, _webDriver.Title);
        }

        [When(@"I click the browser back button")]
        public void WhenIClickTheBrowserBackButton()
        {
            _webDriver.Navigate().Back();
        }

        [When(@"I click the browser forward button")]
        public void WhenIClickTheBrowserForwardButton()
        {
            _webDriver.Navigate().Forward();
        }

        [Then(@"the current url path is ""(.*)""")]
        public void TheCurrentUrlPathIs(string urlPath)
        {
            var currentUrl = new Uri(_webDriver.Url);

            Assert.IsTrue(currentUrl.AbsolutePath.Equals(urlPath, StringComparison.CurrentCultureIgnoreCase));
        }

        [Then(@"the current url has query string parameter ""(.*)""")]
        public void TheCurrentUrlHasQueryStringParameter(string queryStringKey)
        {
            var currentUrl = new Uri(_webDriver.Url);
            var nvc = HttpUtility.ParseQueryString(currentUrl.Query);

            Assert.IsTrue(nvc.AllKeys.Contains(queryStringKey));
        }

        [Then(@"the current url has query string parameters")]
        public void TheCurrentUrlHasQueryStringParametersWithValues(Table table)
        {
            foreach (var tableRow in table.Rows)
            {
                var key = tableRow.Values.First();
                var expectedValue = tableRow.Values.Second();

                TheCurrentUrlHasQueryStringParameterWithValue(key, expectedValue);
            }
        }

        public void TheCurrentUrlHasQueryStringParameterWithValue(string key, string expectedValue)
        {
            var currentUrl = new Uri(_webDriver.Url);
            var nvc = HttpUtility.ParseQueryString(currentUrl.Query);

            Assert.IsTrue(nvc[key].Equals(expectedValue, StringComparison.CurrentCultureIgnoreCase));
        }

        private Uri CreateUri(string url)
        {
            url = url.ToLowerInvariant();

            if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                return new Uri(url, UriKind.Absolute);
            }

            return new Uri(
                    $"{_config.BaseUrl.ToString().TrimEnd('/')}/{url.TrimStart('/')}",
                    UriKind.RelativeOrAbsolute);
        }
    }
}
