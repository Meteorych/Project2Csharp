using ConfigHandlerLibraries;
using ConfigHandlerLibraries.LoginClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace XMLandJSONTests
{
    [TestClass]
    public class XmlConfigHandlerTests
    {
        private const string TestFilesPath = "/Files/";

        [TestMethod]
        public void WindowsEmptyArgsTest()
        {
            const string expectedValue = "?";
            LoginsConfig testLogins = new(new XmlConfig().LoginsDataUpload("TestConfig.xml"));
            Assert.AreEqual(expectedValue, testLogins[0].Windows[1].Attributes["width"]);
        }

        [TestMethod]
        public void LoginDataHandlerTest()
        {
            const bool expectedValue = false;
            LoginsConfig testLogins = new(new XmlConfig().LoginsDataUpload("TestConfig.xml"));
            Assert.AreEqual(expectedValue, testLogins[1].RightConfig);
        }

        /// <summary>
        /// Cleanup after tests.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists("Files/"))
            {
                Directory.Delete("Files/");
            }
        }
    }
}