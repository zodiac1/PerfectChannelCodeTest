using System;

using NLog;

namespace Logging
{
    public static class Log
    {
        public static Logger Instance { get; private set; }

        static Log()
        {
            LogManager.ReconfigExistingLoggers();
            Instance = LogManager.GetCurrentClassLogger();
        }

        public static void Info(string message)
        {
            Instance.Info(message);
        }

        public static void Error(string message, Exception ex)
        {
            Instance.ErrorException(message, ex);
        }
    }
}
