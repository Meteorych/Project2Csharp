using ConfigHandlerLibraries;
using ConfigHandlerLibraries.LoginClasses;

namespace XMLandJSON.Repository
{
    public class RepositoryData : IRepository
    {
        private LoginsConfig? _config;
        public LoginsConfig Config => _config ?? new LoginsConfig(new List<Login>());

        public void Upload(string fileWay)
        {
            _config = Path.GetExtension(fileWay) switch
            {
                ".xml" => new LoginsConfig(XmlConfig.LoginDataHandler(fileWay)),
                ".json" => new LoginsConfig(JsonConfig.ConfigDeserialization(fileWay)),
                _ => _config
            };
        }
        public void Dump(string extensionType)
        {
            switch (extensionType)
            {
                case "json":
                    JsonConfig.ConfigSerialization(Config);
                    break;
                case "xml":
                    XmlConfig.ConfigSerialization(Config);
                    break;
            }

        }
    }
}
