using System.Runtime.InteropServices.ComTypes;
using ConfigHandlerLibraries;
using XMLandJSON.Repository;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        { 
            var filename = "Config.xml";
            Repository? data;
            switch (Path.GetExtension("Config.xml"))
            {
                case ".xml":
                    data = new Repository(new XmlConfig(), filename);
                    break;
                case ".json":
                    data = new Repository(new JsonConfig(), filename);
                    break;
                default:
                    data = null;
                    break;
            };
            data.GetConfig.DisplayConfigs();
            data.GetConfig.DisplayWrongConfigs();
            var extensionType = ".xml";
            switch (extensionType)
            {
                case ".xml":
                    data.Save(new XmlConfig());
                    break;
                case ".json":
                    data.Save(new JsonConfig());
                    break;
            }
        }
    }
}