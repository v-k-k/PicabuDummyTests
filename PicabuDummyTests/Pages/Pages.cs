using System;
using System.Collections.Generic;
using System.Text;
using SeleniumExtras.PageObjects;

namespace PicabuDummyTests.Pages
{
    public enum Pages
    {
        MainPage, BestPage, HottestPage
    }
    //public static class PageGenerator
    //{
    //    //constraint on a parameter   
    //    private static T GetPage<T>() where T : new()
    //    {
    //        var page = new T();
    //        PageFactory.InitElements(Browser.Driver, page);
    //        return page;
    //    }

    //    public static MainPage Login
    //    {
    //        get { return GetPage<MainPage>(); }
    //    }
    //}
}
