using System.Diagnostics;
using Listener;

namespace EventLogListeners
{
    /// <summary>
    /// EventLogListener for logging into Windows Event Log.
    /// </summary>
    public class EventLogListener : AbstractListener
    {
        private readonly object _lockObject = new();
        private readonly string _sourceName = "Assembly and Metadata";

        public EventLogListener(ListenerOptions options)
        {
            Options = options;
            if (!EventLog.SourceExists(_sourceName))
            {
                EventLog.CreateEventSource(_sourceName, "Assembly and Metadata");
            }
        }

        public override void LogMessage(string message)
        {
            if (!IsLogLevelEnabled(message)) return;
            lock (_lockObject)
            {
                EventLog.WriteEntry(_sourceName, message, EventLogEntryType.Information);
            }
           
        }
    }
}
