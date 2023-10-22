namespace XmlAndJson.LoginClasses
{
    /// <summary>
    /// Class for Window objects, that support their attributes.
    /// </summary>
    public class Window
    {
        public Dictionary<string, int> Attributes { get; private set; } = new();
        public Window()
        {
            
        }

    }
}
