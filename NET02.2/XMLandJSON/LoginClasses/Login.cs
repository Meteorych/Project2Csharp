namespace XmlAndJson.LoginClasses
{
    internal class Login
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
