namespace XMLandJSON.Repository
{
    /// <summary>
    /// Interface that setup methods for Repository class
    /// </summary>
    public interface IRepository
    {
        public void Upload(string fileWay);
        public void Dump(string extensionType);
        public void Delete(string name);
    }
}
