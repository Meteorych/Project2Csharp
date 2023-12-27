using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using NLog;
using NLog.Extensions.Logging;

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
        }
    }
}