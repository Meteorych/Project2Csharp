using System.Runtime.InteropServices;
using XmlAndJson.LoginClasses;
using XmlAndJson.XMLHandling;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main()
        {
            var Handler = new XmlConfigHandler("Config.xml");
            Console.WriteLine($"{Handler.Logins[0].Windows[1].Attributes["width"]}");
        }
    }
}