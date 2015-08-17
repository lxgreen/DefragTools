using System;
using System.Linq;
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
            ToolCategory category = new ToolCategory("SysInternals", "0.0.0.1");
            Assert.IsNotNull(category);
            Assert.AreNotEqual(category.ID, Guid.Empty);
            Assert.AreEqual(category.Name, "SysInternals");
            Assert.AreEqual(category.Version, "0.0.0.1");
            Assert.IsNotNull(category.Tools);
            Assert.AreEqual(category.Tools.Count, 0);
        }

        [TestMethod]
        public void CategoryToolsAddTest()
        {
            ToolCategory category = new ToolCategory("SysInternals", "0.0.0.1");
            Assert.AreEqual(category.Tools.Count, 0);
            Tool procDump = new Tool("ProcDump", "0.0.0.1", @"d:\sysinternals\procdump.exe -n 10 -cpu 90");
            Tool procExp = new Tool("ProcExp", "0.0.0.1", @"d:\sysinternals\procexp.exe");
            category.Tools.Add(procDump);
            Assert.AreEqual(category.Tools.Count, 1);
            category.Tools.Add(procExp);
            Assert.AreEqual(category.Tools.Count, 2);
        }

        [TestMethod]
        public void CategoryToolsAddRangeTest()
        {
            Tool procDump = new Tool("ProcDump", "0.0.0.1", @"d:\sysinternals\procdump.exe -n 10 -cpu 90");
            Tool procExp = new Tool("ProcExp", "0.0.0.1", @"d:\sysinternals\procexp.exe");
            ToolCategory category = new ToolCategory("SysInternals", "0.0.0.1");
            Assert.AreEqual(category.Tools.Count, 0);
            category.Tools.Add(procDump, procExp);
            Assert.AreEqual(category.Tools.Count, 2);
        }

        [TestMethod]
        public void CategoryToolsRemoveTest()
        {
            Tool procDump = new Tool("ProcDump", "0.0.0.1", @"d:\sysinternals\procdump.exe -n 10 -cpu 90");
            Tool procExp = new Tool("ProcExp", "0.0.0.1", @"d:\sysinternals\procexp.exe");
            ToolCategory category = new ToolCategory("SysInternals", "0.0.0.1");
            category.Tools.Add(procDump, procExp);

            var isRemoved = category.Tools.Remove(procDump);
            Assert.AreEqual(isRemoved, true);
            Assert.AreEqual(category.Tools.Count, 1);

            isRemoved = category.Tools.Remove(procDump);
            Assert.AreEqual(isRemoved, false);
            Assert.AreEqual(category.Tools.Count, 1);
        }

        [TestMethod]
        public void CategoryToolsIndexerTest()
        {
            Tool procDump = new Tool("ProcDump", "0.0.0.1", @"d:\sysinternals\procdump.exe -n 10 -cpu 90");
            Tool procExp = new Tool("ProcExp", "0.0.0.1", @"d:\sysinternals\procexp.exe");
            ToolCategory category = new ToolCategory("SysInternals", "0.0.0.1");
            category.Tools.Add(procDump, procExp);

            var toolByProcDumpIndex = category.Tools["ProcDump"];
            Assert.AreEqual(toolByProcDumpIndex.FirstOrDefault(), procDump);

            var isRemoved = category.Tools.Remove(procDump);
            Assert.AreEqual(isRemoved, true);
            Assert.AreEqual(category.Tools.Count, 1);

            toolByProcDumpIndex = category.Tools["ProcDump"];
            Assert.AreEqual(toolByProcDumpIndex.Count(), 0);
        }
    }
}