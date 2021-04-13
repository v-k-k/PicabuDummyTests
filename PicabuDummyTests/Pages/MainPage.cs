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
        
        [FindsBy(How = How.XPath, Using = "//option[@selected]")]
        private IWebElement selectedOption;
        
        [FindsBy(How = How.XPath, Using = "//select")]
        private IWebElement selectList;

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
            return checkedFilter.Text == expected;
        }

        public bool IsCalendarWidgetShown()
        {
            actions.MoveToElement(calendar).Click().Build().Perform();
            return calendarHead.Displayed;
        }


        public bool IsShowPostsButtonActive()
        {
            MemoizeDatesFromCalendarField();
            int daysBefore = new Random().Next(10, 30);
            inputFromDate = inputFromDate.AddDays(-daysBefore);
            inputToDate = inputToDate.AddDays(5 - daysBefore);
            UpdateDatesInCalendarFields(calendarHead, inputFromDate, inputToDate);
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
            return dates[0] >= inputFromDate.AddDays(-1) && dates[dates.Count - 1] <= inputToDate;
        }

        public bool IsDesiredOptionChosen(string optionName)
        {
            OpenFilters();
            return selectedOption.Text == optionName;
        }

        public int IsShowListOpened()
        {
            selectList.Click();
            var options = selectList.FindElements(By.XPath("option"));
            return options.Count;
        }

        public int OpenPostLinks(int maxLinks)
        {
            for (int i=0; i<maxLinks; i++)
            {
                Thread.Sleep(10000);
                //OpenPostLink();
                postsLinks[i].Click();
                driver.SwitchTo().Window(driver.WindowHandles.First());
            }
            return driver.WindowHandles.Count;
        }
    }
}
