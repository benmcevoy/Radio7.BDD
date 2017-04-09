using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Radio7.BDD.Steps
{
    [Binding]
    public class ScrollingSteps
    {
        private readonly IWebDriver _webDriver;

        public ScrollingSteps(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [When(@"I scroll the page down ""(.*)"" pixels")]
        public void WhenIScrollThePageDownPixels(int value)
        {
            ScrollTo(value + 1);
        }

        [When(@"I scroll the page up ""(.*)"" pixels")]
        public void WhenIScrollThePageUpPixels(int value)
        {
            ScrollTo(GetPageYOffset() - (value + 1));
        }

        [When(@"I scroll to the top of the page")]
        public void WhenIScrollToTheTopOfThePage()
        {
            ScrollTo(0);
        }

        [When(@"the page height is less then ""(.*)"" pixel")]
        public void WhenThePageHeightIsLessThenPixel(int value)
        {
            var current = GetPageYOffset();

            // scroll a lot, hopefully to the end of the page
            ScrollTo(100000);

            Assert.IsTrue(GetPageYOffset() < value);

            // go back to where you were
            ScrollTo(current);
        }


        [Then(@"I am at the top of the page")]
        public void ThenIAmAtTheTopOfThePage()
        {
            var result = GetPageYOffset();
            // a little leeway in what is top...
            Assert.IsTrue(result < 10);
        }

        private int GetPageYOffset()
        {
            var js = JavScript();

            return Convert.ToInt32(js.ExecuteScript("return window.pageYOffset"));
        }

        private void ScrollTo(int value)
        {
            JavScript().ExecuteScript($"window.scrollTo(0, {value})");
        }

        private IJavaScriptExecutor JavScript()
        {
            var javaScriptExecutor = _webDriver as IJavaScriptExecutor;

            if (javaScriptExecutor == null)
                throw new NullReferenceException("instance of IWebDriver is not IJavaScriptExecutor");

            return javaScriptExecutor;
        }
    }
}