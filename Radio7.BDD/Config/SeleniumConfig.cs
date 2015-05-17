using System;
using System.Configuration;

namespace Radio7.BDD.Config
{
    public class SeleniumConfig : ConfigurationSection, ISeleniumConfig
    {
        [ConfigurationProperty("baseUrl")]
        public Uri BaseUrl
        {
            get { return (Uri)this["baseUrl"]; }
            set { this["baseUrl"] = value; }
        }

        [ConfigurationProperty("webDriverType", DefaultValue = WebDriverType.Firefox)]
        public WebDriverType WebDriverType
        {
            get { return (WebDriverType)this["webDriverType"]; }
            set { this["webDriverType"] = value; }
        }

        [ConfigurationProperty("implicitWaitMilliseconds", DefaultValue = 0)]
        public int ImplicitWaitMilliseconds
        {
            get { return (int)this["implicitWaitMilliseconds"]; }
            set { this["implicitWaitMilliseconds"] = value; }
        }

        [ConfigurationProperty("browserPath", DefaultValue = null)]
        public string BrowserPath
        {
            get { return (string)this["browserPath"]; }
            set { this["browserPath"] = value; }
        }

        [ConfigurationProperty("driverDirectory", DefaultValue = null)]
        public string DriverDirectory
        {
            get { return (string)this["driverDirectory"]; }
            set { this["driverDirectory"] = value; }
        }

        [ConfigurationProperty("desktopWindowWidth", DefaultValue = 1244)]
        public int DesktopWindowWidth
        {
            get { return (int)this["desktopWindowWidth"]; }
            set { this["desktopWindowWidth"] = value; }
        }

        [ConfigurationProperty("mobileWindowWidth", DefaultValue = 480)]
        public int MobileWindowWidth
        {
            get { return (int)this["mobileWindowWidth"]; }
            set { this["mobileWindowWidth"] = value; }
        }
    }
}
