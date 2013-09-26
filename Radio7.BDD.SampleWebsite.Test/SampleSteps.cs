﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Radio7.BDD.Extensions;
using TechTalk.SpecFlow;

namespace Radio7.BDD.SampleWebsite.Test
{
    [Binding]
    public class SampleSteps
    {
        private readonly SamplePage _samplePage;
        private readonly CommonSteps _commonSteps;

        public SampleSteps(SamplePage samplePage, CommonSteps commonSteps)
        {
            _samplePage = samplePage;
            _commonSteps = commonSteps;
            _samplePage.NavigateTo();
        }

        [Given(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)"" to timeout")]
        [When(@"I wait for the value ""(.*)"" to be present in element with id ""(.*)"" to timeout")]
        public void GivenIWaitForTheValueToBePresentInElementWithIdToTimeout(string text, string id)
        {
            try
            {
                _commonSteps.GivenIWaitForValueToBePresentInElementWithId(text, id);
            }
            catch (Exception exception)
            {
                // TODO: promote to infrastructure
                ScenarioContext.Current["LastExceptionTypeName"] = exception.GetType().Name;
            }
        }

        [Given(@"I wait for an alert to be displayed")]
        [When(@"I wait for an alert to be displayed")]
        public void GivenIWaitForAnAlertToBeDisplayed()
        {
            _samplePage.WebDriver.WaitUntilAlertIsPresent();
        }

        [Given(@"an alert is displayed")]
        [Then(@"an alert is displayed")]
        public void GivenAnAlertIsDisplayed()
        {
            _samplePage.WebDriver.SwitchTo().Alert();
        }

        [Given(@"I wait for element with id ""(.*)"" and value ""(.*)"" to be invisible")]
        [When(@"I wait for element with id ""(.*)"" and value ""(.*)"" to be invisible")]
        public void GivenIWaitForElementWithIdAndValueToBeInvisible(string id, string text)
        {
            _samplePage.WebDriver.WaitUntilElementContainsTextIsInvisible(By.Id(id), text);
        }

        [Given(@"element with id ""(.*)"" is invisible")]
        [When(@"element with id ""(.*)"" is invisible")]
        [Then(@"element with id ""(.*)"" is invisible")]
        public void GivenElementWithIdIsInvisible(string id)
        {
            if (_samplePage.WebDriver.ElementExists(By.Id(id)))
            {
                Assert.AreEqual(false, _samplePage.WebDriver.FindElement(By.Id(id)).Displayed);
            }
        }
    }
}
