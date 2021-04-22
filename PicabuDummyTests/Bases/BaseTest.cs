using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.Events;
using PicabuDummyTests.PageActions;
using System;


namespace PicabuDummyTests.Bases
{
    public class BaseTest : ItemsBasis
    {
        PagesCollectionContainer pages;
        ActionsCollection pageActions;
        public EventFiringWebDriver Driver { get; private set; }
        public TestContext TestContext { get; set; }

        internal ActionsCollection PageActions => pageActions;

        [AssemblyInitialize]
        public void InitEnvironment()
        {
        }

        [TestInitialize]
        public void Setup()
        {
            LogStart(TestContext.TestName);
            Environment.Initialize();
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Chrome);
            pages = new PagesCollectionContainer(Driver);
            pageActions = new ActionsCollection(pages);
        }

        [TestCleanup]
        public void Teardown()
        {
            try
            {
                pageActions.ClearCollection();
                pages.ClearCollection();
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
    }
}
