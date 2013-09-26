using System;

namespace Radio7.BDD.Config
{
    public interface ISeleniumConfig
    {
        Uri BaseUrl { get; set; }

        WebDriverType WebDriverType { get; set; }

        int ImplicitWaitMilliseconds { get; set; }
    }
}
