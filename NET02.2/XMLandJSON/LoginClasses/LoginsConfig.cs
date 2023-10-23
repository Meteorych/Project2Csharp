using System.Net;
using System.Runtime.CompilerServices;
using XmlAndJson.LoginClasses;

namespace XMLandJSON.LoginClasses
{
    public class LoginsConfig
    {
        public List<Login> LoginList { get; }

        public LoginsConfig(List<Login> loginList)
        {
            LoginList = loginList;
        }

        public Login this[int num] => LoginList[num];
    }
}
