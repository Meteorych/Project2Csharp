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
    private ILogger _logger;
    private IConfiguration _config;

    public WebCrawler(IConfiguration config, ILogger logger)
    {
        _config = config;
        _logger = logger;
        Task.Run(SetConfig);
        Task.Run(CheckOptionChanges);
        Task.Run(CheckSite);
    }

    private async Task CheckSite()
    {
        await Task.Delay(_timeout);
    }

    private async Task CheckOptionChanges()
    {
        var watcher = new FileSystemWatcher();
        watcher.Path = "appsettings.json";
        watcher.Changed += async (sender, args) =>
        {
            await SetConfig();
        };

        while (true)
        {
            await Task.Delay(1000);
        }
    }

    private async Task SetConfig()
    {
        if (_config["Url"] is null || _config["MailAddress"] is null || !TimeSpan.TryParse(_config["Timeout"], out _timeout)
            || !TimeSpan.TryParse(_config["MaxWaitingTime"], out _maxWaitingTime))
        {
            throw new ArgumentNullException(nameof(_config), "Options is wrong (some of the fields equal null)!");
        }
        _url = _config["Url"]!;
        _mailAddress = _config["MailAddress"]!;
    }
}