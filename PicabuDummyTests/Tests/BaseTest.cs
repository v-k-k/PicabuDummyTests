using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicabuDummyTests
{
    public class BaseTest
    {
        public IWebDriver Driver { get; private set; }

        [AssemblyInitialize]
        public void InitEnvironment()
        {
        }

        [TestInitialize]
        public void Setup()
        {
            Environment.Initialize();
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Firefox);
        }

        [TestCleanup]
        public void Teardown()
        {
            Driver.Quit();
        }
    }
}
