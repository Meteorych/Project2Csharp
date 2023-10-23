using System.Runtime.CompilerServices;
using System.Xml;
using XmlAndJson.LoginClasses;


namespace XmlAndJson.XMLHandling
{
    /// <summary>
    /// Class to handle XML files. There are basic methods to create XML-file object.
    /// </summary>
    public abstract class XmlHandler
    {
        private readonly XmlDocument _xDoc = new();
        protected readonly XmlElement XRoot;

        protected XmlHandler(string xmlWay)
        {
            _xDoc.Load(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Config\", xmlWay));
            XRoot = _xDoc.DocumentElement ?? throw new ArgumentNullException(nameof(xmlWay), message: "Empty or wrong file");
        }
    }
}
