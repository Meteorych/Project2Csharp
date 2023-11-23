using System.Collections;

namespace ConfigHandlerLibraries.LoginClasses
{
    /// <summary>
    /// Class that support Login objects with all their attributes.
    /// </summary>
    public class Login : IEnumerable
    {
        public string Name { get; private set; }
        public List<Window> Windows { get;}

        public bool RightConfig { get; private set; } = true;

        public Login(string name, List<Window> windowsList)
        {
            Name = name;
            Windows = windowsList;
            if (Windows.Any(window => window.RightConfig == false))
            {
                RightConfig = false;
            }
        }

        public void LoginNullToDefault()
        {
            if (Windows.Any(window => window.Attributes.ContainsValue("?")))
            {
                Windows.ForEach(window =>
                {
                    foreach (var key in window.Attributes.Keys.Where(key => window.Attributes[key] == "?"))
                    {
                        window.Attributes[key] = LoginsConfig.DefaultValues[key];
                    }
                });
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
