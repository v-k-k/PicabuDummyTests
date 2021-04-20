using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WaitHelpers = SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PicabuDummyTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            this.type = RegisteredPages.MainPage;
        }

        public new MainPage OpenFilters() { return (MainPage)base.OpenFilters(); }

        public MainPage IsAuthorizationFormVisible()
        {
            LogInfo("Checking the visibility of authorization form");
            bool result = driver.FindElement(authorizationHeader).Displayed && LoginField.Displayed && PasswordField.Displayed;
            Assert.IsTrue(result, "He отображена форма логина");
            LogDebug("Authorization passed");
            return this;
        }

        public MainPage IsCommentOfTheDayVisible()
        {
            LogInfo("Checking the visibility of day comment");
            bool result = driver.FindElement(commentOfDay).Displayed;
            Assert.IsTrue(result, "Bиджет 'коментарий дня' нe отображен");
            LogDebug("Comment visible");
            return this;
        }

        public MainPage IsDateSelected()
        {
            LogInfo("Checking if date selected");
            DateTime tempDate;
            string[] tokens = driver.Url.Split('/');
            string potentialDate = tokens[tokens.Length - 1];
            bool validDate = DateTime.TryParseExact(potentialDate, dateFormats, DateTimeFormatInfo.InvariantInfo, dateTimeStyles, out tempDate);
            Assert.IsFalse(validDate, "Дата выбрана");
            LogDebug("Date was selected");
            return this;
        }

        public MainPage IsDesiredOptionChosen(string optionName)
        {
            LogInfo($"Checking if {optionName} selected");
            Assert.IsTrue(driver.FindElement(selectedOption).Text == optionName, $"Selected option doesn't match the {optionName}");
            LogDebug($"{optionName} was selected");
            return this;
        }

        public MainPage IsShowListOpened()
        {
            LogInfo("Checking if show list opened");
            driver.FindElement(selectList).Click();
            var options = driver.FindElement(selectList).FindElements(dropdownOption);
            Assert.AreNotEqual(options.Count, 0);
            LogDebug("Show list was selected");
            return this;
        }

        public MainPage OpenPostLinks(int maxLinks)
        {
            LogInfo($"Checking first {maxLinks} posts links");
            for (int i=0; i<maxLinks; i++)
            {
                PostsLinks[i].Click();
                var windows = driver.WindowHandles;
                driver.SwitchTo().Window(windows.Last());
                wait.Until(WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(postTitle));
                driver.SwitchTo().Window(windows.First());
            }
            Assert.AreEqual(driver.WindowHandles.Count, maxLinks + 1);
            LogDebug($"First {maxLinks} posts links occurred");
            return this;
        }

        public MainPage IsArticlesContainPreview(int maxArticles)
        {
            LogInfo($"Checking if {maxArticles} articles contain preview");
            driver.Navigate().Refresh();
            var targetArticles = Articles.Take(maxArticles);
            foreach (IWebElement article in targetArticles)
            {
                Assert.IsTrue(!article.FindElement(articlePreview).GetAttribute("class").Contains("content"), "No preview for article");
            }
            LogDebug($"{maxArticles} articles contain preview");
            return this;
        }

        public MainPage RollUpPosts()
        {
            LogDebug("Rolling up posts");
            var selectElement = new SelectElement(driver.FindElement(selectList));
            selectElement.SelectByText("сворачивать");
            return this;
        }

        public MainPage IsArticlesDisplayedWithoutPreview(int maxArticles)
        {
            LogInfo($"Checking if {maxArticles} articles not contain preview");
            var targetArticles = Articles.Take(maxArticles);
            foreach (IWebElement article in targetArticles)
            {
                Assert.IsTrue(article.FindElement(articlePreview).GetAttribute("class").Contains("content"), "Preview displayed for article");
            }
            LogDebug($"{maxArticles} articles not contain preview");
            return this;
        }
    }
}
