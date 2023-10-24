using Microsoft.VisualStudio.TestTools.UnitTesting;
using XmlAndJson.XMLHandling;
using XMLandJSON.LoginClasses;

namespace XMLandJSONTests
{
    [TestClass]
    public class XmlConfigHandlerTests
    {
        [TestMethod]
        public void WindowsEmptyArgsTest()
        {
            const string expectedValue = "?";
            LoginsConfig testLogins = new(XmlConfigHandler.LoginDataHandler("TestConfig.xml"));
            Assert.AreEqual(expectedValue, testLogins[0].Windows[1].Attributes["width"]);
        }

        [TestMethod]
        public void LoginDataHandlerTest()
        {
            const bool expectedValue = false;
            LoginsConfig testLogins = new(XmlConfigHandler.LoginDataHandler("TestConfig.xml"));
            Assert.AreEqual(expectedValue, testLogins[1].RightConfig);
        }
    }
}