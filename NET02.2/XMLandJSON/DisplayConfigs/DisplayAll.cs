﻿using XMLandJSON.LoginClasses;

namespace XMLandJSON.DisplayConfigs
{
    public class DisplayAll
    {
        /// <summary>
        /// Method for displaying all logins.
        /// </summary>
        /// <param name="logins"></param>
        public static void DisplayConfigs (LoginsConfig logins)
        {
            foreach (var login in logins)
            {
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
