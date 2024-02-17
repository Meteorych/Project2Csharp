using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileSystemGlobbing;
using NET02._4.Crawler;
using NET02._4.CrawlerFabric;
using NLog;
using System;

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
        private readonly FileSystemWatcher _systemWatcher = new(Directory.GetCurrentDirectory(), "appsettings.json");

        public static bool IsRunning { get; private set; }

        public MonitorApp(IConfiguration config, ICrawlerFabric crawlerFabric, ILogger logger)
        { 
            _config = config;
            _logger = logger;
            _crawlerFabric = crawlerFabric;
            SetCrawlers();
            _systemWatcher.NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Size;
            _systemWatcher.Changed += ChangeConfig;
            _systemWatcher.EnableRaisingEvents = true;
            var token = _cancellationTokenSource.Token;
            Console.ReadKey();
        }

        public void Run()
        {
            if (IsRunning) return;
            _systemWatcher.EnableRaisingEvents = true;
            var token  = _cancellationTokenSource.Token;
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
            }
            try
            {
                IsRunning = true;
                foreach (var crawler in _crawlerList)
                {
                    crawler.Start(token);
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
        
        private void SetCrawlers()
        {
            _crawlerList.Clear();
            foreach (var crawlerOptions in _config.GetSection("Crawlers").GetChildren())
            {
                _crawlerList.Add(_crawlerFabric.Create(crawlerOptions, _httpClient));
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
