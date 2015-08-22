using System.IO;
using DefragEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DefragEngineTests
{
    [TestClass]
    public class HashTests
    {
        [TestMethod]
        public void HashInstanceTest()
        {
            // var emptyHash = new Hash();                                      // should fail -- no hash without input data allowed

            var stringHash = new Hash("test");                                  // should hold md5 of "test" string

            byte[] testHashValue = stringHash.Value;                            // should return byte[] value
            Assert.AreEqual(testHashValue.Length, 16);

            string testHashString = stringHash.ToString();                      // should return string representing value
            Assert.AreEqual(testHashString, "c8059e2ec7419f590e79d7f1b774bfe6");

            stringHash = new byte[] { 0xde, 0xad, 0xbe, 0xb0 };                 // stringHash now holds this byte[]
            Assert.AreEqual(stringHash.ToString(), "deadbeb0");

            stringHash = "deadbeef";                                            // stringHash now holds this value
            Assert.AreEqual(stringHash.ToString(), "deadbeef");

            testHashValue = stringHash.Value;                                   // should return { 0xde, 0xad, 0xbe, 0xef }
            Assert.AreEqual(testHashValue.Length, 4);
        }

        [TestMethod]
        public void StringHashTest()
        {
            var data1 = @"D:\Sysinternals\ProcDump.exe";
            var data2 = @"D:\Sysinternals\ProcExp.exe";

            var procDumpChecksum = new Hash(data1);
            var procExpChecksum = new Hash(data2);
            var procDumpChecksum2 = new Hash(data1);
            var procExpChecksum2 = new Hash(data2);

            Assert.IsNotNull(procDumpChecksum);
            Assert.IsNotNull(procExpChecksum);
            Assert.AreNotEqual(procDumpChecksum, procExpChecksum);
            Assert.AreEqual(procDumpChecksum, procDumpChecksum2);
            Assert.AreEqual(procExpChecksum, procExpChecksum2);
        }

        [TestMethod]
        public void FileHashTest()
        {
            using (FileStream data1 = new FileStream(@"D:\Sysinternals\ProcDump.exe", FileMode.Open, FileAccess.Read), data2 = new FileStream(@"D:\Sysinternals\ProcExp.exe", FileMode.Open, FileAccess.Read))
            {
                var procDumpChecksum = new Hash(data1);
                var procExpChecksum = new Hash(data2);

                data1.Position = 0;
                data2.Position = 0;

                var procDumpChecksum2 = new Hash(data1);
                var procExpChecksum2 = new Hash(data2);

                Assert.IsNotNull(procDumpChecksum);
                Assert.IsNotNull(procExpChecksum);
                Assert.AreNotEqual(procDumpChecksum, procExpChecksum);
                Assert.AreEqual(procDumpChecksum, procDumpChecksum2);
                Assert.AreEqual(procExpChecksum, procExpChecksum2);
            }
        }
    }
}