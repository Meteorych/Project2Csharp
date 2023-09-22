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
        [TestMethod]
        public void ReleaseDateParsingTest () 
        {
            DateOnly expected = new(2003, 9, 23);
            string releaseDate = "2003.09.23";
            Book testBook = new Book("1234567890123", "Title", releaseDate);
            Assert.AreEqual (expected, testBook.ReleaseDate);
        }
        [TestMethod]
        public void LinqToObjectCatalogueBookTimeTest()
        {

        }
        [TestMethod]
        public void LinqToObjectCatalogueBookNameTest() 
        { 
        
        }
        [TestMethod]
        public void LinqToObjectCatalogueTupleAuthorBookCountTest()
        {
            
        }
        [TestMethod]
        public void CatalogueEnumanatorTest()
        {

        }
        [TestMethod]
        public void GetBookByISBNTest()
        {

        }
    }
}