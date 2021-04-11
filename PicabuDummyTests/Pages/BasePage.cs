using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using WaitHelpers = SeleniumExtras.WaitHelpers;
using System;
using NLog;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PicabuDummyTests.Pages
{
    class BasePage
    {
        protected WebDriverWait wait;
        protected IWebDriver driver;
        protected Pages type;
        protected string[] dateFormats = { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" };
        protected DateTime inputFromDate;
        protected DateTime inputToDate;
        public static Logger logger = LogManager.GetCurrentClassLogger();

        protected IWebElement postsBottom => driver.FindElement(By.XPath("//section[contains(text(),'Отличная работа, все прочитано!')]"));
                
        [FindsBy(How = How.XPath, Using = "//div[@class='story__rating-count']")]
        protected IList<IWebElement> postsRates;
        
        [FindsBy(How = How.XPath, Using = "//div[@class='story__user-info']//time")]
        protected IList<IWebElement> postsDateTimes;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(), 'Фильтры')]")]
        protected IWebElement filtersButton;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
        }

        public void NavigatePage(string url = "")
        {
            switch (type)
            {
                case Pages.MainPage:
                    driver.Navigate().GoToUrl(Environment.BaseUrl);
                    logger.Info($"Navigated to {Environment.BaseUrl}");
                    break;
                default:
                    break;
            }
        }

        protected void SrollToBottom()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            while (true)
            {
                try { if (postsBottom.Displayed) break; }
                catch (NoSuchElementException ex) { }
                finally { ((IJavaScriptExecutor) driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)"); }
            }
        }

        public List<string> GetPostsRates()
        {
            SrollToBottom();
            var result = new List<string>();
            foreach(IWebElement post in postsRates) result.Add(post.Text);
            return result;
        }

        public void OpenFilters()
        {
            wait.Until(WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Фильтры')]")));
            filtersButton.Click();
        }

        public List<string> GetPostsDateTimes()
        {
            SrollToBottom();
            var result = new List<string>();
            foreach (IWebElement dateTime in postsDateTimes) result.Add(dateTime.GetAttribute("datetime"));
            result.Sort();
            return result;
        }
    }
}
