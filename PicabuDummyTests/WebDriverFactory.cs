using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.Events;

namespace PicabuDummyTests
{
    class WebDriverFactory
    {
        public EventFiringWebDriver Create(BrowserType browserType)
        {
            IWebDriver wrappedDriver;
            var browserVersion = VersionHelper.GetLocalVersion(browserType);
            switch (browserType)
                {
                    case BrowserType.Chrome:
                        new DriverManager().SetUpDriver(new ChromeConfig(), browserVersion);
                        ChromeOptions chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--ignore-certificate-errors");
                        chromeOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                        wrappedDriver = new ChromeDriver(chromeOptions);
                        break; 
                    case BrowserType.Firefox:
                        new DriverManager().SetUpDriver(new FirefoxConfig(), browserVersion);
                        FirefoxOptions firefoxOptions = new FirefoxOptions { AcceptInsecureCertificates = true };
                        firefoxOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                        wrappedDriver = new FirefoxDriver(firefoxOptions);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("No such browser exists");
                }
            wrappedDriver.Manage().Cookies.DeleteAllCookies();
            wrappedDriver.Manage().Window.Maximize();
            wrappedDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Environment.ImplicitWait);
            return new EventFiringWebDriver(wrappedDriver);
        }
    }
}
