﻿using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NET02._4.CrawlerFabric;
using NLog;

namespace NET02._4
{
    public class MonitorApp : IDisposable
    {
        private readonly Dictionary<ICrawler, string> _crawlerDict = new();
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
            _systemWatcher = systemWatcher;
        }

        /// <summary>
        /// Initializing crawlers in the app.
        /// </summary>
        public void Initialize()
        {
            SetCrawlers();
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
                
                IsRunning = true;
                foreach (var crawler in _crawlerDict.Keys)
                {
                     tasks.Add(Task.Run(() => crawler.Start(), token));
                }

                await Task.WhenAll(tasks);
                
            }
            catch (OperationCanceledException)
            {
                _logger.Info("Monitoring app is stopped.");
            }
        }

        /// <summary>
        /// Method for stopping app.
        /// </summary>
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

            var crawlerToStop = _crawlerDict
                .Where(c => c.Value == args.FullPath)
                .Select(pair => pair.Key)
                .ToList();

            foreach (var crawler in crawlerToStop)
            {
                crawler.Stop();
                crawler.Start();
            }
            _logger.Info("Configuration is changed.");
        }
        
        /// <summary>
        /// Method for creating SMTP client for app. 
        /// </summary>
        /// <returns>SMTP client for app with authentication and connection.</returns>
        private MailService CreateMailService()
        {
            var mailService = new MailService();
            mailService.InitializeMessage(_config.GetValue<string>("MonitorAppMailAddress") ?? "super.titlov@inbox.ru",
                _config.GetValue<string>("Password"), "Web Crawler");
            return mailService;
        }

        /// <summary>
        /// Setting crawlers for checking URLs.
        /// </summary>
        private void SetCrawlers()

        {
            _crawlerDict.Clear();
            var mailService = CreateMailService();
            foreach (var crawlerOptions in _config.GetSection("Crawler").GetChildren())
            {
                _crawlerDict.Add(_crawlerFabric.Create(crawlerOptions, _httpClient, mailService), crawlerOptions.Path);
            }
        }

        public void Dispose()
        {
            foreach (var crawler in _crawlerDict.Keys)
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
