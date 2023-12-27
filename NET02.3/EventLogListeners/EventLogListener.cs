using System.Diagnostics;
using Listeners;

namespace EventLogListeners
{
    /// <summary>
    /// EventLogListener for logging into Windows Event Log.
    /// </summary>
    public class EventLogListener : IListener
    {
        private readonly string _sourceName = "Assembly and Metadata";

        public EventLogListener()
        {
            if (!EventLog.SourceExists(_sourceName))
            {
                EventLog.CreateEventSource(_sourceName, "Assembly and Metadata");
            }
        }

        public void LogMessage(string message)
        {
            EventLog.WriteEntry(_sourceName, message, EventLogEntryType.Information);
        }
    }
}
