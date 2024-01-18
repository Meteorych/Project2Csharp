namespace AbstractListener;

/// <summary>
/// Private struct to store ListenerOptions for several operations.
/// In the future development should be rewritten.
/// </summary>
public struct ListenerOptions
{
    /// <summary>
    /// Type of Listener option
    /// </summary>
    public string? ListenerType { get; set; }

    /// <summary>
    /// String that defines path to log file
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// Minimum log level of listener.
    /// </summary>
    public LogLevels MinimumLogLevel { get; set; }
}