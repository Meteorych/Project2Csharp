using XMLandJSON.DisplayConfigs;
using XMLandJSON.Json;
using XMLandJSON.LoginClasses;
using XmlAndJson.XMLHandling;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        {
            LoginsConfig logins = new(XmlConfigHandler.LoginDataHandler("Config.xml"));
            DisplayAll.DisplayConfigs(logins);
            WrongElementsDisplay.DisplayConfigs(logins);
            JsonSerialization.ConfigSerialization(logins);
        }
    }
}