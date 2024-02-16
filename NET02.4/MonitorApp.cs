﻿using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NLog;

namespace NET02._4
{
    public class MonitorApp : IDisposable
    {
        private readonly List<WebCrawler> _crawlerList = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly ILogger _logger;
        private IConfiguration _config;
        public HttpClient _httpClient = new ();
        private readonly FileSystemWatcher _systemWatcher = new();

        public static bool IsRunning { get; private set; }
        public MonitorApp(IConfiguration config, ILogger logger)
        { 
            _config = config;
            _logger = logger;
            foreach (var crawlerOptions in _config.GetSection("Crawlers").GetChildren())
            {
                _crawlerList.Add(new WebCrawler(crawlerOptions, logger));
            }
            _systemWatcher.Path = Directory.GetCurrentDirectory();
            _systemWatcher.Filter = "appsettings.json";
            _systemWatcher.Changed += ChangeConfig;
        }

        public void Run()
        {
            if (IsRunning) return;
            _systemWatcher.EnableRaisingEvents = true;
            var token  = _cancellationTokenSource.Token;
            var tasks = new List<Task>();
            try
            {
                IsRunning = true;
                foreach (var crawler in _crawlerList)
                {
                    tasks.Add(Task.Run(() => crawler.Start(token), token));
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
            IsRunning = false;
        }

        /// <summary>
        /// Method for config checking.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ChangeConfig(object sender, FileSystemEventArgs args)
        {
            //Small delay to ensure that all file's changes is saved properly.
            Thread.Sleep(500);

            
            _logger.Info("Configuration changed.");
        }

        public void Dispose()
        {
            foreach (var crawler in _crawlerList)
            {
                crawler.Dispose();
            }
        }
    }
}
