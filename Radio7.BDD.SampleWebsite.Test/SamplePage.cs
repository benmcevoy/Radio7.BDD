﻿using System;
using OpenQA.Selenium;

namespace Radio7.BDD.SampleWebsite.Test
{
    public class SamplePage : Page
    {
        public SamplePage(IWebDriver webDriver, ISeleniumConfig seleniumConfig)
            : base(webDriver, seleniumConfig)
        {
        }

        public override Uri Url
        {
            get { return new Uri("/SamplePage.html", UriKind.Relative); }
        }
    }
}