namespace XmlAndJson.LoginClasses
{
    /// <summary>
    /// Class for Window objects, that support their attributes.
    /// </summary>
    public class Window
    {
        public string Title { get;}
        public Dictionary<string, int?> Attributes { get; private set; } = new();
        public Window(string title, int top, int left, int width, int height)
        {
            Title = title;
            Attributes["top"] = top;
            Attributes["left"] = left;
            Attributes["width"] = width;
            Attributes["height"] = height;
        }

    }
}
