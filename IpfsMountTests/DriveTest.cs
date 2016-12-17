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
    public class DriveTest
    {
        [TestMethod]
        public void Info()
        {
            var info = new DriveInfo("z");

            Assert.IsTrue(info.IsReady);
            Assert.AreEqual("IPFS", info.DriveFormat);
            Assert.AreEqual("Interplanetary", info.VolumeLabel);
        }

        [TestMethod]
        public void Root_Exists()
        {
            var info = new DriveInfo("z").RootDirectory;
            Assert.IsTrue(info.Exists);
            Assert.AreEqual(@"z:\", info.FullName);
        }

        [TestMethod]
        public void Root_Contains_Special_Folders()
        {
            var info = new DriveInfo("z").RootDirectory;
            Assert.IsTrue(info.Exists);

            var folder = new DirectoryInfo("z:/ipfs");
            Assert.IsTrue(folder.Exists);
            folder = new DirectoryInfo("z:/ipns");
            Assert.IsTrue(folder.Exists);

            folder = new DirectoryInfo("z:/foobar");
            Assert.IsFalse(folder.Exists);
        }

        [TestMethod]
        public void Root_Enumerate_Special_Folders()
        {
            var folders = Directory.EnumerateDirectories("z:/");
            Assert.AreEqual(2, folders.Count());
        }

    }
}