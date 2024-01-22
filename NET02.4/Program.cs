﻿using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NLog;

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
        var logger = LogManager.GetLogger("Crawling Logger");
        var crawler = new WebCrawler(config, logger);
        crawler.Start();
        while (Console.ReadKey().Key != ConsoleKey.Q)
        {
        }

        crawler.Stop();
        
    }
}