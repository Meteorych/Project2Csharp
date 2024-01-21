using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NLog;

namespace NET02._4.Crawler;

public class WebCrawler
{
    private TimeSpan _timeout;
    private TimeSpan _maxWaitingTime;
    private string _url;
    private string _mailAddress;
    private Logger _logger;


    public WebCrawler(CrawlerOptions options, Logger logger)
    {
        _logger = logger;
        _timeout = options.Timeout;
        _maxWaitingTime = options.MaxWaitingTime;
        if (options.Url is null || options.MailAddress is null)
        {   
            var exceptions = new List<Exception>
            {
                new ArgumentNullException(nameof(options.Url)),
                new ArgumentNullException(nameof(options.MailAddress))
            };
            throw new AggregateException("Options is wrong (some of the fields equal null)!", exceptions);
        }
        _url = options.Url;
        _mailAddress = options.MailAddress;
    }
}