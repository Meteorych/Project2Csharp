using ConfigHandlerLibraries;

namespace XMLandJSON.Repository
{
    public interface IRepositoryOperations
    {
        public void Delete(string userName);
        public void Update(string updateFilePath, IConfigurable config);
    }
}
