using System.Text;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office.Word;
using Listener;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Assembly_and_Metadata.Tests
{
    public class LoggerTests
    {
        [Fact]
        public void LogDebugMessage_WhenMinLogLevelIsInfo_DoNotLog()
        {

            var configurationText = @"{
                                          ""Listeners"": [
                                            {
                                              ""ListenerType"": ""TextListeners.TextListener, TextListeners"",
                                              ""FilePath"": ""log1.txt"",
                                              ""MinimumLogLevel"": ""Info""
                                            }
                                        ]
                                       }";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(configurationText)))
                .Build();
            var logger = new MyLogger(configuration);
            var message = "DEBUG||This isn't working";

            // Act
            logger.InitializeListeners();
            logger.LogMessage(message);

            // Assert
            Assert.Empty(File.ReadAllLines(configuration.GetSection("Listeners:0")["FilePath"]));
        }

        [Fact]
        public void LogDebugMessage_WhenMinLogLevelIsDebug_Log()
        {
            var configurationText = @"{
                                          ""Listeners"": [
                                            {
                                              ""ListenerType"": ""TextListeners.TextListener, TextListeners"",
                                              ""FilePath"": ""log2.txt"",
                                              ""MinimumLogLevel"": ""Info""
                                            }
                                        ]
                                       }";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(configurationText)))
                .Build();
            var logger = new MyLogger(configuration);
            var message = "INFO || This isn't working";

            // Act
            logger.InitializeListeners();
            logger.LogMessage(message);

            // Assert
            Assert.Contains(message, File.ReadAllLines(configuration.GetSection("Listeners:0")["FilePath"])[0]);
        }

        [Fact]
        public void TrackObject_TrackObjectPerson_LogTraceInfoAboutIt()
        {
            throw new NotImplementedException();
        }
    }
}