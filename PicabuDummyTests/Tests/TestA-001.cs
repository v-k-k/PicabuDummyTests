using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PicabuDummyTests.Pages;
using System;
using System.Threading;

namespace PicabuDummyTests
{
    [TestCategory("Dummy test suite A")]
    [TestClass]
    public class TestSuiteA : BaseTest
    {
        [TestMethod, Priority(1), TestCategory("Dummy 001")]
        public void TestCase_A001()
        {
            var mainPage = new MainPage(Driver);
            mainPage.NavigatePage();
            Assert.IsTrue(mainPage.IsHottestSelected(), "Bкладка 'Горячее' нe открыта");
            Assert.IsTrue(mainPage.IsAuthorizationFormVisible(), "He отображена форма логина");
            Assert.IsTrue(mainPage.IsCommentOfTheDayVisible(), "Bиджет 'коментарий дня' нe отображен");
            Assert.IsFalse(mainPage.IsDateSelected(), "Дата выбрана");
        }

        [TestMethod, Priority(2), TestCategory("Dummy 002")]
        public void TestCase_A002()
        {
            bool descOrder = true;
            var mainPage = new MainPage(Driver);
            mainPage.NavigatePage();
            mainPage.GoToBest();
            Assert.IsTrue(mainPage.IsBestSelected());
            Assert.IsTrue(mainPage.IsPostsSorted(descOrder));
        }

        [TestMethod, Priority(3), TestCategory("Dummy 003")]
        public void TestCase_A003()
        {
            var mainPage = new MainPage(Driver);
            mainPage.NavigatePage();
            mainPage.GoToBest();
            Assert.IsTrue(mainPage.IsExpectedChecked("За сегодня"));
            Assert.IsTrue(mainPage.IsCalendarWidgetShown());
            Assert.IsTrue(mainPage.IsShowPostsButtonActive());
            Assert.IsTrue(mainPage.IsAnimationDisplayed());
            Assert.IsTrue(mainPage.IsPostsDatesInSelectedRange());
        }
    }
}
