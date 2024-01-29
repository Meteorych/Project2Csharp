using Microsoft.Extensions.Configuration;
using MimeKit;
using NLog;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace NET02._4.Crawler;

public class WebCrawler : ICrawler, IDisposable
{
    private TimeSpan _timeout;
    private TimeSpan _maxWaitingTime;
    private string _url;
    private string _mailAddress;
    private string _adminName;
    private readonly MimeMessage _message;

    private readonly ILogger _logger;
    private readonly IConfiguration _config;
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
        _message = CreateEmailMessage();
        _systemWatcher.Path = Directory.GetCurrentDirectory();
        _systemWatcher.Filter = "appsettings.json";
        _systemWatcher.Changed += ChangeConfig;

    }

    /// <summary>
    /// Method that starts running of crawler.
    /// </summary>
    /// <returns></returns>
    public void Start()
    {
        _systemWatcher.EnableRaisingEvents = true;
        var cancellationToken = _cancellationTokenSource.Token;

        Task.Run(() => CheckSite(cancellationToken), cancellationToken);
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
    public async Task CheckSite(CancellationToken token)
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
                    using var client = new SmtpClient();
                    await client.ConnectAsync("smtp.mail.ru", 465, true, CancellationToken.None);
                    await client.AuthenticateAsync("super.titlov@inbox.ru", _config.GetValue<string>("Password"), CancellationToken.None);
                    await client.SendAsync(_message, CancellationToken.None);
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
    private void ChangeConfig(object sender , FileSystemEventArgs args)
    {
        //Small delay to ensure that all file's changes is saved properly.
        Thread.Sleep(500);

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
        emailMessage.To.Add(new MailboxAddress(_adminName, _mailAddress));
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
        if (_config["Url"] is null || _config["MailAddress"] is null || _config["AdminName"] is null || !TimeSpan.TryParse(_config["Timeout"], out _timeout)
            || !TimeSpan.TryParse(_config["MaxWaitingTime"], out _maxWaitingTime))
        {
            throw new ArgumentNullException(nameof(_config), "Options is wrong (some of the fields equal null)!");
        }
        _url = _config["Url"]!;
        _adminName = _config["AdminName"]!;
        _mailAddress = _config["MailAddress"]!;
    }

    public void Dispose()
    {
        _message.Dispose();
        _systemWatcher.Dispose();
        _cancellationTokenSource.Dispose();
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }

    ~WebCrawler()
    {
        Dispose();
        _logger.Info("Object is finalized.");
    }
}