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
            lock (_lockObject)
            {
                EventLog.WriteEntry(_sourceName, message, EventLogEntryType.Information);
            }
           
        }
    }
}
