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
            var info = new DriveInfo("t");

            Assert.IsTrue(info.IsReady);
            Assert.AreEqual("IPFS", info.DriveFormat);
            Assert.AreEqual("Interplanetary", info.VolumeLabel);
        }

        [TestMethod]
        public void Drive_Letter_is_Case_Insensitive()
        {
            var info = new DriveInfo("T");

            Assert.IsTrue(info.IsReady);
            Assert.AreEqual("IPFS", info.DriveFormat);
            Assert.AreEqual("Interplanetary", info.VolumeLabel);
        }

        [TestMethod]
        public void Root_Exists()
        {
            var info = new DriveInfo("t").RootDirectory;
            Assert.IsTrue(info.Exists);
            Assert.AreEqual(@"t:\", info.FullName);
        }

        [TestMethod]
        public void Root_Contains_Special_Folders()
        {
            var info = new DriveInfo("t").RootDirectory;
            Assert.IsTrue(info.Exists);

            var folder = new DirectoryInfo("t:/ipfs");
            Assert.IsTrue(folder.Exists);
            folder = new DirectoryInfo("t:/ipns");
            Assert.IsTrue(folder.Exists);

            folder = new DirectoryInfo("t:/foobar");
            Assert.IsFalse(folder.Exists);
        }

        [TestMethod]
        public void Root_Enumerate_Special_Folders()
        {
            var folders = Directory.EnumerateDirectories("t:/");
            Assert.AreEqual(2, folders.Count());
        }

    }
}