using Microsoft.Extensions.Configuration;
using NET02._4.CrawlerFabric;
using NLog;

namespace NET02._4;

public class Program
{
    static void Main()
    {
        var configFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"));
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"))
            .AddUserSecrets<Program>();
        foreach (var file in configFiles)
        {
            configBuilder.AddJsonFile(file);
        }
        var config = configBuilder.Build();
        var logger = LogManager.GetLogger("Crawler's Logger");
        var systemWatcher = new FileSystemWatcher(Path.Combine(Directory.GetCurrentDirectory(), "Configurations"));


        using var app = new MonitorApp(config, new WebCrawlerFabric(logger), systemWatcher, logger);
        app.Initialize();
        app.Run();
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
        }
        app.Stop();
    }
}