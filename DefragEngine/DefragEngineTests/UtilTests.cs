﻿using System.Diagnostics;
using DefragEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefragEngineTests
{
    /// <summary>
    /// Summary description for UtilTests
    /// </summary>
    [TestClass]
    public class UtilTests
    {
        public UtilTests()
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
        public void GetExtendedPropertiesTest()
        {
            var properties = DefragEngineUtils.GetExtendedProperties(@"D:\Sysinternals\ProcDump.exe");
            foreach (var prop in properties)
            {
                Debug.WriteLine(string.Format("{0}: {1}", prop.Key, prop.Value));
            }

            Assert.IsNotNull(properties);
            Assert.AreNotEqual(properties.Count, 0);
        }

        [TestMethod]
        public void GetExtendedPropertyHeadersTest()
        {
            var headers = DefragEngineUtils.GetExtendedPropertyHeaders(@"D:\Sysinternals\ProcDump.exe");
            foreach (var header in headers)
            {
                Debug.WriteLine(string.Format("{0}. {1}", header.Key, header.Value));
            }

            Assert.IsNotNull(headers);
            Assert.AreNotEqual(headers.Count, 0);
        }
    }
}