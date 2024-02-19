using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NET02._4.CrawlerFabric;
using NLog;
using MailKit.Net.Smtp;
using MimeKit;

namespace NET02._4
{
    public class MonitorApp : IDisposable
    {
        private readonly List<ICrawler> _crawlerList = [];
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly ICrawlerFabric _crawlerFabric;
        private readonly HttpClient _httpClient = new ();
        private readonly FileSystemWatcher _systemWatcher;

        /// <summary>
        /// Watching if one instance of class is running.
        /// </summary>
        public static bool IsRunning { get; private set; }

        /// <summary>
        /// Constructor of the monitoring app that is used to create list of crawlers and start them.
        /// </summary>
        /// <param name="config">Configuration for app.</param>
        /// <param name="crawlerFabric">Abstract fabric for creating crawlers.</param>
        /// <param name="systemWatcher">Watcher after config file.</param>
        /// <param name="logger">Logger for app.</param>
        public MonitorApp(IConfiguration config, ICrawlerFabric crawlerFabric, FileSystemWatcher systemWatcher, ILogger logger)
        { 
            _config = config;
            _logger = logger;
            _crawlerFabric = crawlerFabric;
            SetCrawlers();
            _systemWatcher = systemWatcher;
            _systemWatcher.NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Size;
            _systemWatcher.Changed += ChangeConfig;
        }

        /// <summary>
        /// Method for starting app.
        /// </summary>
        public async void Run()
        {
            if (IsRunning) return;
            _systemWatcher.EnableRaisingEvents = true;
            var token  = _cancellationTokenSource.Token;
            var tasks = new List<Task>();
            try
            {
                while (!token.IsCancellationRequested)
                {
                    IsRunning = true;
                    foreach (var crawler in _crawlerList)
                    {
                         tasks.Add(Task.Run(() => crawler.Start(token), token));
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.Info("Monitoring app is stopped.");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _systemWatcher.EnableRaisingEvents = false;
            IsRunning = false;
            _logger.Info("Operations is canceled!");
        }

        /// <summary>
        /// Method for config checking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void ChangeConfig(object sender, FileSystemEventArgs args)
        {

            Thread.Sleep(500);

            Stop();
            SetCrawlers();
            Run();
            _logger.Info("Configuration is changed.");
        }
        
        /// <summary>
        /// Method for creating SMTP client for app. 
        /// </summary>
        /// <returns>SMTP client for app with authentication and connection.</returns>
        private SmtpClient CreateSmtpClient()
        {
            var client = new SmtpClient();
            client.Connect("smtp.mail.ru", 465, true, CancellationToken.None);
            client.Authenticate(_config.GetValue<string>("MonitorAppMailAddress") ?? "super.titlov@inbox.ru",
                _config.GetValue<string>("Password"), CancellationToken.None);
            return client;
        }

        /// <summary>
        /// Method with creating of email message to administrator of site.
        /// </summary>
        /// <returns></returns>
        private MimeMessage CreateEmailMessage()
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Web Crawler", _config.GetValue<string>("MonitorAppMailAddress") ?? "super.titlov@inbox.ru"));
            return emailMessage;
        }

        /// <summary>
        /// Setting crawlers for checking URLs.
        /// </summary>
        private void SetCrawlers()
        {
            _crawlerList.Clear();
            var smtpClient = CreateSmtpClient();
            var message = CreateEmailMessage();
            foreach (var crawlerOptions in _config.GetSection("Crawlers").GetChildren())
            {
                _crawlerList.Add(_crawlerFabric.Create(crawlerOptions, smtpClient, message, _httpClient));
            }
        }

        public void Dispose()
        {
            foreach (var crawler in _crawlerList)
            {
                crawler.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        ~MonitorApp()
        {
            Dispose();
        }
    }
}
