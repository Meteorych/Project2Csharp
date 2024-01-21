using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NLog;

namespace NET02._4;

public class Program
{
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var logger = LogManager.GetLogger("Crawling Logger");
        var crawlerOptions = new CrawlerOptions();
        var crawler = new WebCrawler(crawlerOptions, logger);
        config.Bind(crawlerOptions);
        
    }
}