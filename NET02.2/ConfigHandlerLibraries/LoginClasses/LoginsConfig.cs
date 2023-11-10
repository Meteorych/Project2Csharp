using System.Collections;
using System.Collections.ObjectModel;

namespace ConfigHandlerLibraries.LoginClasses
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

        /// <summary>
        /// Method for displaying configs of all logins.
        /// </summary>
        public void DisplayConfigs()
        {
            foreach (var login in LoginList)
            {
                Console.WriteLine($"Login: {login.Name}");
                foreach (var window in login)
                {
                    Console.WriteLine($"\t{window.Title}: {window.Attributes["top"]}, {window.Attributes["left"]}, " +
                                      $"{window.Attributes["width"]}, {window.Attributes["height"]}");
                }
            }
        }
        /// <summary>
        /// Method for displaying logins with wrong configuration.
        /// </summary>
        public void DisplayWrongConfigs()
        {
            Console.WriteLine("Logins with wrong configuration:");
            foreach (var login in LoginList.Where(login => !login.RightConfig))
            {
                Console.WriteLine($"Login: {login.Name}");
                foreach (var window in login)
                {
                    Console.WriteLine($"\t{window.Title}: {window.Attributes["top"]}, {window.Attributes["left"]}, " +
                                      $"{window.Attributes["width"]}, {window.Attributes["height"]}");
                }
            }
        }
        /// <summary>
        /// Default values for attributes during Serialization.
        /// </summary>
        public static readonly ReadOnlyDictionary<string, string> DefaultValues = new(new Dictionary<string, string>
        {
            { "top", "0" },
            { "left", "0" },
            { "width", "400" },
            { "height", "150" },
        });
    }
}
