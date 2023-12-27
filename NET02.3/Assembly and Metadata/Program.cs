using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace Assembly_and_Metadata
{
    internal class Program
    {
        static void Main()
        {
            var nLogConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var logger = LogManager.GetCurrentClassLogger();
            
            var myLogger = new MyLogger(logger, nLogConfig);
            myLogger.InitializeListeners();
            myLogger.LogMessage("Test");
            myLogger.Track(new Person());
            LogManager.Shutdown();
        }
    }
}