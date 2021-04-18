using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PicabuDummyTests
{
    public class BaseTest : ItemsBasis
    {
        private ScreenshotTaker ScreenshotTaker { get; set; }
        public EventFiringWebDriver Driver { get; private set; }
        public TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public void InitEnvironment()
        {
        }

        [TestInitialize]
        public void Setup()
        {
            LogStart(TestContext.TestName);
            //Reporter.AddTestCaseMetadataToHtmlReport(TestContext);
            Environment.Initialize();
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Chrome);
            //ScreenshotTaker = new ScreenshotTaker(Driver, TestContext);
        }

        [TestCleanup]
        public void Teardown()
        {
            try
            {
                //TakeScreenshotForTestFailure();
                if (Driver == null) return;
            }
            catch (Exception e)
            {
                string[] allMessages = new string[] { e.Source, e.StackTrace, e.InnerException.ToString(), e.Message };
                LogError(allMessages);
            }
            finally
            {
                Driver.Quit();
                Driver = null;
                LogFinish(TestContext.TestName);
            }
        }

        //private void TakeScreenshotForTestFailure()
        //{
        //    if (ScreenshotTaker != null)
        //    {
        //        ScreenshotTaker.CreateScreenshotIfTestFailed();
        //        Reporter.ReportTestOutcome(ScreenshotTaker.ScreenshotFilePath);
        //    }
        //    else Reporter.ReportTestOutcome("");
        //}
    }
}
