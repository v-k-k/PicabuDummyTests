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
            Assert.IsTrue(mainPage.IsHottestSelected(), "B������ '�������' �e �������");
            Assert.IsTrue(mainPage.IsAuthorizationFormVisible(), "He ���������� ����� ������");
            Assert.IsTrue(mainPage.IsCommentOfTheDayVisible(), "B����� '���������� ���' �e ���������");
            Assert.IsFalse(mainPage.IsDateSelected(), "���� �������");
        }

        [TestMethod, Priority(2), TestCategory("Dummy 002")]
        [ExpectedException(typeof(AssertFailedException))]
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
            Assert.IsTrue(mainPage.IsExpectedChecked("�� �������"));
            Assert.IsTrue(mainPage.IsCalendarWidgetShown());
            Assert.IsTrue(mainPage.IsShowPostsButtonActive());
            Assert.IsTrue(mainPage.IsAnimationDisplayed());
            Assert.IsTrue(mainPage.IsPostsDatesInSelectedRange());
        }

        [TestMethod, Priority(4), TestCategory("Dummy 004")]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestCase_A004()
        {
            var desiredOption = "����������";
            var expectedTabs = 4;
            var mainPage = new MainPage(Driver);
            mainPage.NavigatePage();
            Assert.IsTrue(mainPage.IsDesiredOptionChosen(desiredOption));
            Assert.AreNotEqual(mainPage.IsShowListOpened(), 0);
            Assert.AreEqual(mainPage.OpenPostLinks(expectedTabs - 1), expectedTabs);
            Assert.IsTrue(mainPage.IsArticlesContainPreview(expectedTabs - 1));
            Assert.IsTrue(mainPage.IsArticlesDisplayedWithoutPreview(expectedTabs - 1));
        }
    }
}
