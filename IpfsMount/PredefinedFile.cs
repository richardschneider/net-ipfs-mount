using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ipfs.VirtualDisk
{
    class PredefinedFile
    {
        public static Dictionary<string, PredefinedFile> All { get; private set; }

        static PredefinedFile()
        {
            const string prefix = "Ipfs.VirtualDisk.Resources.PredefineFiles.";
            All = Assembly.GetExecutingAssembly()
                .GetManifestResourceNames()
                .Where(name => name.StartsWith(prefix))
                .Select(name => new PredefinedFile
                {
                    Name = @"\" + name.Substring(prefix.Length),
                    Data = GetData(name)
                })
                .ToDictionary(f => f.Name, f => f);
        }

        public string Name { get; set; }
        public byte[] Data { get; set; }

        static byte[] GetData(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
