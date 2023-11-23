using System.Collections;
using System.Collections.ObjectModel;
using System.Text;

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
        /// Overriding of string method
        /// </summary>
        /// <returns>Redirecting it to the string method with new string with bool parameter substituting default value "true"</returns>
        public override string ToString()
        {
            return ToString(true);
        }

        /// <summary>
        /// New private string method to realization of different kinds of displaying according of requirements to the program
        /// </summary>
        /// <returns>Returns all config parameters in string</returns>
        private string ToString(bool all)
        {
            var result = new StringBuilder();
            if (all)
            {
                foreach (var login in LoginList)
                {
                    result.Append($"Login: {login.Name}");
                    foreach (var window in login)
                    {
                        result.Append($"\n\t{window.Title}: {window.Attributes["top"]}, {window.Attributes["left"]}, " +
                                      $"{window.Attributes["width"]}, {window.Attributes["height"]}");
                    }
                }
            }
            else
            {
                result.Append("Logins with wrong configuration:");
                foreach (var login in LoginList.Where(login => !login.RightConfig))
                {
                    result.Append($"\nLogin: {login.Name}");
                    foreach (var window in login)
                    {
                        result.Append($"\n\t{window.Title}: {window.Attributes["top"]}, {window.Attributes["left"]}, " +
                                          $"{window.Attributes["width"]}, {window.Attributes["height"]}");
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Method for displaying configs of all logins.
        /// </summary>
        public void DisplayConfigs()
        {
            Console.WriteLine($"{this}");
        }

        /// <summary>
        /// Method for displaying logins with wrong configuration.
        /// </summary>
        public void DisplayWrongConfigs()
        {
            Console.WriteLine($"{this.ToString(false)}");
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
