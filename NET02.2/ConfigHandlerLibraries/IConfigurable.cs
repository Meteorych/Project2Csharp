using ConfigHandlerLibraries.LoginClasses;

namespace ConfigHandlerLibraries;
/// <summary>
/// Interface for classes that realize work with datafiles
/// </summary>
public interface IConfigurable
{
    public List<Login> LoginsDataUpload(string uploadFileName);
    public void LoginsDataDump(LoginsConfig? loginsData);
}

