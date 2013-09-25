using System;
using System.Linq;
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

        [Given(@"I click the element with label ""(.*)""")]
        [When(@"I click the element with label ""(.*)""")]
        public void GivenIClickTheElementWithLabel(string label)
        {
            var field = _webDriver.FindElement(By.XPath(string.Format("//*[text()='{0}']", label)));

            field.Click();
        }

        [Given(@"I click the element with id ""(.*)""")]
        [When(@"I click the element with id ""(.*)""")]
        public void GivenIClickTheElementWithId(string id)
        {
            var field = _webDriver.FindElement(By.Id(id));

            field.Click();
        }

        [Given(@"the field with id ""(.*)"" has value ""(.*)""")]
        [When(@"the field with id ""(.*)"" has value ""(.*)""")]
        [Then(@"the field with id ""(.*)"" has value ""(.*)""")]
        public void GivenFieldWithIdHasValue(string id, string value)
        {
            var field = _webDriver.FindElement(By.Id(id));
            Assert.AreEqual(value.ToLowerInvariant(), field.GetValue().ToLowerInvariant());
        }

        [Given(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        [When(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        [Then(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        public void GivenIWaitForValueToBePresentInElementWithId(string text, string id)
        {
            _webDriver.WaitUntilTextToBePresentInElementValue(By.Id(id), text);
        }

        [Then(@"the expected exception is of type ""(.*)""")]
        public void ThenTheExpectedExceptionIsOfType(string expectedExceptionTypeName)
        {
            // TODO: promote to infrastructure (along with current page, is logged in etc).
            var lastExceptionTypeName = ScenarioContext.Current.ContainsKey("LastExceptionTypeName") ?
                (string)ScenarioContext.Current["LastExceptionTypeName"] : 
                "";

            Assert.AreEqual(expectedExceptionTypeName, lastExceptionTypeName);
        }
    }
}
