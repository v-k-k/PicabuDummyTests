using PicabuDummyTests.Bases;


namespace PicabuDummyTests.PageActions
{
    public class BestPageActions : BaseAction
    {
        public BestPageActions(PagesCollectionContainer pages) : base(pages) { }

        public void NavigateAndCheckBestPageTab()
        {
            var bestPage = Pages.GetBestPage();
            bestPage.NavigateAndCheckPageTab(bestPage.tab);
        }

        public void IsPostsSortedInDescOrder()
        {
            Pages.GetBestPage()
                .IsPostsSorted(true);
        }

        public void CheckTodaysOptionSelected()
        {
            Pages.GetBestPage()
                .OpenFilters()
                .IsExpectedChecked("За сегодня");
        }

        public void CheckCalendarWidget()
        {
            Pages.GetBestPage()
                .IsCalendarWidgetShown()
                .MemoizeDatesFromCalendarField()
                .SetRandomDatesRange()
                .UpdateDatesInCalendarFields()
                .IsShowPostsButtonActive();
        }

        public void CheckPostsDatesInSelectedRange()
        {
            Pages.GetBestPage()
                .IsAnimationDisplayed()
                .IsPostsDatesInSelectedRange();
        }
    }
}
