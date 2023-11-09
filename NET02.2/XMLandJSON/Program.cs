using ConfigHandlerLibraries;
using ConfigHandlerLibraries.LoginClasses;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        {
            //перевести на интерфейсы 
            LoginsConfig logins = new(XmlConfigHandler.LoginDataHandler("Config.xml"));
            logins.DisplayConfigs();
            logins.DisplayWrongConfigs();
            JsonSerialization.ConfigSerialization(logins);
        }
    }
}