using Listeners;

namespace TextListeners
{
    /// <summary>
    /// TextListener class for logging on *.txt files
    /// </summary>
    public class TextListener : IListener
    {
        private readonly object _lockObject = new();
        private readonly string _filePath;

        public EventHandler<EventListenerArgs>? Events;

        public TextListener(ListenerOptions options)
        {
            if (string.IsNullOrEmpty(options.FilePath))
            {
                throw new ArgumentNullException(nameof(options.FilePath), "File path can't be empty");
            }
            _filePath = options.FilePath;
        }

        public void LogMessage(string message)
        {
            lock (_lockObject)
            {
                try
                {
                    File.AppendAllText(_filePath, $"{DateTime.Now}: {message}\n");
                }
                catch (Exception ex)
                {
                    OnEvent(new EventListenerArgs(ex.Message));
                }
            }
        }

        protected void OnEvent(EventListenerArgs e)
        {
            Events?.Invoke(this, e);
        }
    }
}
