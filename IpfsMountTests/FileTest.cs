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
    public class FileTest
    {

        [TestMethod]
        public void Exists()
        {
            Assert.IsTrue(File.Exists("z:/ipfs/QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF/cat.jpg"));
            Assert.IsTrue(File.Exists("z:/ipfs/QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF/test/baz/f"));
            Assert.IsFalse(File.Exists("z:/ipfs/QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF/test/baz/g"));
        }

        [TestMethod]
        public void File_Info()
        {
            var info = new FileInfo("z:/ipfs/QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF/cat.jpg");

            Assert.IsTrue(info.Exists);
            Assert.IsTrue(info.IsReadOnly);
            Assert.AreEqual(139781, info.Length);
        }

        [TestMethod]
        public void Can_Read_File()
        {
            var s = File.ReadAllText(@"z:\ipfs\QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF\test\baz\f");
            Assert.AreEqual("foo\n", s);
        }

        [TestMethod]
        public void Can_Read_Big_File()
        {
            var content = File.ReadAllBytes(@"z:\ipfs\QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF\cat.jpg");
            Assert.AreEqual(139781, content.Length);
            Assert.AreEqual(255, content[0]);
            Assert.AreEqual(217, content[139780]);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void Cannot_Create_File()
        {
            File.Create(@"z:\ipfs\QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF\bad.file");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void Cannot_Delete_File()
        {
            File.Create(@"z:\ipfs\QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF\cat.jpg");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void Cannot_Update_File()
        {
            File.OpenWrite(@"z:\ipfs\QmRCJXG7HSmprrYwDrK1GctXHgbV7EYpVcJPQPwevoQuqF\cat.jpg");
        }
    }
}