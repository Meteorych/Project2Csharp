using ConfigHandlerLibraries;

namespace XMLandJSON.Repository
{
    public interface IRepositoryReader
    {
        public void Upload(IConfigurable configParser, string uploadFileName);
    }
}
