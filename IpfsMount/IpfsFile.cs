using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.VirtualDisk
{
    class IpfsFile
    {
        public string Hash { get; set; }
        public long Size { get; set; }
        public bool IsDirectory { get; set; }
        public List<IpfsFileLink> Links { get; set; }
    }

    class IpfsFileLink
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public long Size { get; set; }
        public bool IsDirectory { get; set; }
    }
}
