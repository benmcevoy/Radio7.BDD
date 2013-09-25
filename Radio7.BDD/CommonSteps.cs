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
            _webDriver.Navigate().GoToUrl(_seleniumConfig.BaseUrl + url);
        }

        [Given(@"I click the button with label ""(.*)""")]
        [When(@"I click the button with label ""(.*)""")]
        public void GivenIClickTheButtonWithLabel(string buttonLabel)
        {
            var field = _webDriver.FindElement(By.XPath(string.Format("//*[text()='{0}']", buttonLabel)));

            field.Click();
        }

        [Given(@"I click the element with id ""(.*)""")]
        [When(@"I click the element with id ""(.*)""")]
        public void GivenIClickTheElementWithLabel(string id)
        {
            var field = _webDriver.FindElement(By.Id(id));

            field.Click();
        }

        [Then(@"field with id ""(.*)"" has value ""(.*)""")]
        public void ThenFieldWithIdHasValue(string id, string value)
        {
            var field = _webDriver.FindElement(By.Id(id));
            Assert.AreEqual(value.ToLowerInvariant(), field.GetValue().ToLowerInvariant());
        }

        [Given(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        [When(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        public void GivenIWaitForToBePresentInElementWithId(string text, string id)
        {
            _webDriver.WaitUntilTextToBePresentInElementValue(By.Id(id), text);
        }
    }
}
