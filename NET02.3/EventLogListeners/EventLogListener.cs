using System.Diagnostics;
using Listeners;

namespace EventLogListeners
{
    /// <summary>
    /// EventLogListener for logging into Windows Event Log.
    /// </summary>
    public class EventLogListener : IListener
    {
        private readonly object _lockObject = new();
        private readonly string _sourceName = "Assembly and Metadata";
        private readonly ListenerOptions _options;

        public EventLogListener(ListenerOptions options)
        {
            _options = options;
            if (!EventLog.SourceExists(_sourceName))
            {
                EventLog.CreateEventSource(_sourceName, "Assembly and Metadata");
            }
        }

        public void LogMessage(string message)
        {
            if (!IsLogLevelEnabled(message)) return;
            lock (_lockObject)
            {
                EventLog.WriteEntry(_sourceName, message, EventLogEntryType.Information);
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
    }
}
