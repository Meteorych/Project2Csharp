using Listener;

namespace Assembly_and_Metadata
{
    public interface ILogger
    {
        public void LogMessage(string message);
        public void InitializeListeners();
        public void RegisterListener(IListener listener);
        public void Track(object trackedObject);
    }
}
