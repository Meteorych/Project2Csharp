using Listener;

namespace TextListeners
{
    /// <summary>
    /// TextListener class for logging on *.txt files
    /// </summary>
    public class TextListener : AbstractListener
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
            Options = options;
            _filePath = Options.FilePath;
        }

        public override void LogMessage(string message)
        {
            if (!IsLogLevelEnabled(message)) return;
            lock (_lockObject)
            {
                try
                {
                    File.AppendAllText(_filePath ?? throw new InvalidOperationException(), $"{DateTime.Now}: {message}\n");
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
