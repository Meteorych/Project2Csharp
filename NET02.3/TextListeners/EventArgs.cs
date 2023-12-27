namespace TextListeners
{
    public class EventListenerArgs : EventArgs
    {
        public string Message { get; }

        public EventListenerArgs(string message)
        {
            Message = message;
        }
    }
}
