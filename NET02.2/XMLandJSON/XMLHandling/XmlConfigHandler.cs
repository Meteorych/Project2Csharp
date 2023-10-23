using System.Xml;
using XmlAndJson.LoginClasses;

namespace XmlAndJson.XMLHandling
{
    /// <summary>
    /// Derived class from XMLHandler that handles "Login" config files.
    /// </summary>
    public class XmlConfigHandler : XmlHandler
    {
        public List<Login> Logins { get; } = new();

        public XmlConfigHandler(string xmlWay) : base(xmlWay)
        {
            LoginDataHandler();
        }
        /// <summary>
        /// Method for handling "Logins" data from XML-file.
        /// </summary>
        public void LoginDataHandler()
        {
            var loginNodes = XRoot.SelectNodes("//login");
            if (loginNodes is null) return;
            foreach (XmlElement loginElement in loginNodes)
            {
                var loginName = loginElement.GetAttribute("name");
                var loginWindowsList = WindowDataHandler(loginElement);
                Logins.Add(new Login(loginName, loginWindowsList));
            }
        }
        /// <summary>
        /// Method for handling "Windows" data from XML-file.
        /// </summary>
        /// <param name="loginElement"></param>
        /// <returns></returns>
        private List<Window> WindowDataHandler(XmlNode loginElement)
        {
            List<Window> windows = new();
            foreach (XmlElement windowElement in loginElement.SelectNodes("window"))
            {
                var title = windowElement.Attributes["title"]?.Value;
                var top = Convert.ToInt32(windowElement.SelectSingleNode("top")?.InnerText);
                var left = Convert.ToInt32(windowElement.SelectSingleNode("left")?.InnerText);
                var width = Convert.ToInt32(windowElement.SelectSingleNode("width")?.InnerText);
                var height = Convert.ToInt32(windowElement.SelectSingleNode("height")?.InnerText);
                Window window = new(title, top, left, width, height);
                windows.Add(window);
            }
            return windows;
        }
    }
}
