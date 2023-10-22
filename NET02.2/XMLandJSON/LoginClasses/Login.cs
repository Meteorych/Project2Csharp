namespace XmlAndJson.LoginClasses
{
    /// <summary>
    /// Class that support Login objects with all their attributes.
    /// </summary>
    public class Login
    {
        public string Name { get; }
        public List<Window> Windows { get; }

        public Login(string name)
        {
            Name = name;
            Windows = new List<Window>();
        }

        public void NewWindow(Window window)
        {
            Windows.Add(window);
        }
    }
}
