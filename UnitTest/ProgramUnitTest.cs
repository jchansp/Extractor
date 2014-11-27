using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Extractor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Uri = System.Uri;

namespace UnitTest
{
    /// <summary>
    ///     Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProgramUnitTest
    {
        public static DatabaseEntities DatabaseEntities;

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void MyTestInitialize()
        {
            DatabaseEntities = new DatabaseEntities();
        }

        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FindArticlesTest()
        {
            Program.FindArticles(
                new Uri(
                    "http://www.chollos24.com/cajas-y-barebones/fuente-de-alimentacion-csp-x1200cs-1200w-modular-club-3d"));
        }

        [TestMethod]
        public void AlreadyHaveUriTest()
        {
            var context = new TestContext();
            context.Blogs.Add(new Blog { Name = "BBB" });
            context.Blogs.Add(new Blog { Name = "ZZZ" });
            context.Blogs.Add(new Blog { Name = "AAA" });

            var service = new BlogService(context);
            var blogs = service.AlreadyHaveUriTest();

            Assert.AreEqual(3, blogs.Count);
            Assert.AreEqual("AAA", blogs[0].Name);
            Assert.AreEqual("BBB", blogs[1].Name);
            Assert.AreEqual("ZZZ", blogs[2].Name); 
        }
    }
}