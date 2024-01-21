using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using NLog;
using Org.BouncyCastle.Tls;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace NET02._4.Crawler;

public class WebCrawler
{
    private TimeSpan _timeout;
    private TimeSpan _maxWaitingTime;
    private string _url;
    private string _mailAddress;
    private readonly ILogger _logger;
    private IConfiguration _config;
    private readonly HttpClient _httpClient;
    private readonly CancellationTokenSource _cancellationTokenSource = new ();
    private readonly FileSystemWatcher _systemWatcher = new();

    public WebCrawler(IConfiguration config, ILogger logger)
    {
        _config = config;
        _logger = logger;
        _httpClient = new HttpClient();
        _systemWatcher.Path = Directory.GetCurrentDirectory();
        _systemWatcher.Filter = "appsettings.json";
        _systemWatcher.Changed += ChangeConfig;
        SetConfig();
    }

    public async Task Start()
    {
        var cancellationToken = _cancellationTokenSource.Token;
        await CheckSite(cancellationToken);
    }
    public void Stop()
    {
        _cancellationTokenSource.Cancel();
    }

    private async Task CheckSite(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var startTime = DateTime.Now;
                var response = _httpClient.GetAsync(_url, token).Result;
                var elapsedTime = DateTime.Now - startTime;
                if (false || elapsedTime < _maxWaitingTime)
                {
                    _logger.Info("Site is working properly.");
                }
                else
                {
                    using var message = CreateEmailMessage();
                    using var client = new SmtpClient();
                    await client.ConnectAsync("smtp.mail.ru", 465, false);
                    await client.AuthenticateAsync("super.titlov@inbox.ru", "");
                    await client.SendAsync(message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Can't send email: {ex.Message}");
            }
            await Task.Delay(_timeout);
        }
        
    }

    private void ChangeConfig(object sender , FileSystemEventArgs args)
    {
        var watcher = new FileSystemWatcher();
        watcher.Path = "appsettings.json";
        watcher.Changed += (_, _) =>
        {
            SetConfig();
            _logger.Info("Configuration changed.");
        };

        while (true)
        {
            Task.Delay(1000);
        }
    }

    private MimeMessage CreateEmailMessage()
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Web Crawler", "crawler@mail.ru"));
        emailMessage.To.Add(new MailboxAddress("Ivan Titlov", _mailAddress));
        emailMessage.Subject = "Your site isn't working";
        emailMessage.Body = new TextPart("plain")
        {
            Text =
                $"Hello, Dear Administrator of site with address {_url}, \n\t I want to inform you that your site is currently isn't working. \n\t Regards,\n\t Web Crawler"
        };
        return emailMessage;

    }

    private void SetConfig()
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