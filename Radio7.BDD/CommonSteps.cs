﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Radio7.BDD
{
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
        public void GivenIHaveNavigatedTo(string url)
        {
            _webDriver.Navigate().GoToUrl(_seleniumConfig.BaseUrl + url);
        }

        [Given(@"I click the button with label ""(.*)""")]
        public void GivenIClickTheButtonWithLabel(string buttonLabel)
        {
            var field = _webDriver.FindElement(By.XPath(string.Format("//*[text()='{0}']", buttonLabel)));

            field.Click();
        }

        [Given(@"I click the element with id ""(.*)""")]
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

        [When(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)""")]
        public void WhenIWaitForToBePresentInElementWithId(string text, string id)
        {
            _webDriver.WaitUntilTextToBePresentInElementValue(By.Id(id), text);
        }
    }
}