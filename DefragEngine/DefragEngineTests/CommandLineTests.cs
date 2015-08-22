using DefragEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefragEngineTests
{
    [TestClass]
    public class CommandLineTests
    {
        [TestMethod]
        public void CommandLineParseTest()
        {
            string procdumpTestLine = @"d:\sysinternals\procdump -n 10 -cpu 90";
            string batTestLine = @"""d:\My Work\deploy_igloo_bin.bat"" ""d:\My Work\Interception\LowLevel\"" ";
            string exec;
            var args = CommandLineParser.CommandLineToArgs(procdumpTestLine, out exec);
            Assert.AreEqual(@"d:\sysinternals\procdump", exec);
            Assert.AreEqual(args.Length, 4);

            args = CommandLineParser.CommandLineToArgs(batTestLine, out exec);
            Assert.AreEqual(@"d:\My Work\deploy_igloo_bin.bat", exec);
            Assert.AreEqual(args.Length, 1);
        }
    }
}