using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Ipfs.VirtualDisk
{
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
