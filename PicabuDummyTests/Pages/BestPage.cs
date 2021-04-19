using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using PicabuDummyTests.Bases;
using PicabuDummyTests.Pages;
using PicabuDummyTests.Utils;
using WaitHelpers = SeleniumExtras.WaitHelpers;

namespace PicabuDummyTests 
{
    partial class BestPage : BasePage
    {
        private IWebElement PostsBottom => driver.FindElement(allRead);
        private IList<IWebElement> PostsRates => driver.FindElements(postsRates);
        private IList<IWebElement> PostsDateTimes => driver.FindElements(postsDateTimes);

        public BestPage(IWebDriver driver) :
            base(driver)
        {
            this.type = PagesCollection.BestPage;
        }

        public bool IsExpectedChecked(string expected)
        {
            OpenFilters();
            return driver.FindElement(checkedFilter).Text == expected;
        }

        public List<int> GetPostsRates()
        {
            var result = new List<int>();
            while (true)
            {
                try { if (PostsBottom.Displayed) break; }
                catch (NoSuchElementException ex) { }
                finally
                {
                    foreach (IWebElement post in PostsRates)
                    {
                        try
                        {
                            var rate = int.Parse(post.Text);
                            if (!result.Contains(rate)) result.Add(rate);
                        }
                        catch (StaleElementReferenceException ex) { continue; }
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(JsScriptsCollection.scrollDown);
                }
            }
            return result;
        }

        public bool IsPostsSorted(bool descOrder = false)
        {
            var rates = GetPostsRates();
            var comparable = rates.GetRange(0, rates.Count);
            comparable.Sort();
            if (descOrder) comparable.Reverse();
            return rates.SequenceEqual(comparable);
        }

        public List<DateTime> GetPostsDateTimes()
        {
            var result = new List<DateTime>();
            while (true)
            {
                try { if (PostsBottom.Displayed) break; }
                catch (NoSuchElementException ex) { }
                finally
                {
                    foreach (IWebElement dt in PostsDateTimes)
                    {
                        try
                        {
                            var dateTime = Convert.ToDateTime(dt.GetAttribute("datetime"));
                            if (!result.Contains(dateTime)) result.Add(dateTime);
                        }
                        catch (StaleElementReferenceException ex) { continue; }
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(JsScriptsCollection.scrollDown);
                }
            }
            result.Sort();
            return result;
        }

        public bool IsCalendarWidgetShown()
        {
            actions.MoveToElement(driver.FindElement(calendar)).Click().Build().Perform();
            return driver.FindElement(calendarHead).Displayed;
        }

        public bool IsShowPostsButtonActive()
        {
            MemoizeDatesFromCalendarField();
            int daysBefore = new Random().Next(10, 30);
            inputFromDate = inputFromDate.AddDays(-daysBefore);
            inputToDate = inputToDate.AddDays(1 - daysBefore);
            UpdateDatesInCalendarFields(driver.FindElement(calendarHead), inputFromDate, inputToDate);
            return driver.FindElement(showPostsButton).Displayed;
        }

        public bool IsAnimationDisplayed()
        {
            driver.FindElement(showPostsButton).Click();
            return driver.FindElement(animation).Displayed;
        }

        public bool IsPostsDatesInSelectedRange()
        {
            var dates = GetPostsDateTimes();
            return dates[0] >= inputFromDate.AddDays(-1) && dates[dates.Count - 1] < inputToDate.AddDays(1);
        }
    }
}
