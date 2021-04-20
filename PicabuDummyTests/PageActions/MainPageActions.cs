using PicabuDummyTests.Bases;


namespace PicabuDummyTests.PageActions
{
    public class MainPageActions : BaseAction
    {
        public MainPageActions(PagesCollectionContainer pages) : base(pages) { }

        public void NavigateAndCheckMainPageTab()
        {
            var mainPage = Pages.GetMainPage();
            mainPage.NavigateAndCheckPageTab(mainPage.tab);
        }

        public void CheckGeneralElements()
        {
            Pages.GetMainPage()
                .IsAuthorizationFormVisible()
                .IsCommentOfTheDayVisible()
                .IsDateSelected();
        }

        public void CheckDesiredOptionsInShowListDropdown(string optionName)
        {
            Pages.GetMainPage()
                .OpenFilters()
                .IsDesiredOptionChosen(optionName)
                .IsShowListOpened();
        }

        public void OpenPostsAndCheckPreviews(int maxPostsLinks)
        {
            Pages.GetMainPage()
                .OpenPostLinks(maxPostsLinks)
                .IsArticlesContainPreview(maxPostsLinks)
                .OpenFilters()
                .RollUpPosts()
                .IsArticlesDisplayedWithoutPreview(maxPostsLinks);
        }
    }
}
