using System;
using System.IO;

namespace QuickMark
{
    public static class Logger
    {
        private static string logFilePath;

        static Logger()
        {
            string filepath = "log.txt";
            logFilePath = filepath;
        }

        public static void Log(string message)
        {
            string logMessage = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {message}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
