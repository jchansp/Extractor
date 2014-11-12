using Extractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uri = System.Uri;

namespace UnitTest
{
    [TestClass]
    public class ProgramUnitTest
    {
        [TestMethod]
        public void FindArticlesTest()
        {
            Program.FindArticles(
                new Uri(
                    "http://www.chollos24.com/cajas-y-barebones/fuente-de-alimentacion-csp-x1200cs-1200w-modular-club-3d"));
        }
    }
}