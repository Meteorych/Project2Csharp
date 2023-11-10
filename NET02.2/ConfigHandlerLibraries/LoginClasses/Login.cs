using System.Collections;

namespace ConfigHandlerLibraries.LoginClasses
{
    /// <summary>
    /// Class that support Login objects with all their attributes.
    /// </summary>
    public class Login : IEnumerable
    {
        public string Name { get; private set; } = "empty config";
        public List<Window> Windows { get; private set; } = new List<Window>();

        public bool RightConfig { get; private set; } = true;
        public Login(){}

        public Login(string name, List<Window> windowsList)
        {
            Name = name;
            Windows = windowsList;
            if (Windows.Any(window => window.RightConfig == false))
            {
                RightConfig = false;
            }
        }

        public void Add(Window windowConfig)
        {
            Windows.Add(windowConfig);
        }
        public IEnumerator<Window> GetEnumerator()
        {
            return Windows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
