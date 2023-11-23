using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ConfigHandlerLibraries.LoginClasses
{
    /// <summary>
    /// Class for Window objects, that support their attributes.
    /// </summary>
    public class Window
    {
        public string Title { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new();

        [JsonIgnore]
        public bool RightConfig { get;}
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="title">Title of window.</param>
        /// <param name="top"> Top coordinates.</param>
        /// <param name="left">Left coordinates.</param>
        /// <param name="width">Width coordinates.</param>
        /// <param name="height">Height coordinates.</param>
        /// <param name="rightConfig">Bool parameter that states if config is right or not.</param>
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
}
