using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.Configuration;
using Moq;
using NET02._4.Crawler;
using NLog;

namespace NET02._4.Tests
{
    public class UnitTest1
    {
        private readonly Mock<ILogger> _logger = new();

        [Fact]
        public void Test1()
        {
            var config = Mock.Of<IConfiguration>(
                config => config["Url"] == "https://www.youtube.com" &&
                          config["Timeout"] == "00:00:02" &&
                          config["MaxWaitingTime"] == "00:00:03" &&
                          config["MailAddress"] == "super.titlov@inbox.ru" &&
                          config["AdministratorName"] == "Ivan Titlov");
            var crawler = new WebCrawler(config, _logger.Object);

            //Act
            var startTime = DateTime.Now;
            do
            {
                crawler.Start();
            } while ((DateTime.Now - startTime).Milliseconds < 100);
            crawler.Stop();

            //TODO: Write tests

            _logger.Verify(logger => logger.Info(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}