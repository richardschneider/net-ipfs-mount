using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DokanNet;


namespace IpfsMount
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Missing the drive letter.");

            // Allow colon after drive letter
            var drive = args[0];
            if (drive.EndsWith(":"))
                drive = drive.Substring(0, drive.Length - 1);
            drive = drive + @":\";

            Dokan.Mount(new IpfsDokan(), drive, DokanOptions.WriteProtection);
            Console.WriteLine(drive);
        }
    }
}
