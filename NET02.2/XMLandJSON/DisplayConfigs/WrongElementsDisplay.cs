using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlAndJson.LoginClasses;
using XMLandJSON.LoginClasses;

namespace XMLandJSON.DisplayConfigs
{
    public class WrongElementsDisplay
    {
        public static void DisplayConfigs(LoginsConfig logins)
        {
            foreach (var login in logins)
            {
                if (login.RightConfig) continue;
                Console.WriteLine("Login with wrong configuration!");
                Console.WriteLine($"Login: {login.Name}");
                foreach (var window in login)
                {
                    Console.WriteLine($"\t{window.Title}: {window.Attributes["top"]}, {window.Attributes["left"]}, " +
                                      $"{window.Attributes["width"]}, {window.Attributes["height"]}");
                }
            }
        }
    }
}
