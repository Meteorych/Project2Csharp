using ConfigHandlerLibraries;
using ConfigHandlerLibraries.LoginClasses;

namespace XMLandJSON.Repository
{
    /// <summary>
    /// Class for interim data container
    /// </summary>
    public class RepositoryData : IRepository
    {
        private LoginsConfig? _config;
        public LoginsConfig Config => _config ?? new LoginsConfig(new List<Login>());
        /// <summary>
        /// Method for upload data info in repository
        /// </summary>
        /// <param name="fileWay"></param>
        public void Upload(string fileWay)
        {
            _config = Path.GetExtension(fileWay) switch
            {
                ".xml" => new LoginsConfig(XmlConfig.LoginDataUpload(fileWay)),
                ".json" => new LoginsConfig(JsonConfig.LoginDataUpload(fileWay)),
                _ => _config
            };
        }
        /// <summary>
        /// Method for dumping data info in repository
        /// </summary>
        /// <param name="extensionType"></param>
        public void Dump(string extensionType)
        {
            switch (extensionType)
            {
                case "json":
                    JsonConfig.LoginsDataDump(Config);
                    break;
                case "xml":
                    XmlConfig.LoginsDataDump(Config);
                    break;
            }

        }
    }
}
