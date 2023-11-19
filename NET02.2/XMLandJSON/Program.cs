using ConfigHandlerLibraries;
using XMLandJSON.Repository;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        { 
            var data = new RepositoryData();
            data.Upload("Config.xml");
            data.Config.DisplayConfigs();
            data.Config.DisplayWrongConfigs();
            XmlConfig.LoginsDataDump(data.Config);
        }
    }
}