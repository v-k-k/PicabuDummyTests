using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace PicabuDummyTests
{
    class WebDriverFactory
    {
        public IWebDriver Create(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--ignore-certificate-errors");
                    chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    return new ChromeDriver(chromeOptions);
                case BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    FirefoxOptions firefoxOptions = new FirefoxOptions
                    {
                        AcceptInsecureCertificates = true
                    };
                    firefoxOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    return new FirefoxDriver(firefoxOptions);
                default:
                    throw new ArgumentOutOfRangeException("No such browser exists");
            }
        }
    }
}
