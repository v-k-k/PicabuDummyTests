using OpenQA.Selenium;
using PicabuDummyTests.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using PicabuDummyTests.Bases;


namespace PicabuDummyTests
{
    partial class MainPage : BasePage
    {
        public readonly By tab = By.XPath("//a[contains(text(),'Горячее')]/..");

        private readonly By authorizationHeader = By.XPath("//div[contains(text(),'Авторизация')]");

        private readonly By commentOfDay = By.XPath("//*[contains(text(),'Комментарий дня')]");

        private readonly By selectedOption = By.XPath("//option[@selected]");

        private readonly By selectList = By.XPath("//select");

        private readonly By loginField = By.XPath("..//form//input[@name='username']");

        private readonly By passwordField = By.XPath("..//form//input[@name='password']");

        private readonly By postsLinks = By.XPath("//h2/a");

        private readonly By articles = By.XPath("//article");

        private readonly By postTitle = By.CssSelector(".story__title-link");

        private readonly By articlePreview = By.XPath("div[2]/div[1]");

        private readonly By dropdownOption = By.XPath("option");
    }
}
