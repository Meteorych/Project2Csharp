﻿using System.Collections;

namespace XmlAndJson.LoginClasses
{
    /// <summary>
    /// Class for Window objects, that support their attributes.
    /// </summary>
    public class Window
    {
        public string Title { get;}
        public Dictionary<string, string> Attributes { get;} = new();
        public bool RightConfig { get;}
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
