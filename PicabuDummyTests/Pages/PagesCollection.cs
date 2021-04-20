using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using PicabuDummyTests.Bases;

namespace PicabuDummyTests
{    
    public class PagesCollectionContainer
    {
        private Dictionary<RegisteredPages, BasePage> collection;
        private IWebDriver driver;

        internal Dictionary<RegisteredPages, BasePage> Collection => collection;
        public IWebDriver Driver => driver;

        public PagesCollectionContainer(IWebDriver driver)
        {
            collection = new Dictionary<RegisteredPages, BasePage>();
            this.driver = driver;
        }

        internal MainPage GetMainPage()
        {
            return (MainPage)GetPage(RegisteredPages.MainPage);
        }

        internal BestPage GetBestPage()
        {
            return (BestPage)GetPage(RegisteredPages.BestPage);
        }

        private BasePage GetPage(RegisteredPages type)
        {
            if (!collection.ContainsKey(type))
                switch (type)
                {
                    case RegisteredPages.MainPage:
                        collection.Add(RegisteredPages.MainPage, new MainPage(driver));
                        break;
                    case RegisteredPages.BestPage:
                        collection.Add(RegisteredPages.BestPage, new BestPage(driver));
                        break;
                }
            return collection[type];
        }

        public void ClearCollection()
        {
            collection.Clear();
        }
    } 
}
