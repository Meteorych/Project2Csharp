using System.Runtime.CompilerServices;
using System.Xml;

[assembly: InternalsVisibleTo("MyTest")]
namespace XmlAndJson
{
    internal class XMLConnection
    {
        private XmlDocument _xDoc = new();

        public XMLConnection(string xmlWay)
        {
            _xDoc.LoadXml(xmlWay);
            var xRoot = _xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xNode in xRoot)
                {
                    
                }
            }
        }
    }
}
