﻿using OpenQA.Selenium;
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
using PicabuDummyTests.Utils;
using PicabuDummyTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PicabuDummyTests.Bases
{
    class BasePage : ItemsBasis
    {
        protected WebDriverWait wait;
        protected IWebDriver driver;
        protected Actions actions;
        protected PagesCollection type;
        protected string[] dateFormats = { "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy" };
        protected DateTime inputFromDate;
        protected DateTime inputToDate;

        private readonly IFormatProvider invariantCulture = System.Globalization.CultureInfo.InvariantCulture;
        private readonly System.Globalization.DateTimeStyles dateTimeStyles = System.Globalization.DateTimeStyles.None;
        private By filtersButton = By.XPath("//span[contains(text(), 'Фильтры')]");

        public virtual bool IsSelected(By tab)
        {
            return driver.FindElement(tab).GetAttribute("class").Contains("menu__item_current");
        }

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            actions = new Actions(driver);
            wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(5));
        }

        public void NavigateAndCheckPageTab(By tab)
        {
            LogInfo($"Navigating to {Environment.BaseUrl}");
            driver.Navigate().GoToUrl(Environment.BaseUrl);
            LogDebug($"Navigated to {Environment.BaseUrl}");
            string pageName;
            string errorMessage;
            switch (type)
            {    
                case PagesCollection.BestPage:
                    driver.FindElement(tab).Click();
                    pageName = "best";
                    errorMessage = "Bкладка 'Лучшее' нe открыта";
                    break;         
                default:
                    pageName = "main";
                    errorMessage = "Bкладка 'Горячее' нe открыта";
                    break;
            }
            LogInfo($"Checking the {pageName} page tab");
            Assert.IsTrue(IsSelected(tab), errorMessage); 
            LogDebug($"Checked the {pageName} page tab");
        }

        protected void MemoizeDatesFromCalendarField(string dateFormat = null)
        {
            if (dateFormat == null) dateFormat = dateFormats[2];
            string inputFromContent = (string)((IJavaScriptExecutor)driver).ExecuteScript(JsScriptsCollection.getDateFrom);
            string inputToContent = (string)((IJavaScriptExecutor)driver).ExecuteScript(JsScriptsCollection.getDateTo);
            inputFromDate = DateTime.ParseExact(inputFromContent, dateFormat, invariantCulture, dateTimeStyles);
            inputToDate = DateTime.ParseExact(inputToContent, dateFormat, invariantCulture, dateTimeStyles);
        }

        protected void UpdateDatesInCalendarFields(IWebElement calendarHead, DateTime from, DateTime to, string dateFormat = null)
        {
            if (dateFormat == null) dateFormat = dateFormats[2];
            var dates = new List<DateHelper> { new DateHelper("from", from), new DateHelper("to", to) };
            for (int i = 0; i < dates.Count; i++)
            {
                IWebElement field = calendarHead.FindElement(By.XPath($"div/div[{i + 1}]/input"));
                while (true)
                {
                    var stringifiedDate = dates[i].date.ToString(dateFormat).Replace(".", "/");
                    var content = (string)((IJavaScriptExecutor)driver).ExecuteScript(string.Format(JsScriptsCollection.getContent, dates[i].name));
                    if (content == stringifiedDate) break;
                    field.Clear();
                    field.SendKeys(stringifiedDate);
                }
            }
        }

        protected void OpenFilters()
        {
            wait.Until(WaitHelpers.ExpectedConditions.ElementToBeClickable(filtersButton));
            driver.FindElement(filtersButton).Click();
        }

    }
}
