using System.Runtime.InteropServices.ComTypes;
using ConfigHandlerLibraries;
using XMLandJSON.Repository;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        { 
            //Ну сделать Data Access layer...(да, я знаю, не хочется) или переделать XML сериализацию в нормальный XML
            var data = new RepositoryData();
            const string filename = "Config.xml";
            switch (Path.GetExtension("Config.xml"))
            {
                case ".xml":
                    data.Upload(new XmlConfig(), filename);
                    break;
                case ".json":
                    data.Upload(new JsonConfig(), filename);
                    break;
            };
            const string extensionType = ".json";
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