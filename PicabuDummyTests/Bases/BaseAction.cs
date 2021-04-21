

namespace PicabuDummyTests.Bases
{
    public class BaseAction
    {
        PagesCollectionContainer pages;

        public PagesCollectionContainer Pages => pages;

        internal BaseAction(PagesCollectionContainer pages)
        {
            this.pages = pages;
        }
    }
}
