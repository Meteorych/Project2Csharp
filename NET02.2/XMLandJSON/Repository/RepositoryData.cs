using ConfigHandlerLibraries;
using ConfigHandlerLibraries.LoginClasses;

namespace XMLandJSON.Repository
{
    /// <summary>
    /// Class for interim data container
    /// </summary>
    public class RepositoryData : IRepositoryWriter, IRepositoryReader
    {
        private LoginsConfig? _config;
        public LoginsConfig Config => _config ?? new LoginsConfig(new List<Login>());

        /// <summary>
        /// Method for upload data info in repository
        /// </summary>
        /// <param name="configParser">Parser of data from the datafile</param>
        /// <param name="uploadFileName">Name of the datafile</param>
        public void Upload(IConfigurable configParser, string uploadFileName)
        {
            _config = new LoginsConfig(configParser.LoginsDataUpload(uploadFileName));
        }

        public void Delete(string userName)
        {
            Config.LoginList.RemoveAt(Config.LoginList.FindIndex(a => a.Name == userName));
        }

        /// <summary>
        /// Method for dumping data info in repository
        /// </summary>
        /// <param name="configParser">Parser of data to the datafile</param>
        public void Dump(IConfigurable configParser)
        {
            configParser.LoginsDataDump(_config);
        }
    }
}
