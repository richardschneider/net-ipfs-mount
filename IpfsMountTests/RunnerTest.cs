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
    public class RunnerTest
    {
        [TestMethod]
        public void MountTest()
        {
            var info = new DriveInfo("z");

            Assert.IsTrue(info.IsReady);
        }

        [TestMethod]
        public void UnmountTest()
        {
            var vdisk = new Runner();
            //vdisk.Unmount("z:");
        }
    }
}