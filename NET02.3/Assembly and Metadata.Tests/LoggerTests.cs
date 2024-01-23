using System.Text;
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
            // TODO: Doesn't working: right way will be to correctly mock Configuration.
            var configurationText = @"{
                                          ""Listeners"": [
                                            {
                                              ""ListenerType"": ""TextListeners.TextListener, TextListeners"",
                                              ""FilePath"": ""log.txt"",
                                              ""MinimumLogLevel"": ""Info""
                                            }
                                        ]
                                       }";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(configurationText)))
                .Build();
            var textListenerMock = new Mock<IListener>();
            var logger = new MyLogger(configuration);

            // Act
            logger.InitializeListeners();
            logger.LogMessage("DEBUG||This isn't working");

            // Assert
            textListenerMock.Verify(listener => listener.LogMessage("INFO||This isn't working"), Times.Never);
        }

        [Fact]
        public void LogDebugMessage_WhenMinLogLevelIsDebug_Log()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void TrackObject_TrackObjectPerson_LogTraceInfoAboutIt()
        {
            throw new NotImplementedException();
        }
    }
}