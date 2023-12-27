using Microsoft.Extensions.Configuration;
using NLog;

namespace Assembly_and_Metadata
{
    internal class Program
    {
        static void Main()
        {
            var logger = LogManager.GetCurrentClassLogger();
            var nLogConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var myLogger = new MyLogger(logger, nLogConfig);
            myLogger.InitializeListeners();
            myLogger.LogMessage("Test");
            LogManager.Shutdown();
        }
    }
}