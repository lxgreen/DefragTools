using System;
using DefragEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefragEngineTests
{
    /// <summary>
    /// Summary description for BundleTests
    /// </summary>
    [TestClass]
    public class BundleTests
    {
        public BundleTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [TestMethod]
        public void BundleInstanceTest()
        {
            ToolBundle bundle = new ToolBundle("TestBundle");
            Assert.IsNotNull(bundle);
            Assert.AreNotEqual(bundle.ID, Guid.Empty);
            Assert.AreEqual(bundle.Name, "TestBundle");
            Assert.IsNotNull(bundle.Categories);
            Assert.AreEqual(bundle.Categories.Count, 0);
        }
    }
}