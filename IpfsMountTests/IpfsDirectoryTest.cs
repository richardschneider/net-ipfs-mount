using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ipfs.VirtualDisk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.VirtualDisk.Tests
{
    [TestClass]
    public class IpfsDirectoryTest
    {

        [TestMethod]
        public void Exists()
        {
            Assert.IsTrue(Directory.Exists("t:/ipfs"));
        }

        [TestMethod]
        public void Has_No_Files()
        {
            var info = new DirectoryInfo("t:/ipfs");

            Assert.IsTrue(info.Exists);
            Assert.AreEqual(0, info.GetFileSystemInfos("*.*").Length);
        }

    }
}