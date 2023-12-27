using Listeners;

namespace TextListeners
{
    /// <summary>
    /// TextListener class for logging on *.txt files
    /// </summary>
    public class TextListener : IListener
    {
        private readonly string _filePath;

        public EventHandler<EventListenerArgs>? Events;

        public TextListener(string path)
        {
            _filePath = path;
        }

        public void LogMessage(string message)
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

        protected void OnEvent(EventListenerArgs e)
        {
            Events?.Invoke(this, e);
        }
    }
}
