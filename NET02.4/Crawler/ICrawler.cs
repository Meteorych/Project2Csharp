namespace NET02._4.Crawler;

/// <summary>
/// Crawler's interface with public methods.
/// </summary>
public interface ICrawler : IDisposable
{
    public Task Start(CancellationToken token);
}