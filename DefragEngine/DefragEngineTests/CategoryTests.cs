using System;
using DefragEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefragEngineTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CategoryTests
    {
        public CategoryTests()
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
        public void CategoryInstanceTests()
        {
            ToolCategory category = new ToolCategory("SysInternals");
            Assert.IsNotNull(category);
            Assert.AreNotEqual(category.ID, Guid.Empty);
            Assert.AreEqual(category.Name, "SysInternals");
            Assert.IsNotNull(category.Tools);
            Assert.AreEqual(category.Tools.Count, 0);
        }
    }
}