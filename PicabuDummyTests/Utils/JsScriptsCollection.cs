using System;
using System.Collections.Generic;
using System.Text;

namespace PicabuDummyTests.Utils
{
    public static class JsScriptsCollection
    {
        public static readonly string getDateFrom = "return document.querySelector('input[data-type=\"from\"]').value";
        public static readonly string getDateTo = "return document.querySelector('input[data-type=\"to\"]').value";
        public static readonly string getContent = "return document.querySelector('input[data-type=\"{0}\"]').value";
        public static readonly string scrollDown = "window.scrollTo(0, document.body.scrollHeight)";
    }
}
