using System;

namespace Radio7.BDD
{
    public interface ISeleniumConfig
    {
        Uri BaseUrl { get; set; }

        WebDriverType WebDriverType { get; set; }
    }
}
