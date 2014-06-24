using System;
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

        [Given(@"I click the element labelled ""(.*)""")]
        [When(@"I click the element labelled ""(.*)""")]
        public void GivenIClickTheElementLabelled(string label)
        {
            var field = _webDriver.FindElement(By.XPath(string.Format("//*[text()='{0}']", label)));

            field.Click();
        }

        [Given(@"I enter ""(.*)"" in the element with id ""(.*)""")]
        [When(@"I enter ""(.*)"" in the element with id ""(.*)""")]
        public void GivenIEnterInTheElementWithId(string value, string id)
        {
            var field = _webDriver.FindElement(By.Id(id));

            field.SendKeys(value);
        }

        [Given(@"I click the element with id ""(.*)""")]
        [When(@"I click the element with id ""(.*)""")]
        public void GivenIClickTheElementWithId(string id)
        {
            var field = _webDriver.FindElement(By.Id(id));

            field.Click();
        }

        [Given(@"I clear the element with id ""(.*)""")]
        [When(@"I clear the element with id ""(.*)""")]
        public void GivenIClearTheElementWithId(string id)
        {
            var field = _webDriver.FindElement(By.Id(id));

            field.Clear();
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

        [Given(@"element with id ""(.*)"" is clickable")]
        [When(@"element with id ""(.*)"" is clickable")]
        [Then(@"element with id ""(.*)"" is clickable")]
        public void GivenElementWithIdIsClickable(string id)
        {
            _webDriver.WaitUntilElementToBeClickable(By.Id(id));
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
    }
}
