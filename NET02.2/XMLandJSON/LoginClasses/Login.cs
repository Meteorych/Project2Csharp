namespace XmlAndJson.LoginClasses
{
    /// <summary>
    /// Class that support Login objects with all their attributes.
    /// </summary>
    public class Login
    {
        public string Name { get; }
        public List<Window> Windows { get; }

        public Login(string name, List<Window> windowsList)
        {
            Name = name;
            Windows = windowsList;
        }
    }
}
