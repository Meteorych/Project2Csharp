using ConfigHandlerLibraries;
using ConfigHandlerLibraries.LoginClasses;

namespace XMLandJSON.Repository
{
    public class RepositoryData : IRepository
    {
        public LoginsConfig? Config { get; private set; }

        public void Upload(string fileWay)
        {
            Config = new LoginsConfig(XmlConfigHandler.LoginDataHandler(fileWay));
        }
        public void Dump(string extensionType)
        {
            switch (extensionType)
            {
                case "json":
                    if (Config != null) JsonSerialization.ConfigSerialization(Config);
                    break;
            }

        }
    }
}
