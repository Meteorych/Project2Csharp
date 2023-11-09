namespace XMLandJSON.Repository
{
    public interface IRepository
    {
        public void Upload(string fileWay);
        public void Dump(string extensionType);
    }
}
