using ConfigHandlerLibraries;

namespace XMLandJSON.Repository
{
    /// <summary>
    /// Interface that setup methods for Repository class
    /// </summary>
    public interface IRepositoryWriter
    {
        public void Save(IConfigurable configParser);
    }
}
