using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using WaitHelpers = SeleniumExtras.WaitHelpers;
using System;
using NLog;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace PicabuDummyTests.Pages
{
    class BasePage
    {
        protected WebDriverWait wait;
        protected IWebDriver driver;
        protected Actions actions;
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
        
        [FindsBy(How = How.XPath, Using = "//h2/a")]
        protected IList<IWebElement> postsLinks;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            actions = new Actions(driver);
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
        }

        public void NavigatePage(string url = "")
        {
            switch (type)
            {             
                case Pages.MainPage:
                    driver.Manage().Window.Maximize();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Environment.ImplicitWait);
                    driver.Navigate().GoToUrl(Environment.BaseUrl);
                    logger.Info($"Navigated to {Environment.BaseUrl}");
                    break;
                default:
                    break;
            }
        }

        protected void MemoizeDatesFromCalendarField()
        {
            string inputFromContent = (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('input[data-type=\"from\"]').value");
            string inputToContent = (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('input[data-type=\"to\"]').value");
            inputFromDate = Convert.ToDateTime(inputFromContent);
            inputToDate = Convert.ToDateTime(inputToContent);
        }

        protected void UpdateDatesInCalendarFields(IWebElement calendarHead, DateTime from, DateTime to)
        {
            var dates = new List<DateTime> { from, to };
            for (int i = 0; i < dates.Count; i++)
            {
                IWebElement field = calendarHead.FindElement(By.XPath($"div/div[{i + 1}]/input"));
                field.Clear();
                field.SendKeys(dates[i].ToString("dd/MM/yyyy").Replace(".", "/"));
                Thread.Sleep(1000);
            }
        }

        public List<int> GetPostsRates()
        {
            var result = new List<int>();
            while (true)
            {
                try { if (postsBottom.Displayed) break; }
                catch (NoSuchElementException ex) { }
                finally
                {
                    foreach (IWebElement post in postsRates)
                    {
                        try
                        {
                            var rate = int.Parse(post.Text);
                            if (!result.Contains(rate)) result.Add(rate);
                        }
                        catch (StaleElementReferenceException ex) { continue; }
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                }
            }
            return result;
        }

        public void OpenFilters()
        {
            wait.Until(WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Фильтры')]")));
            filtersButton.Click();
        }

        public List<DateTime> GetPostsDateTimes()
        {
            var result = new List<DateTime>();
            while (true)
            {
                try { if (postsBottom.Displayed) break; }
                catch (NoSuchElementException ex) { }
                finally 
                {
                    foreach (IWebElement dt in postsDateTimes)
                    {
                        try
                        {
                            var dateTime = Convert.ToDateTime(dt.GetAttribute("datetime"));
                            if (!result.Contains(dateTime)) result.Add(dateTime);
                        }
                        catch (StaleElementReferenceException ex) { continue; }
                    }                         
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)"); 
                }
            }
            result.Sort();
            return result;
        }
        
        //protected void OpenPostLink()
        //{
        //    postsLinks[0].Click();
        //}
    }
}
