using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NLog;

namespace NET02._4.CrawlerFabric
{
    public class WebCrawlerFabric : ICrawlerFabric
    {

        private readonly ILogger _logger; 
        public WebCrawlerFabric(ILogger logger)
        {
            _logger = logger;
        }

        public ICrawler Create(IConfigurationSection config)
        {
            return new WebCrawler(config, _logger);
        }
    }
}
