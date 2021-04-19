using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using WaitHelpers = SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using PicabuDummyTests.Bases;
using PicabuDummyTests.Pages;

namespace PicabuDummyTests
{
    partial class MainPage : BasePage
    {
        private IWebElement LoginField => driver.FindElement(authorizationHeader).FindElement(loginField);
        private IWebElement PasswordField => driver.FindElement(authorizationHeader).FindElement(passwordField);
        private IList<IWebElement> PostsLinks => driver.FindElements(postsLinks);
        private IList<IWebElement> Articles => driver.FindElements(articles);

        public MainPage(IWebDriver driver) :
            base(driver)
        {
            this.type = PagesCollection.MainPage;
        }

        public bool IsAuthorizationFormVisible()
        { 
            return driver.FindElement(authorizationHeader).Displayed && LoginField.Displayed && PasswordField.Displayed;
        }

        public bool IsCommentOfTheDayVisible()
        {
            return driver.FindElement(commentOfDay).Displayed;
        }

        public bool IsDateSelected()
        {
            DateTime tempDate;
            string[] tokens = driver.Url.Split('/');
            string potentialDate = tokens[tokens.Length - 1];
            bool validDate = DateTime.TryParseExact(potentialDate, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate);
            return validDate;
        }

        public bool IsDesiredOptionChosen(string optionName)
        {
            OpenFilters();
            return driver.FindElement(selectedOption).Text == optionName;
        }

        public int IsShowListOpened()
        {
            driver.FindElement(selectList).Click();
            var options = driver.FindElement(selectList).FindElements(dropdownOption);
            return options.Count;
        }

        public int OpenPostLinks(int maxLinks)
        {
            for (int i=0; i<maxLinks; i++)
            {
                PostsLinks[i].Click();
                var windows = driver.WindowHandles;
                driver.SwitchTo().Window(windows.Last());
                wait.Until(WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(postTitle));
                driver.SwitchTo().Window(windows.First());
            }
            return driver.WindowHandles.Count;
        }

        public bool IsArticlesContainPreview(int maxArticles)
        {
            driver.Navigate().Refresh();
            var targetArticles = Articles.Take(maxArticles);
            foreach (IWebElement article in targetArticles)
            {
                if (!article.FindElement(articlePreview).GetAttribute("class").Contains("content")) return false;
            }
            return true;
        }

        public void RollUpPosts()
        {
            OpenFilters();
            var selectElement = new SelectElement(driver.FindElement(selectList));
            selectElement.SelectByText("сворачивать");
        }

        public bool IsArticlesDisplayedWithoutPreview(int maxArticles)
        {
            RollUpPosts();
            var targetArticles = Articles.Take(maxArticles);
            foreach (IWebElement article in targetArticles)
            {
                if (article.FindElement(articlePreview).GetAttribute("class").Contains("content")) return false;
            }
            return true;
        }
    }
}
