using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Radio7.BDD.Extensions
{
    public static class WebDriverExtensions
    {
        internal static void NavigateTo(this IWebDriver webDriver, Uri url, Uri baseUrl)
        {
            if (url.IsAbsoluteUri)
            {
                if (webDriver.Url.Equals(url.ToString())) return;
                webDriver.Navigate().GoToUrl(url);
                return;
            }

            if (webDriver.Url.Equals(new Uri(baseUrl, url).ToString())) return;

            webDriver.Navigate().GoToUrl(new Uri(baseUrl, url));
        }

        public static bool ElementExists(this IWebDriver webDriver, By by)
        {
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IWebElement WaitUntilElementExists(this IWebDriver webDriver, By by, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditions.ElementExists(by));
        }

        public static IWebElement WaitUntilElementIsVisible(this IWebDriver webDriver, By by, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }

        public static bool WaitUntilTitleContains(this IWebDriver webDriver, string title, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditions.TitleContains(title));
        }

        public static bool WaitUntilTitleIs(this IWebDriver webDriver, string title, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditions.TitleIs(title));
        }

        public static bool WaitUntilElementContainsTextIsInvisible(this IWebDriver webDriver, By by, string text, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.ElementContainsTextIsInvisible(by, text));
        }

        public static bool WaitUntilElementIsInvisible(this IWebDriver webDriver, By by, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.ElementIsInvisible(by));
        }

        public static bool WaitUntilElementSelectionStateIs(this IWebDriver webDriver, By by, bool selected, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.ElementSelectionStateToBe(by, selected));
        }

        public static bool WaitUntilElementSelectionStateIs(this IWebDriver webDriver, IWebElement webElement, bool selected, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.ElementSelectionStateToBe(webElement, selected));
        }

        public static bool WaitUntilElementIsSelected(this IWebDriver webDriver, By by, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.ElementToBeSelected(by));
        }

        public static bool WaitUntilElementIsSelected(this IWebDriver webDriver, IWebElement webElement, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.ElementToBeSelected(webElement));
        }

        public static bool WaitUntilTextIsPresentInElement(this IWebDriver webDriver, By by, string text, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.TextToBePresentInElement(by, text));
        }

        public static bool WaitUntilTextIsPresentInElementValue(this IWebDriver webDriver, By by, string text, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.TextToBePresentInElementValue(by, text));
        }

        public static IAlert WaitUntilAlertIsPresent(this IWebDriver webDriver, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.AlertIsPresent());
        }

        public static IWebElement WaitUntilElementIsClickable(this IWebDriver webDriver, By by, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutInSeconds));

            return wait.Until(ExpectedConditionsExtensions.ElementToBeClickable(by));
        }
    }
}
