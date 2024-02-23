using Microsoft.Extensions.Configuration;
using MimeKit;
using NLog;

namespace NET02._4.Crawler;

public class WebCrawler : ICrawler
{
    private TimeSpan _timeout;
    private TimeSpan _maxWaitingTime;
    private string _url;
    private string _mailAddress;
    private string _adminName;
    private readonly ILogger _logger;
    private readonly IConfiguration _config;
    private bool _runningStatus;
    private readonly HttpClient _httpClient;
    private readonly MailService _mailService;

    /// <summary>
    /// Constructor for crawler.
    /// </summary>
    /// <param name="config">Crawler's configuration.</param>
    /// <param name="logger">Logger for crawler.</param>
    /// <param name="httpClient">HttpClient for crawler.</param>
    /// <param name="mailService">SmtpClient for crawler.</param>
    public WebCrawler(IConfiguration config, MailService? mailService, HttpClient httpClient, ILogger logger)
    {
        _config = config;
        _logger = logger;
        _httpClient = httpClient;
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        SetConfig();
    }

    /// <summary>
    /// Method for checking sites.
    /// </summary>
    /// <returns></returns>
    public async Task Start()
    {
        _runningStatus = true;
        while (_runningStatus)
        {

            try
            {
                var startTime = DateTime.Now;
                var response = await _httpClient.GetAsync(_url, CancellationToken.None);
                var elapsedTime = DateTime.Now - startTime;
                if (response.IsSuccessStatusCode && elapsedTime < _maxWaitingTime)
                {
                    _logger.Info($"{_url} is working properly.");
                }
                else
                {
                    var bodyMessage = new TextPart("plain")
                    {
                        Text =
                            $"Hello, Dear Administrator of site with address {_url}, \n\t I want to inform you that your site is currently isn't working. \n\t Regards,\n\t Web Crawler"
                    };
                    _mailService.InitializeMessageBody(new MailboxAddress(_adminName, _mailAddress), "Your site isn't working", bodyMessage);
                    await _mailService.SendMessage();
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

    public void Stop()
    {
        _runningStatus = false;
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
        _mailService.Dispose();
        _httpClient.Dispose();
        _logger.Debug("Object is disposed.");
        GC.SuppressFinalize(this);
    }

    ~WebCrawler()
    {
        Dispose();
        _logger.Debug("Object is finalized.");
    }
}