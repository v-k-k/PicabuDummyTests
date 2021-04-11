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
        [TestCategory("Dummy 001")]
        [TestMethod]
        public void TestCase_A001()
        {
            var mainPage = new MainPage(Driver);
            mainPage.NavigatePage();
            Assert.IsTrue(mainPage.IsHottestSelected(), "B������ '�������' �e �������");
            Assert.IsTrue(mainPage.IsAuthorizationFormVisible(), "He ���������� ����� ������");
            Assert.IsTrue(mainPage.IsCommentOfTheDayVisible(), "B����� '���������� ���' �e ���������");
            Assert.IsFalse(mainPage.IsDateSelected(), "���� �������");
        }

        [TestCategory("Dummy 002")]
        [TestMethod]
        public void TestCase_A002()
        {
            var mainPage = new MainPage(Driver);
            mainPage.NavigatePage();
            mainPage.GoToBest();
            Assert.IsTrue(mainPage.IsBestSelected());
            Assert.IsTrue(mainPage.IsPostsSorted(true));
        }

        [TestCategory("Dummy 003")]
        [TestMethod]
        public void TestCase_A003()
        {
            var mainPage = new MainPage(Driver);
            mainPage.NavigatePage();
            mainPage.GoToBest();
            Assert.IsTrue(mainPage.IsExpectedChecked("�� �������"));
            Assert.IsTrue(mainPage.IsCalendarWidgetShown());
            Assert.IsTrue(mainPage.IsShowPostsButtonActive());
        }
    }
}
