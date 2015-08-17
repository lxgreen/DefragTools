using System;
using System.Linq;
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
            ToolBundle bundle = new ToolBundle("TestBundle", "0.0.0.1");
            Assert.IsNotNull(bundle);
            Assert.AreNotEqual(bundle.ID, Guid.Empty);
            Assert.AreEqual(bundle.Name, "TestBundle");
            Assert.AreEqual(bundle.Version, "0.0.0.1");
            Assert.IsNotNull(bundle.Categories);
            Assert.AreEqual(bundle.Categories.Count, 0);
        }

        [TestMethod]
        public void BundleCategoriesAddTest()
        {
            ToolBundle bundle = new ToolBundle("TestBundle", "0.0.0.1");
            Assert.AreEqual(bundle.Categories.Count, 0);
            var testCategory = new ToolCategory("Test", "0.0.0.1");
            bundle.Categories.Add(testCategory);
            Assert.AreEqual(bundle.Categories.Count, 1);
            var categoryToAdd = new ToolCategory("Test2", "0.0.0.1");
            bundle.Categories.Add(categoryToAdd);
            Assert.AreEqual(bundle.Categories.Count, 2);
        }

        [TestMethod]
        public void BundleCategoriesAddRangeTest()
        {
            var testCategory = new ToolCategory("Test", "0.0.0.1");
            var categoryToAdd = new ToolCategory("Test2", "0.0.0.1");
            ToolBundle bundle = new ToolBundle("TestBundle", "0.0.0.1");
            Assert.AreEqual(bundle.Categories.Count, 0);
            bundle.Categories.Add(categoryToAdd, testCategory);
            Assert.AreEqual(bundle.Categories.Count, 2);
        }

        [TestMethod]
        public void BundleCategoriesRemoveTest()
        {
            var testCategory = new ToolCategory("Test", "0.0.0.1");
            var categoryToAdd = new ToolCategory("Test2", "0.0.0.1");
            ToolBundle bundle = new ToolBundle("TestBundle", "0.0.0.1");
            bundle.Categories.Add(categoryToAdd, testCategory);
            Assert.AreEqual(bundle.Categories.Count, 2);

            var isRemoved = bundle.Categories.Remove(testCategory);
            Assert.AreEqual(isRemoved, true);
            Assert.AreEqual(bundle.Categories.Count, 1);

            var isRemovedAgain = bundle.Categories.Remove(testCategory);
            Assert.AreEqual(isRemovedAgain, false);
            Assert.AreEqual(bundle.Categories.Count, 1);
        }

        [TestMethod]
        public void BundleCategoriesIndexerTest()
        {
            var testCategory = new ToolCategory("Test", "0.0.0.0");
            var categoryToAdd = new ToolCategory("Test2", "0.0.0.0");
            ToolBundle bundle = new ToolBundle("TestBundle", "0.0.0.0");
            bundle.Categories.Add(categoryToAdd, testCategory);
            Assert.AreEqual(bundle.Categories.Count, 2);

            var categoryByTestIndex = bundle.Categories["Test"];
            Assert.AreEqual(categoryByTestIndex.FirstOrDefault(), testCategory);

            var isRemoved = bundle.Categories.Remove(testCategory);
            Assert.AreEqual(isRemoved, true);
            Assert.AreEqual(bundle.Categories.Count, 1);

            categoryByTestIndex = bundle.Categories["Test"];
            Assert.AreEqual(categoryByTestIndex.Count(), 0);
        }

        [TestMethod]
        public void BundleSerializationTest()
        {
            ToolBundle bundle = new ToolBundle("SysInternals", "0.0.0.1")
            {
                Description = "SysInternals Bundle"
            };

            var debugging = new ToolCategory("Debugging", "0.0.0.1")
            {
                Description = "Debugging Tools from SysInternals.Com"
            };

            var monitoring = new ToolCategory("Monitoring", "0.0.0.1")
            {
                Description = "Monitoring Tools from SysInternals.Com"
            };

            Tool procDump = new Tool("ProcDump", "0.0.0.1", @"d:\sysinternals\procdump.exe -n 10 -cpu 90")
            {
                Description = "Automatic Dump capture",
                IsPortable = true
            };

            Tool procExp = new Tool("ProcExp", "0.0.0.1", @"d:\sysinternals\procexp.exe")
            {
                Description = "Process Explorer -- TaskMgr on steroids!",
                UpdateURL = "http://sysinternals.com",
                CanUpdate = true
            };

            procExp.Properties.Add("Company", "Microsoft");
            procExp.Properties.Add("Published", "2015");

            debugging.Tools.Add(procDump);
            monitoring.Tools.Add(procExp);

            bundle.Categories.Add(debugging);
            bundle.Categories.Add(monitoring);
            Assert.AreEqual(bundle.Categories.Count, 2);

            var xml = bundle.ToXML();

            Assert.IsNotNull(xml);
        }
    }
}