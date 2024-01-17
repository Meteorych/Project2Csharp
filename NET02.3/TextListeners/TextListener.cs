using Listeners;

namespace TextListeners
{
    /// <summary>
    /// TextListener class for logging on *.txt files
    /// </summary>
    public class TextListener : IListener
    {
        private readonly object _lockObject = new();
        private readonly ListenerOptions _options;
        public EventHandler<EventListenerArgs>? Events;

        public TextListener(ListenerOptions options)
        {
            if (string.IsNullOrEmpty(options.FilePath))
            {
                throw new ArgumentNullException(nameof(options.FilePath), "File path can't be empty");
            }
        }

        public void LogMessage(string message)
        {
            if (!IsLogLevelEnabled(message)) return;
            lock (_lockObject)
            {
                try
                {
                    File.AppendAllText(_options.FilePath ?? throw new InvalidOperationException(), $"{DateTime.Now}: {message}\n");
                }
                catch (Exception ex)
                {
                    OnEvent(new EventListenerArgs(ex.Message));
                }
            }
        }

        private bool IsLogLevelEnabled(string message)
        {
            var comparisonOption = StringComparison.InvariantCultureIgnoreCase;
            switch (_options.MinimumLogLevel)
            {
                case LogLevels.Trace:
                    return true; //All levels
                case LogLevels.Debug:
                    return (message.Contains("DEBUG", comparisonOption) || message.Contains("INFO", comparisonOption) ||
                            message.Contains("WARN", comparisonOption)
                            || message.Contains("ERROR", comparisonOption));
                case LogLevels.Info:
                    return (message.Contains("INFO", comparisonOption) ||
                            message.Contains("WARN", comparisonOption)
                            || message.Contains("ERROR", comparisonOption));
                case LogLevels.Warn:
                    return (message.Contains("WARN", comparisonOption)
                            || message.Contains("ERROR", comparisonOption));
                case LogLevels.Error:
                    return message.Contains("ERROR", comparisonOption);
                default:
                    return false;
            }
        }

        protected void OnEvent(EventListenerArgs e)
        {
            Events?.Invoke(this, e);
        }

    }
}
