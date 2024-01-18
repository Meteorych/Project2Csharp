namespace Listener
{
    /// <summary>
    /// Abstract class with realization of some common methods and fields for Listeners.
    /// </summary>
    public abstract class AbstractListener : IListener
    {
        protected ListenerOptions Options;

        public abstract void LogMessage(string message);
        protected bool IsLogLevelEnabled(string message)
        {
            var comparisonOption = StringComparison.InvariantCultureIgnoreCase;
            switch (Options.MinimumLogLevel)
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
