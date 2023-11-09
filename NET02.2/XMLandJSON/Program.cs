using XMLandJSON.Json;
using XMLandJSON.LoginClasses;
using XmlAndJson.XMLHandling;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        {
            //перевести на интерфейсы 
            LoginsConfig logins = new(XmlConfigHandler.LoginDataHandler("Config.xml"));
            //JsonConfig и XMLConfig через отдельные библиотеки!
            logins.DisplayConfigs();
            logins.DisplayConfigs();
            JsonSerialization.ConfigSerialization(logins);
        }
    }
}