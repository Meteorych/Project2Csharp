using Microsoft.Extensions.Configuration;
using NET02._4.Crawler;
using NLog;

namespace NET02._4.Tests
{
    public class UnitTest1
    {
        private readonly IConfiguration _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        private readonly Logger _logger = LogManager.GetLogger("Crawling Logger");

        [Fact]
        public void Test1()
        {
            //TODO: Write tests in the future
        }
    }
}