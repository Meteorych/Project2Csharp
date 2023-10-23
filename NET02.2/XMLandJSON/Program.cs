using System.Runtime.InteropServices;
using XmlAndJson.LoginClasses;
using XMLandJSON.LoginClasses;
using XmlAndJson.XMLHandling;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        {
            LoginsConfig logins = new(XmlConfigHandler.LoginDataHandler("Config.xml"));
            logins.LoginList[]
        }
    }
}