using System.Text.Json;
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
        public List<Login> LoginsDataUpload(string uploadFileName)
        {
            var xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Config\", uploadFileName));
            var xRoot = xDoc.DocumentElement ?? throw new ArgumentNullException(nameof(uploadFileName), message: "Empty or wrong file");
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
        //To XML through Serialization (don't like this method because you have to change your class structure drastically
        //public void LoginsDataDump(LoginsConfig? config)
        //{
        //    XmlSerializer xmlSerializer = new(typeof(Login));
        //    if (config is null) return;
        //    foreach (var login in config)
        //    {
        //        var path = (Path.Combine(Environment.CurrentDirectory, @$"..\..\..\Config\{login.Name}.xml;"));
        //        if (login.Windows.Any(window => window.Attributes.ContainsValue("?")))
        //        {
        //            login.Windows.ForEach(window =>
        //            {
        //                window.Attributes = window.Attributes.ToDictionary(pair => pair.Key, pair => LoginsConfig.DefaultValues.ContainsKey(pair.Key) && pair.Value == "?" ? LoginsConfig.DefaultValues[pair.Key] : pair.Value);
        //            });
        //        }
        //        xmlSerializer.Serialize(new FileStream(path, FileMode.Create), login);
        //    }
        //}
        public void LoginsDataDump(LoginsConfig? logins)
        {
            {

                if (logins is null) return;
                foreach (var login in logins.LoginList)
                {
                    login.LoginNullToDefault();
                    var xDoc = new XmlDocument();
                    var rootNode = xDoc.CreateElement("Login");
                    xDoc.AppendChild(rootNode);
                    rootNode.SetAttribute("name", login.Name);

                    foreach (var window in login.Windows)
                    {
                        var windowElement = xDoc.CreateElement("Window");
                        windowElement.SetAttribute("title", window.Title);

                        var topNode = xDoc.CreateElement("top");
                        topNode.InnerText = window.Attributes["top"];
                        windowElement.AppendChild(topNode);

                        var leftNode = xDoc.CreateElement("left");
                        leftNode.InnerText = window.Attributes["left"];
                        windowElement.AppendChild(leftNode);

                        var widthNode = xDoc.CreateElement("width");
                        widthNode.InnerText = window.Attributes["width"];
                        windowElement.AppendChild(widthNode);

                        var heightNode = xDoc.CreateElement("height");
                        heightNode.InnerText = window.Attributes["height"];
                        windowElement.AppendChild(heightNode);

                        rootNode.AppendChild(windowElement);
                    }

                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, @$"..\..\..\Config\{login.Name}.xml;"));
                }
            }
        }

    }
}
