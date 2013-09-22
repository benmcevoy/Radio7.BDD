using System;
using System.Configuration;

namespace Radio7.BDD
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
    }
}
