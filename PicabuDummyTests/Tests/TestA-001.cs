using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PicabuDummyTests.Pages;
using System;
using System.Threading;
using PicabuDummyTests.Bases;


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
            mainPage.NavigateAndCheckPageTab(mainPage.tab);
            Assert.IsTrue(mainPage.IsAuthorizationFormVisible(), "He отображена форма логина");
            Assert.IsTrue(mainPage.IsCommentOfTheDayVisible(), "Bиджет 'коментарий дня' нe отображен");
            Assert.IsFalse(mainPage.IsDateSelected(), "Дата выбрана");
        }

        [TestMethod, Priority(2), TestCategory("Dummy 002")]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestCase_A002()
        {
            bool descOrder = true;
            var bestPage = new BestPage(Driver);
            bestPage.NavigateAndCheckPageTab(bestPage.tab);
            Assert.IsTrue(bestPage.IsPostsSorted(descOrder));
        }

        [TestMethod, Priority(3), TestCategory("Dummy 003")]
        public void TestCase_A003()
        {
            var keyword = "За сегодня";
            var bestPage = new BestPage(Driver);
            bestPage.NavigateAndCheckPageTab(bestPage.tab);
            Assert.IsTrue(bestPage.IsExpectedChecked(keyword));
            Assert.IsTrue(bestPage.IsCalendarWidgetShown());
            Assert.IsTrue(bestPage.IsShowPostsButtonActive());
            Assert.IsTrue(bestPage.IsAnimationDisplayed());
            Assert.IsTrue(bestPage.IsPostsDatesInSelectedRange());
        }

        [TestMethod, Priority(4), TestCategory("Dummy 004")]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestCase_A004()
        {
            var desiredOption = "показывать";
            var expectedTabs = 4;
            var mainPage = new MainPage(Driver);
            mainPage.NavigateAndCheckPageTab(mainPage.tab);
            Assert.IsTrue(mainPage.IsDesiredOptionChosen(desiredOption));
            Assert.AreNotEqual(mainPage.IsShowListOpened(), 0);
            Assert.AreEqual(mainPage.OpenPostLinks(expectedTabs - 1), expectedTabs);
            Assert.IsTrue(mainPage.IsArticlesContainPreview(expectedTabs - 1));
            Assert.IsTrue(mainPage.IsArticlesDisplayedWithoutPreview(expectedTabs - 1));
        }
    }
}
