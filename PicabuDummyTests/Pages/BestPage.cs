using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PicabuDummyTests.Bases;
using PicabuDummyTests.Utils;


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
            this.type = RegisteredPages.BestPage;
        }

        public new BestPage OpenFilters() { return (BestPage)base.OpenFilters(); }

        public BestPage MemoizeDatesFromCalendarField() { return (BestPage)base.MemoizeDatesFromCalendarField(); }

        public BestPage UpdateDatesInCalendarFields()
        {
            return (BestPage)base.UpdateDatesInCalendarFields(driver.FindElement(calendarHead), inputFromDate, inputToDate);
        }

        public BestPage IsExpectedChecked(string expected)
        {
            Assert.IsTrue(driver.FindElement(checkedFilter).Text == expected, $"{expected} wasn't occur");
            return this;
        }

        private List<int> GetPostsRates()
        {
            LogInfo("Getting posts rates");
            var result = new List<int>();
            while (true)
            {
                try { if (PostsBottom.Displayed) break; }
                catch (NoSuchElementException ex) { LogException(ex); }
                finally
                {
                    foreach (IWebElement post in PostsRates)
                    {
                        try
                        {
                            var rate = int.Parse(post.Text);
                            if (!result.Contains(rate)) result.Add(rate);
                        }
                        catch (StaleElementReferenceException ex) 
                        {
                            LogException(ex);
                            continue; 
                        }
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(JsScriptsCollection.scrollDown);
                }
            }
            LogDebug("All posts rates collected");
            return result;
        }

        public BestPage IsPostsSorted(bool descOrder = false)
        {
            LogInfo("Check the posts rates sorting");
            var rates = GetPostsRates();
            var comparable = rates.GetRange(0, rates.Count);
            comparable.Sort();
            LogDebug($"Descent order is {descOrder}");
            if (descOrder) comparable.Reverse();
            Assert.IsTrue(rates.SequenceEqual(comparable), "Posts wasn't sorted correctly");
            LogDebug("Posts rates sorted correctly");
            return this;
        }

        private List<DateTime> GetPostsDateTimes()
        {
            LogInfo("Getting posts datetimes");
            var result = new List<DateTime>();
            while (true)
            {
                try { if (PostsBottom.Displayed) break; }
                catch (NoSuchElementException ex) { LogException(ex); }
                finally
                {
                    foreach (IWebElement dt in PostsDateTimes)
                    {
                        try
                        {
                            var dateTime = Convert.ToDateTime(dt.GetAttribute("datetime"));
                            if (!result.Contains(dateTime)) result.Add(dateTime);
                        }
                        catch (StaleElementReferenceException ex) 
                        { 
                            LogException(ex); 
                            continue; 
                        }
                    }
                    ((IJavaScriptExecutor)driver).ExecuteScript(JsScriptsCollection.scrollDown);
                }
            }
            result.Sort();
            LogDebug("All posts datetimes collected");
            return result;
        }

        public BestPage IsCalendarWidgetShown()
        {
            actions.MoveToElement(driver.FindElement(calendar)).Click().Build().Perform();
            Assert.IsTrue(driver.FindElement(calendarHead).Displayed, "Calendar widget wasn't shown");
            return this;
        }

        public BestPage IsShowPostsButtonActive()
        {
            Assert.IsTrue(driver.FindElement(showPostsButton).Displayed, "Show posts button wasn't active");
            return this;
        }

        public BestPage SetRandomDatesRange()
        {
            int daysBefore = new Random().Next(10, 30);
            inputFromDate = inputFromDate.AddDays(-daysBefore);
            inputToDate = inputToDate.AddDays(1 - daysBefore);
            return this;
        }

        public BestPage IsAnimationDisplayed()
        {
            driver.FindElement(showPostsButton).Click();
            Assert.IsTrue(driver.FindElement(animation).Displayed, "Animation wasn't displayed");
            return this;
        }

        public BestPage IsPostsDatesInSelectedRange()
        {
            var dates = GetPostsDateTimes();
            bool result = dates[0] >= inputFromDate.AddDays(-1) && dates[dates.Count - 1] < inputToDate.AddDays(1);
            Assert.IsTrue(result, "Posts dates wasn't in selected range");
            return this;
        }
    }
}
