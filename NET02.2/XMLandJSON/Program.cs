using System.Runtime.InteropServices.ComTypes;
using ConfigHandlerLibraries;
using XMLandJSON.Repository;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        { 
            var data = new RepositoryData();
            var filename = "Config.xml";
            switch (Path.GetExtension("Config.xml"))
            {
                case ".xml":
                    data.Upload(new XmlConfig(), filename);
                    break;
                case ".json":
                    data.Upload(new JsonConfig(), filename);
                    break;
            };
            data.Config.DisplayConfigs();
            data.Config.DisplayWrongConfigs();
            var extensionType = ".xml";
            switch (extensionType)
            {
                case ".xml":
                    data.Dump(new XmlConfig());
                    break;
                case ".json":
                    data.Dump(new JsonConfig());
                    break;
            }
        }
    }
}