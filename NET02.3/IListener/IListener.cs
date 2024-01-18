namespace AbstractListener
{
    /// <summary>
    /// IListener interface that have compulsory method for logger listeners.
    /// </summary>
    public interface IListener
    {
        public void LogMessage(string message);
    }
}
