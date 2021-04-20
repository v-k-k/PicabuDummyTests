using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            PageActions.GetMainPageActions().NavigateAndCheckMainPageTab();
            PageActions.GetMainPageActions().CheckGeneralElements();
        }

        [TestMethod, Priority(2), TestCategory("Dummy 002")]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestCase_A002()
        {
            PageActions.GetBestPageActions().NavigateAndCheckBestPageTab();
            PageActions.GetBestPageActions().IsPostsSortedInDescOrder();
        }

        [TestMethod, Priority(3), TestCategory("Dummy 003")]
        public void TestCase_A003()
        {
            PageActions.GetBestPageActions().NavigateAndCheckBestPageTab();
            PageActions.GetBestPageActions().CheckTodaysOptionSelected();
            PageActions.GetBestPageActions().CheckCalendarWidget();
            PageActions.GetBestPageActions().CheckPostsDatesInSelectedRange();
        }

        [TestMethod, Priority(4), TestCategory("Dummy 004")]
        [ExpectedException(typeof(AssertFailedException))]
        public void TestCase_A004()
        {
            var desiredOption = "показывать";
            var expectedTabs = 3;
            PageActions.GetMainPageActions().NavigateAndCheckMainPageTab();
            PageActions.GetMainPageActions().CheckDesiredOptionsInShowListDropdown(desiredOption);
            PageActions.GetMainPageActions().OpenPostsAndCheckPreviews(expectedTabs);
        }
    }
}
