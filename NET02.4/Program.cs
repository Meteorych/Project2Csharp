using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NLog;

namespace NET02._4;

public class Program
{
    static async Task Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        var logger = LogManager.GetLogger("Crawling Logger");
        var crawler = new WebCrawler(config, logger);
        
        await crawler.Start();
        await Task.Delay(100000);
        crawler.Stop();
        
    }
}