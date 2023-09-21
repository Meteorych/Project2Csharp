using BooksCatalogue.AddMethods;
using BooksCatalogue.Classes;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestingFirstNameConstraint()
        {
            int excpectedLength = 200;
            Author testAuthor1 = new($"{RandomStringCreation.RandomString(205)}", $"{RandomStringCreation.RandomString(205)}");
            Assert.AreEqual(excpectedLength, testAuthor1.FirstName.Length);
        }
        [TestMethod]
        public void TestingLastNameConstraint()
        {
            int excpectedLength = 200;
            Author testAuthor1 = new($"{RandomStringCreation.RandomString(205)}", $"{RandomStringCreation.RandomString(205)}");
            Assert.AreEqual(excpectedLength, testAuthor1.LastName.Length);
        }
    }
}