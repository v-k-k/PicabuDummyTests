using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace PicabuDummyTests.Pages
{
    class MainPage : BasePage
    {
        public MainPage(IWebDriver driver) :
            base(driver)
        {
            this.type = Pages.MainPage;              
        }
        
        [FindsBy(How = How.XPath, Using = "//span[@class='radio radio_checked']/following-sibling::span")]
        private IWebElement checkedFilter;

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Горячее')]/..")]
        private IWebElement hottest;

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Лучшее')]/..")]
        private IWebElement best;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Авторизация')]")]
        private IWebElement authorizationHeader;
        
        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Комментарий дня')]")]
        private IWebElement commentOfDay;
        
        [FindsBy(How = How.XPath, Using = "//p[@data-role='calendar']")]
        private IWebElement calendar;

        [FindsBy(How = How.XPath, Using = "//div[@class='calendar-head']")]
        private IWebElement calendarHead;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Показать посты')]")]
        private IWebElement showPostsButton;
        
        [FindsBy(How = How.XPath, Using = "//div[@class='stories-feed__spinner']/div[@class='player']/div[@class='player__overlay']")]
        private IWebElement animation;

        private IWebElement LoginField => authorizationHeader.FindElement(By.XPath("..//form//input[@name='username']"));
        private IWebElement PasswordField => authorizationHeader.FindElement(By.XPath("..//form//input[@name='password']"));

        private bool IsTabSelected(IWebElement tab)
        {
            return tab.GetAttribute("class").Contains("menu__item_current");
        }
        public bool IsHottestSelected()
        {
            return IsTabSelected(hottest);
        }

        public bool IsBestSelected()
        {
            return IsTabSelected(best);
        }

        public bool IsAuthorizationFormVisible()
        { 
            return authorizationHeader.Displayed && LoginField.Displayed && PasswordField.Displayed;
        }

        public bool IsCommentOfTheDayVisible()
        {
            return commentOfDay.Displayed;
        }

        public bool IsDateSelected()
        {
            DateTime tempDate;
            string[] tokens = driver.Url.Split('/');
            string potentialDate = tokens[tokens.Length - 1];
            bool validDate = DateTime.TryParseExact(potentialDate, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate);
            return validDate;
        }

        public void GoToBest()
        {
            best.Click();
        }

        public bool IsPostsSorted(bool descOrder=false)
        {
            var rates = GetPostsRates();
            var comparable = rates.GetRange(0, rates.Count);
            comparable.Sort();
            if (descOrder) comparable.Reverse();            
            return rates.SequenceEqual(comparable);
        }

        public bool IsExpectedChecked(string expected)
        {
            OpenFilters();
            return checkedFilter.Text.Equals(expected);
        }

        public bool IsCalendarWidgetShown()
        {
            actions.MoveToElement(calendar).Click().Build().Perform();
            return calendarHead.Displayed;
        }

        public bool IsShowPostsButtonActive()
        {
            string inputFromContent = (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('input[data-type=\"from\"]').value");
            string inputToContent = (string)((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('input[data-type=\"to\"]').value");
            inputFromDate = Convert.ToDateTime(inputFromContent);
            inputToDate = Convert.ToDateTime(inputToContent);
            int daysBefore = new Random().Next(10, 30);
            inputFromDate = inputFromDate.AddDays(-daysBefore);
            inputToDate = inputToDate.AddDays(5 - daysBefore);
            var dates = new List<DateTime> { inputFromDate, inputToDate };
            for (int i = 0; i < dates.Count; i++)
            {
                IWebElement field = calendarHead.FindElement(By.XPath($"div/div[{i + 1}]/input"));
                field.Clear();
                field.SendKeys(dates[i].ToString("dd/MM/yyyy").Replace(".", "/"));
                Thread.Sleep(1000);
            }
            return showPostsButton.Displayed;
        }

        public bool IsAnimationDisplayed()
        {
            showPostsButton.Click();
            return animation.Displayed;
        }

        public bool IsPostsDatesInSelectedRange()
        {
            var dates = GetPostsDateTimes();
            return dates[0] > inputFromDate && dates[dates.Count - 1] < inputToDate;
        }
    }
}
