using System.Xml;
using System.Xml.Serialization;
using ConfigHandlerLibraries.LoginClasses;

namespace ConfigHandlerLibraries
{
    /// <summary>
    /// Class that handles "Login" config files.
    /// </summary>
    public class XmlConfig : IConfigurable
    {
        /// <summary>
        /// Method for handling "Logins" data from XML-file.
        /// </summary>
        public static List<Login> LoginDataUpload(string xmlWay)
        {
            var xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Config\", xmlWay));
            var xRoot = xDoc.DocumentElement ?? throw new ArgumentNullException(nameof(xmlWay), message: "Empty or wrong file");
            var loginsList = new List<Login>();
            var loginNodes = xRoot.SelectNodes("//login");
            if (loginNodes is null) return loginsList;
            foreach (XmlElement loginElement in loginNodes)
            {
                var loginName = loginElement.GetAttribute("name");
                var loginWindowsList = WindowDataHandler(loginElement);
                loginsList.Add(new Login(loginName, loginWindowsList));
            }
            return loginsList;
        }
        /// <summary>
        /// Method for handling "Windows" data from XML-file.
        /// </summary>
        /// <param name="loginElement"></param>
        /// <returns>List of windows</returns>
        private static List<Window> WindowDataHandler(XmlNode loginElement)
        {
            List<Window> windows = new();
            foreach (XmlElement windowElement in loginElement.SelectNodes("window"))
            {
                var title = windowElement.Attributes["title"].Value;
                var topNode = windowElement.SelectSingleNode("top");
                var leftNode = windowElement.SelectSingleNode("left");
                var widthNode = windowElement.SelectSingleNode("width");
                var heightNode = windowElement.SelectSingleNode("height");
                var top = topNode != null ? topNode.InnerText : "?";
                var left = leftNode != null ? leftNode.InnerText : "?";
                var width = widthNode != null ? widthNode.InnerText : "?";
                var height = heightNode != null ? heightNode.InnerText : "?";
                if (title == "main" && (top == "?" || left == "?" || width == "?" || height == "?"))
                {
                    Window window = new(title, top, left, width, height, rightConfig: false);
                    windows.Add(window);
                }
                else
                {
                    Window window = new(title, top, left, width, height);
                    windows.Add(window);
                }
            }
            return windows;
        }

        public static void LoginsDataDump(LoginsConfig config)
        {
            XmlSerializer xmlSerializer = new(typeof(Login));
            foreach (var login in config)
            {
                var path = (Path.Combine(Environment.CurrentDirectory, @$"..\..\..\Config\{login.Name}.xml;"));
                if (login.Windows.Any(window => window.Attributes.ContainsValue("?")))
                {
                    login.Windows.ForEach(window =>
                    {
                        window.Attributes = window.Attributes.ToDictionary(pair => pair.Key, pair => LoginsConfig.DefaultValues.ContainsKey(pair.Key) && pair.Value == "?" ? LoginsConfig.DefaultValues[pair.Key] : pair.Value);
                    });
                }
                xmlSerializer.Serialize(new FileStream(path, FileMode.Create), login);
            }
        }
    }
}
