using PicabuDummyTests.Bases;
using System.Collections.Generic;


namespace PicabuDummyTests.PageActions
{
    class ActionsCollection
    {
        private Dictionary<RegisteredPages, BaseAction> collection;
        private PagesCollectionContainer pages;

        public Dictionary<RegisteredPages, BaseAction> Collection => collection;
        public PagesCollectionContainer Pages => pages;

        public ActionsCollection(PagesCollectionContainer pages)
        {
            collection = new Dictionary<RegisteredPages, BaseAction>();
            this.pages = pages;
        }

        internal MainPageActions GetMainPageActions()
        {
            return (MainPageActions)GetAction(RegisteredPages.MainPage);
        }

        internal BestPageActions GetBestPageActions()
        {
            return (BestPageActions)GetAction(RegisteredPages.BestPage);
        }

        private BaseAction GetAction(RegisteredPages type)
        {
            if (!collection.ContainsKey(type))
                switch (type)
                {
                    case RegisteredPages.MainPage:
                        collection.Add(RegisteredPages.MainPage, new MainPageActions(pages));
                        break;
                    case RegisteredPages.BestPage:
                        collection.Add(RegisteredPages.BestPage, new BestPageActions(pages));
                        break;
                }
            return collection[type];
        }

        public void ClearCollection()
        {
            collection.Clear();
        }
    }
}
