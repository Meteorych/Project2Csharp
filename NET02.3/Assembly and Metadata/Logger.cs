using EventLogListeners;
using Listeners;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Targets;
using TextListeners;
using WordListeners;

namespace Assembly_and_Metadata
{
    public class MyLogger
    {
        private ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, IListener> _listeners = new Dictionary<string, IListener>();

        public MyLogger(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void InitializeListeners()
        {
            _listeners.Add("TextListeners", new TextListener(_configuration.GetSection("NLog:targets:textFile:fileName").Value ?? "logwrong.txt"));
            _listeners.Add("WordListener", new WordListener(_configuration.GetSection("NLog:targets:wordFile:fileName").Value ?? "logwrong.docx"));
            _listeners.Add("EventLogListener", new EventLogListener());
        }

        public void LogMessage(string message)
        {
            foreach (var pair in _listeners)
            {
                pair.Value.LogMessage(message);
            }
        }
    }
}
