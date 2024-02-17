using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NET02._4.CrawlerFabric;
using NLog;
using NLog.LayoutRenderers.Wrappers;

namespace NET02._4;

public class Program
{
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<Program>()
            .Build();
        var logger = LogManager.GetLogger("Crawler's Logger");
        var app = new MonitorApp(config, new WebCrawlerFabric(logger), logger);

        app.Run();


        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
        }
        app.Stop();
        

    }
}