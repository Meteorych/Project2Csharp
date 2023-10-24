using System.Collections;
using XmlAndJson.LoginClasses;

namespace XMLandJSON.LoginClasses
{
    /// <summary>
    /// Class for collection of configurations of Logins.
    /// </summary>
    public class LoginsConfig : IEnumerable
    {
        public List<Login> LoginList { get; }

        public LoginsConfig(List<Login> loginList)
        {
            LoginList = loginList;
        }

        public Login this[int num] => LoginList[num];
        public IEnumerator<Login> GetEnumerator()
        {
            return LoginList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
