using System;

namespace Radio7.BDD.Config
{
    public interface ISeleniumConfig
    {
        /// <summary>
        /// Base url for test to run against.
        /// </summary>
        Uri BaseUrl { get; set; }

        /// <summary>
        /// The web driver type, Firefox, Chrome, IE
        /// </summary>
        WebDriverType WebDriverType { get; set; }

        /// <summary>
        /// Specifies the amount of time the web driver should wait when searching for an 
        /// element if it is not immiediately present.
        /// </summary>
        /// <remarks>
        /// Setting the ImplicitWaitMilliseconds can be used to slow down the running of tests.
        /// </remarks>
        int ImplicitWaitMilliseconds { get; set; }

        /// <summary>
        /// The location of the browsers executable file.
        /// </summary>
        /// <remarks>
        /// This is useful using a portable browser, like GoogleChromePortable.  If the browser is 
        /// installed on the machine then you should be able to ignore this setting.
        /// </remarks>
        string BrowserPath { get; set; }

        /// <summary>
        /// The path to the directory containing the web driver executable file.
        /// </summary>
        /// <remarks>Note that this is just the path to the directory.</remarks>
        string DriverDirectory { get; set; }

        /// <summary>
        /// The width of the browser window when Desktop has been requested
        /// </summary>
        int DesktopWindowWidth { get; set; }

        /// <summary>
        /// The width of the browser window when Mobile has been requested
        /// </summary>
        int MobileWindowWidth { get; set; }
    }
}

