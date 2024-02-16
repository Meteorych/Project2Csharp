//using Microsoft.Extensions.Configuration;
//using Moq;
//using NET02._4.Crawler;
//using NLog;

//namespace NET02._4.Tests
//{
//    public class OldUnitTest1
//    {
//        private readonly Mock<ILogger> _logger = new();

//        [Fact]
//        public async Task CheckSite_CheckingWorkingSite_LogMessage()
//        {
//            //Arrange
//            var config = Mock.Of<IConfiguration>(
//                config => config["Url"] == "https://www.youtube.com" &&
//                          config["Timeout"] == "00:00:02" &&
//                          config["MaxWaitingTime"] == "00:00:03" &&
//                          config["MailAddress"] == "super.titlov@inbox.ru" &&
//                          config["AdminName"] == "Ivan Titlov");
//            var crawler = new WebCrawler(config, _logger.Object);

//            //Act
//            await Task.Run(async () =>
//            {
//                crawler.Start();
//                await Task.Delay(3000); // Let the crawler run for 10 seconds
//                crawler.Stop();
//            });

//            //Assert
//            _logger.Verify(logger => logger.Info("Site is working properly."), Times.AtLeastOnce);
//        }

//        [Fact]
//        public void CrawlerCreation_ConfigurationParameterEqualsNull_LogError()
//        {
//            //Arrange
//            var config = Mock.Of<IConfiguration>(
//                config => config["Url"] == "https://www.youtube.com" &&
//                          config["Timeout"] == "00:00:02" &&
//                          config["MaxWaitingTime"] == "00:00:03" &&
//                          config["AdminName"] == "Ivan Titlov");


//            //Act
//            WebCrawler TestAction() => new(config, _logger.Object);

//            Assert.Throws<ArgumentNullException>(((Func<WebCrawler>?)TestAction)!);
//        }

//        [Fact]
//        public async Task CheckSite_CheckingInaccessibleSite_LogError()
//        {
//            //Arrange
//            var config = Mock.Of<IConfiguration>(
//                config => config["Url"] == "https://www.allgame.com/game.php?id=49265&tab=credits" &&
//                          config["Timeout"] == "00:00:02" &&
//                          config["MaxWaitingTime"] == "00:00:03" &&
//                          config["MailAddress"] == "super.titlov@inbox.ru" &&
//                          config["AdminName"] == "Ivan Titlov");
//            var crawler = new WebCrawler(config, _logger.Object);


//            //Act
//            await Task.Run(async () =>
//            {
//                crawler.Start();
//                await Task.Delay(3000); // Let the crawler run for 10 seconds
//                crawler.Stop();
//            });

//            //Assert
//            _logger.Verify(logger => logger.Error("Can't send email: Object reference not set to an instance of an object."), Times.AtLeastOnce);
//        }
//    }
//}