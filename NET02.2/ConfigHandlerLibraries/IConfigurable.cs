using ConfigHandlerLibraries.LoginClasses;

namespace ConfigHandlerLibraries;
/// <summary>
/// Interface for classes that realize work with datafiles
/// </summary>
public interface IConfigurable
{
    public List<Login> LoginDataUpload(string uploadFileName);
    public void LoginsDataDump(LoginsConfig? loginsData);
}

