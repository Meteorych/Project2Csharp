using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ConfigHandlerLibraries.LoginClasses
{
    /// <summary>
    /// Class for Window objects, that support their attributes.
    /// </summary>
    public class Window
    {
        public string Title { get; set; } = "";
        [XmlIgnore] // Ignore for serialization
        public Dictionary<string, string> Attributes { get; set; } = new();
        [JsonIgnore]
        public List<AttributeItem> AttributesXml
        {
            get
            {
                return Attributes.Select(attribute => new AttributeItem { Key = attribute.Key, Value = attribute.Value }).ToList();
            }
            set
            {
                Attributes = new Dictionary<string, string>();
                foreach (var item in value)
                {
                    Attributes[item.Key] = item.Value;
                }
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        public bool RightConfig { get;}
        public Window() { }
        public Window(string title, string top, string left, string width, string height, bool rightConfig = true)
        {
            Title = title;
            Attributes["top"] = top;
            Attributes["left"] = left;
            Attributes["width"] = width;
            Attributes["height"] = height;
            RightConfig = rightConfig;
        }
    }

    public class AttributeItem
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
    }

}
