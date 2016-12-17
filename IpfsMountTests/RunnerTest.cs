using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ipfs.VirtualDisk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ipfs.VirtualDisk.Tests
{
    [TestClass]
    public class RunnerTest
    {
        [AssemblyInitialize]
        public static void Mount(TestContext context)
        {
            var vdisk = new Runner();
            var thread = new Thread(() => vdisk.Mount("t:", null, true));
            thread.Start();

            // Wait for Mount to work.
            while (true)
            {
                Thread.Sleep(0);
                var info = new DriveInfo("t");
                if (info.IsReady)
                    break;
            }
        }

        [AssemblyCleanup]
        public static void Unmount()
        {
            var vdisk = new Runner();
            vdisk.Unmount("t:");
        }
    }
}