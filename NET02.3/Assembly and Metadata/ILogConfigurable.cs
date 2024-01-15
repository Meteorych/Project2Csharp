namespace Assembly_and_Metadata;

public interface ILogConfigurable
{
    public LogLevels GetLogMinimumLevel { get; }
}