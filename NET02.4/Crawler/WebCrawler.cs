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
    private readonly HttpClient _httpClient = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly FileSystemWatcher _systemWatcher = new();

    /// <summary>
    /// Constructor for crawler.
    /// </summary>
    /// <param name="config">Crawler's configuration.</param>
    /// <param name="logger">Logger for crawler.</param>
    public WebCrawler(IConfiguration config, ILogger logger)
    {
        _config = config;
        _logger = logger;
        SetConfig();
        _systemWatcher.Path = Directory.GetCurrentDirectory();
        _systemWatcher.Filter = "appsettings.json";
        _systemWatcher.Changed += async (sender, args) => await Task.Run(() => ChangeConfig(sender, args));
    }

    /// <summary>
    /// Method that starts running of crawler.
    /// </summary>
    /// <returns></returns>
    public async Task Start()
    {
        _systemWatcher.EnableRaisingEvents = true;
        var cancellationToken = _cancellationTokenSource.Token;

        await Task.WhenAll(CheckSite(cancellationToken));
    }

    /// <summary>
    /// Method for stopping the crawler.
    /// </summary>
    public void Stop()
    {
        _cancellationTokenSource.Cancel();
        _systemWatcher.EnableRaisingEvents = false;
    }

    /// <summary>
    /// Method for checking sites.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns></returns>
    private async Task CheckSite(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var startTime = DateTime.Now;
                var response = await _httpClient.GetAsync(_url, CancellationToken.None);
                var elapsedTime = DateTime.Now - startTime;
                if (response.IsSuccessStatusCode || elapsedTime < _maxWaitingTime)
                {
                    _logger.Info("Site is working properly.");
                }
                else
                {
                    using var message = CreateEmailMessage();
                    using var client = new SmtpClient();
                    await client.ConnectAsync("smtp.mail.ru", 465, true, CancellationToken.None);
                    await client.AuthenticateAsync("super.titlov@inbox.ru", "", CancellationToken.None);
                    await client.SendAsync(message, CancellationToken.None);
                    _logger.Info("Email is sent.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Can't send email: {ex.Message}");
            }
            await Task.Delay(_timeout, CancellationToken.None);
        }
        
    }

    /// <summary>
    /// Method for config checking.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private async Task ChangeConfig(object sender , FileSystemEventArgs args)
    {
        SetConfig();
        _logger.Info("Configuration changed.");
    }

    /// <summary>
    /// Method with creating of email message to administrator of site.
    /// </summary>
    /// <returns></returns>
    private MimeMessage CreateEmailMessage()
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Web Crawler", "super.titlov@inbox.ru"));
        emailMessage.To.Add(new MailboxAddress("Ivan Titlov", _mailAddress));
        emailMessage.Subject = "Your site isn't working";
        emailMessage.Body = new TextPart("plain")
        {
            Text =
                $"Hello, Dear Administrator of site with address {_url}, \n\t I want to inform you that your site is currently isn't working. \n\t Regards,\n\t Web Crawler"
        };
        return emailMessage;

    }

    /// <summary>
    /// Setting of configuration.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
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