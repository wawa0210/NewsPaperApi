using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;

namespace CommonLib
{
    public static class LogHelper
    {
        private static ILoggerRepository Repository { get; set; }

        private static ILog Logger { get; set; }

        static LogHelper()
        {
            Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));

            Logger = LogManager.GetLogger(Repository.Name, typeof(LogHelper));
        }

        public static void LogInfo(object message)
        {
            Logger.Info(message);
        }

        public static void LogError(object message, Exception exception)
        {
            Logger.Error(message, exception);
        }
    }
}
