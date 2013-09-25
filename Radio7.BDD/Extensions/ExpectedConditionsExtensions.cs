using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Radio7.BDD.Extensions
{
    /// <summary>
    /// Canned <see cref="ExpectedConditions">ExpectedConditions</see> which are generally useful within webdriver
    /// tests.
    /// </summary>
    public static class ExpectedConditionsExtensions
    {
        /// <summary>
        /// An expectation for checking if the given text is present in the specified element
        /// </summary>
        public static Func<IWebDriver, bool> TextToBePresentInElement(By locator, string text)
        {
            return driver =>
            {
                try
                {
                    var elementText = driver.FindElement(locator).Text;
                    return elementText.Contains(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given text is present in the specified elements value attribute.
        /// </summary>
        public static Func<IWebDriver, bool> TextToBePresentInElementValue(By locator, string text)
        {
            return driver =>
            {
                try
                {
                    var elementText = driver.FindElement(locator).GetAttribute("value");

                    return elementText != null && elementText.Contains(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that an element is either invisible or not present on the DOM.
        /// </summary>
        public static Func<IWebDriver, bool> ElementIsInvisible(By locator)
        {
            return driver =>
            {
                try
                {
                    return !(driver.FindElement(locator).Displayed);
                }
                catch (NoSuchElementException)
                {
                    // Returns true because the element is not present in DOM. The
                    // try block checks if the element is present but is invisible.
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // Returns true because stale element reference implies that element
                    // is no longer visible.
                    return true;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that an element with text is either invisible or not present on the DOM.
        /// </summary>
        public static Func<IWebDriver, bool> ElementContainsTextIsInvisible(By locator, string text)
        {
            return driver =>
            {
                try
                {
                    return !driver.FindElement(locator).Text.Equals(text);
                }
                catch (NoSuchElementException)
                {
                    // Returns true because the element is not present in DOM. The
                    // try block checks if the element is present but is invisible.
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // Returns true because stale element reference implies that element
                    // is no longer visible.
                    return true;
                }
            };
        }

        /// <summary>
        /// An expectation for checking an element is visible and enabled such that you can click it.
        /// </summary>
        public static Func<IWebDriver, IWebElement> ElementToBeClickable(By locator)
        {
            return driver =>
            {
                var element = ExpectedConditions.ElementIsVisible(locator).Invoke(driver);

                try
                {
                    if (element != null && element.Enabled)
                    {
                        return element;
                    }

                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// Wait until an element is no longer attached to the DOM.
        /// </summary>
        //public static Func<IWebDriver, bool> ElementIsStale(IWebElement element)
        //{
        //    return driver =>
        //    {
        //        try
        //        {
        //            // Calling any method forces a staleness check
        //            // TODO: I suspect this will be optimised away by release mode?
        //            element.ToString();
        //            return false;
        //        }
        //        catch (StaleElementReferenceException)
        //        {
        //            return true;
        //        }
        //    };
        //}

        /// <summary>
        /// Wrapper for a condition, which allows for elements to update by redrawing.
        /// </summary>
        /// <remarks>
        /// This works around the problem of conditions which have two parts: find an
        /// element and then check for some condition on it. For these conditions it is
        /// possible that an element is located and then subsequently it is redrawn on
        /// the client. When this happens a <see cref="StaleElementReferenceException">StaleElementReferenceException</see> is
        /// thrown when the second part of the condition is checked.
        /// </remarks>
        //public static Func<IWebDriver, bool> ElementIsRefreshed(By locator)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// An expectation for checking if the given element is selected.
        /// </summary>
        public static Func<IWebDriver, bool> ElementToBeSelected(IWebElement element)
        {
            return ElementSelectionStateToBe(element, true);
        }

        /// <summary>
        /// An expectation for checking if the given element is selected.
        /// </summary>
        public static Func<IWebDriver, bool> ElementToBeSelected(By locator)
        {
            return ElementSelectionStateToBe(locator, true);
        }

        /// <summary>
        /// An expectation for checking if the given element is selected.
        /// </summary>
        public static Func<IWebDriver, bool> ElementSelectionStateToBe(IWebElement element, bool selected)
        {
            return driver => element.Selected == selected;
        }

        /// <summary>
        /// An expectation for checking if the given element is selected.
        /// </summary>
        public static Func<IWebDriver, bool> ElementSelectionStateToBe(By locator, bool selected)
        {
            return driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);

                    return element != null && element.Selected == selected;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, IAlert> AlertIsPresent()
        {
            return driver =>
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            };
        }
    }
}
