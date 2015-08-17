using System;
using DefragEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefragEngineTests
{
    [TestClass]
    public class ToolTests
    {
        [TestMethod]
        public void ToolInstanceTest()
        {
            Tool tool = new Tool("ProcDump", "0.0.0.1", @"d:\sysinternals\procdump.exe");
            Assert.IsNotNull(tool);
            Assert.AreNotEqual(tool.ID, Guid.Empty);
            Assert.AreEqual(tool.Name, "ProcDump");
            Assert.AreEqual(tool.Version, "0.0.0.1");
            Assert.AreEqual(tool.CommandLine, @"d:\sysinternals\procdump.exe");
        }
    }
}