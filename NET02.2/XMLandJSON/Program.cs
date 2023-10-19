using XmlAndJson.LoginClasses;

namespace XmlAndJson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login login = new("fdsafs");
            login.Windows = new List<Window>();
        }
    }
}