using System;
using System.Drawing;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Radio7.BDD.Config;
using Radio7.BDD.Extensions;
using TechTalk.SpecFlow;

namespace Radio7.BDD
{
    /// <summary>
    /// Common Selenium steps that are page agnostic.
    /// </summary>
    [Binding]
    public class CommonSteps
    {
        private readonly IWebDriver _webDriver;
        private readonly ISeleniumConfig _seleniumConfig;

        public CommonSteps(IWebDriver webDriver, ISeleniumConfig seleniumConfig)
        {
            _webDriver = webDriver;
            _seleniumConfig = seleniumConfig;
        }

        [Given(@"I have navigated to ""(.*)""")]
        [When(@"I have navigated to ""(.*)""")]
        public void GivenIHaveNavigatedTo(string url)
        {
            _webDriver.NavigateTo(new Uri(url, UriKind.RelativeOrAbsolute), _seleniumConfig.BaseUrl);
        }

        [Given(@"the current url is ""(.*)""")]
        [Then(@"the current url is ""(.*)""")]
        public void ThenTheCurrentUrlIs(string url)
        {
            try
            {
                var currentUrl = new Uri(_webDriver.Url);
                Uri expectedUrl;

                if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    expectedUrl = new Uri(url, UriKind.Absolute);
                }
                else
                {
                    expectedUrl = new Uri(
                        string.Format("{0}/{1}",
                            _seleniumConfig.BaseUrl.ToString().TrimEnd('/'),
                            url.TrimStart('/')),
                        UriKind.RelativeOrAbsolute);
                }

                Assert.IsTrue(currentUrl.PathAndQuery.Equals(expectedUrl.PathAndQuery, StringComparison.OrdinalIgnoreCase));
            }
            catch (UriFormatException)
            {
                Assert.Fail("The url '{0}' was not valid.", url);
            }
        }

        [Given(@"I click the element labelled ""(.*)""")]
        [When(@"I click the element labelled ""(.*)""")]
        public void GivenIClickTheElementLabelled(string label)
        {
            var field = _webDriver.WaitUntilElementIsClickable(By.XPath(string.Format("//*[text()='{0}']", label)));

            field.Click();
        }

        [Given(@"I enter ""(.*)"" in the element with id ""(.*)""")]
        [When(@"I enter ""(.*)"" in the element with id ""(.*)""")]
        public void GivenIEnterInTheElementWithId(string value, string id)
        {
            var field = _webDriver.WaitUntilElementIsClickable(By.Id(id));

            field.SendKeys(value);
        }

        [Given(@"I click the element with id ""(.*)""")]
        [When(@"I click the element with id ""(.*)""")]
        public void GivenIClickTheElementWithId(string id)
        {
            var field = _webDriver.WaitUntilElementIsClickable(By.Id(id));

            field.Click();
        }

        [Given(@"I clear the element with id ""(.*)""")]
        [When(@"I clear the element with id ""(.*)""")]
        public void GivenIClearTheElementWithId(string id)
        {
            var field = _webDriver.WaitUntilElementIsClickable(By.Id(id));

            field.Clear();
        }

        [Given(@"the field with id ""(.*)"" has value ""(.*)""")]
        [When(@"the field with id ""(.*)"" has value ""(.*)""")]
        [Then(@"the field with id ""(.*)"" has value ""(.*)""")]
        public void GivenFieldWithIdHasValue(string id, string value)
        {
            var field = _webDriver.WaitUntilElementIsVisible(By.Id(id));
            Assert.AreEqual(value.ToLowerInvariant(), field.GetValue().ToLowerInvariant());
        }

        [Given(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        [When(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        [Then(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        public void GivenIWaitForValueToBePresentInElementWithId(string text, string id)
        {
            _webDriver.WaitUntilTextIsPresentInElementValue(By.Id(id), text);
        }

        [Given(@"element with id ""(.*)"" is clickable")]
        [When(@"element with id ""(.*)"" is clickable")]
        [Then(@"element with id ""(.*)"" is clickable")]
        public void GivenElementWithIdIsClickable(string id)
        {
            _webDriver.WaitUntilElementIsClickable(By.Id(id));
        }

        [When(@"I click OK on the confirmation dialog")]
        [Then(@"I click OK on the confirmation dialog")]
        public void WhenIClickOKOnTheConfirmationDialog()
        {
            _webDriver.SwitchTo().Alert().Accept();
        }

        [Then(@"the expected exception is of type ""(.*)""")]
        public void ThenTheExpectedExceptionIsOfType(string expectedExceptionTypeName)
        {
            var lastExceptionTypeName = ScenarioContext.Current.ContainsKey("LastExceptionTypeName") ?
                (string)ScenarioContext.Current["LastExceptionTypeName"] :
                "";

            Assert.AreEqual(expectedExceptionTypeName, lastExceptionTypeName);
        }

        /// <summary>
        /// Helper method to catch exceptions and stash the exception type name into the scenario context
        /// </summary>
        /// <param name="action"></param>
        public void ExpectException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                ScenarioContext.Current["LastExceptionTypeName"] = exception.GetType().Name;
            }
        }

        [Given("The browser window is at desktop size")]
        public void GivenTheBrowserWindowIsDesktop()
        {
            _webDriver.Manage().Window.Maximize();

            Thread.Sleep(500);

            var originalSize = _webDriver.Manage().Window.Size;

            Thread.Sleep(500);

            _webDriver.Manage().Window.Size = new Size(_seleniumConfig.DesktopWindowWidth, originalSize.Height);
        }

        [Given("The browser window is at mobile size")]
        public void GivenTheBrowserWindowIsMobile()
        {
            _webDriver.Manage().Window.Maximize();

            Thread.Sleep(500);

            var originalSize = _webDriver.Manage().Window.Size;

            _webDriver.Manage().Window.Size = new Size(_seleniumConfig.MobileWindowWidth, originalSize.Height);

            Thread.Sleep(500);
        }
    }
}
