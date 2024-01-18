using Assembly_and_Metadata.TestEntities;
using Microsoft.Extensions.Configuration;

namespace Assembly_and_Metadata
{
    internal class Program
    {
        static void Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var logger = new MyLogger(config);
            logger.InitializeListeners();
            logger.LogMessage("WARN: I want something to do!");
            var person = new Person();
            logger.Track(person);
        }
    }
}