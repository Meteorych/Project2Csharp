using ConfigHandlerLibraries.LoginClasses;

namespace ConfigHandlerLibraries;
/// <summary>
/// Interface for classes that realize work with datafiles
/// </summary>
public interface IConfigurable
{
    public static abstract List<Login> LoginDataUpload(string dataFileWay);
    public static abstract void LoginsDataDump(LoginsConfig loginsData);
}

