using System.Runtime.CompilerServices;
using System.Xml;
using XmlAndJson.LoginClasses;


namespace XmlAndJson
{
    /// <summary>
    /// Class to handle XML file with configuration
    /// </summary>
    public class XmlConfigHandler
    {
        private readonly XmlDocument _xDoc = new();

        public XmlConfigHandler(string xmlWay)
        {
            _xDoc.LoadXml(xmlWay);
            

        }

        public void HandlingLoginData()
        {
            var xRoot = _xDoc.DocumentElement ?? throw new ArgumentNullException(nameof(xmlWay), message: "Empty or wrong file");
            //var login = new Login(xLogin.GetAttribute("title"));
            //foreach (XmlElement xWin )
            //{

            //}
        }
    }
}
