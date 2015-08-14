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
            Tool tool = new Tool("ProcDump");
            Assert.IsNotNull(tool);
            Assert.AreNotEqual(tool.ID, Guid.Empty);
            Assert.AreEqual(tool.Name, "ProcDump");
        }
    }
}