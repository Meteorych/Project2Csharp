using ConfigHandlerLibraries;
using ConfigHandlerLibraries.LoginClasses;

namespace XMLandJSON.Repository
{
    /// <summary>
    /// Class for interim data container
    /// </summary>
    public class Repository : IRepositoryWriter, IRepositoryOperations
    {
        public LoginsConfig GetConfig { get; }

        /// <summary>
        /// Method for upload data info in repository
        /// </summary>
        /// <param name="configParser">Parser of data from the datafile</param>
        /// <param name="uploadFilePath">Name of the datafile</param>
        public Repository(IConfigurable configParser, string uploadFilePath)
        {
            GetConfig = new LoginsConfig(configParser.LoginsDataUpload(uploadFilePath));
        }

        public void Delete(string userName)
        {
            GetConfig.LoginList.RemoveAt(GetConfig.LoginList.FindIndex(a => a.Name == userName));
        }
        /// <summary>
        /// Update login data with new elements by parsing extra file.
        /// </summary>
        /// <param name="updateFilePath"></param>
        /// <param name="configParser"></param>
        public void Update(string updateFilePath, IConfigurable configParser)
        {
            GetConfig.LoginList.AddRange(configParser.LoginsDataUpload(updateFilePath));
        }
        /// <summary>
        /// Method for dumping data info in repository
        /// </summary>
        /// <param name="configParser">Parser of data to the datafile</param>
        public void Save(IConfigurable configParser)
        {
            configParser.LoginsDataDump(GetConfig);
        }
    }
}
