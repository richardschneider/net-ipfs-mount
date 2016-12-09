using Common.Logging;
using Common.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DokanNet;
using Ipfs.Api;
using NDesk.Options;

namespace IpfsMount
{
    class Program
    {
        static int Main(string[] args)
        {
            // Command line parsing
            bool help = false;
            bool debugging = false;
            string ipfsServer = "http://127.0.0.1:5001";
            var p = new OptionSet() {
                { "s|server=", "IPFS API server address", v => ipfsServer = v},
                { "d|debug", "Display debugging information", v => debugging = v != null },
                { "h|?|help", "Show this help", v => help = v != null },
            };
            List<string> extras;
            try
            {
                extras = p.Parse(args);
            }
            catch (OptionException e)
            {
                return ShowError(e.Message);
            }
            if (help)
                return ShowHelp(p);

            // Logging
            NameValueCollection properties = new NameValueCollection();
            properties["level"] = debugging ? "Debug" : "Error";
            properties["showDateTime"] = debugging.ToString();
            LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
            var log = LogManager.GetLogger("ipfs-mount");

            // Allow colon after drive letter
            if (extras.Count < 1)
                return ShowError("Missing the drive letter.");
            var drive = extras[0];
            if (drive.EndsWith(":"))
                drive = drive.Substring(0, drive.Length - 1);
            drive = drive + @":\";

            // Verify that the local IPFS service is up and running
            try
            {
                IpfsClient.DefaultApiUri = new Uri(ipfsServer);
                new IpfsClient().Id();
            }
            catch (Exception e)
            {
                if (debugging)
                    log.Fatal("IPFS is not running", e);
                else
                    Console.WriteLine("IPFS is not running at {0}\nTry 'ipfs daemon'.", ipfsServer);
                return 1;
            }
            
            // Mount IPFS, doesn't return until the drive is dismounted
            var options = DokanOptions.WriteProtection;
            if (debugging)
                options |= DokanOptions.DebugMode;
            try
            {
                Dokan.Mount(new IpfsDokan(),
                    drive,
                    options,
                    new DokanLogger());
            }
            catch (Exception e)
            {
                return ShowError(e.Message);
            }

            return 0;
        }

        static int ShowError(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Try 'ipfs-mount --help' for more information.");
            return 1;
        }

        static int ShowHelp(OptionSet p)
        {
            Console.WriteLine("Mount the IPFS on the specified drive");
            Console.WriteLine();
            Console.WriteLine("Usage: ipfs-mount drive [OPTIONS]");
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
            return 0;
        }
    }

    /// <summary>
    ///   Maps Dokan logging to Common Logging.
    /// </summary>
    class DokanLogger : DokanNet.Logging.ILogger
    {
        static ILog log = LogManager.GetLogger("Dokan");

        public void Debug(string message, params object[] args)
        {
            if (log.IsDebugEnabled)
            {
                if (args.Length > 0)
                    log.DebugFormat(message, args);
                else
                    log.Debug(message);
            }
        }

        public void Error(string message, params object[] args)
        {
            if (log.IsErrorEnabled)
            {
                if (args.Length > 0)
                    log.ErrorFormat(message, args);
                else
                    log.Error(message);
            }
        }

        public void Fatal(string message, params object[] args)
        {
            if (log.IsFatalEnabled)
            {
                if (args.Length > 0)
                    log.FatalFormat(message, args);
                else
                    log.Fatal(message);
            }
        }

        public void Info(string message, params object[] args)
        {
            if (log.IsInfoEnabled)
            {
                if (args.Length > 0)
                    log.InfoFormat(message, args);
                else
                    log.Info(message);
            }
        }

        public void Warn(string message, params object[] args)
        {
            if (log.IsWarnEnabled)
            {
                if (args.Length > 0)
                    log.WarnFormat(message, args);
                else
                    log.Warn(message);
            }
        }
    }
}
