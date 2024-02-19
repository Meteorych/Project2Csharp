using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
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

        public ICrawler Create(IConfigurationSection config, HttpClient httpClient, SmtpClient? smtpClient, MimeMessage? message)
        {
            return new WebCrawler(config, smtpClient, message, httpClient, _logger);
        }
    }
}
