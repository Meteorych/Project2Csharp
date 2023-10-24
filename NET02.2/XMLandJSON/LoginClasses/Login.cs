using System.Collections;
using System.Text.Json.Serialization;

namespace XmlAndJson.LoginClasses
{
    /// <summary>
    /// Class that support Login objects with all their attributes.
    /// </summary>
    public class Login : IEnumerable
    {
        public string Name { get; }
        public List<Window> Windows { get; }

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
