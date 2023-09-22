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
            Book testBook = new("1234567890123", "Title", releaseDate);
            Assert.AreEqual (expected, testBook.ReleaseDate);
        }
        [TestMethod]
        public void LinqToObjectCatalogueBookTimeTest()
        {
            List<Book> testBookList = new() { new Book("1234433212343", "TestBook1", "2005.07.08"), new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08") };
            var expected = testBookList.OrderByDescending(book => book.ReleaseDate).ToList();
            var result = new Catalogue(testBookList).LinqTime().ToList();
            CollectionAssert.AreEqual(expected, result);
        }
        [TestMethod]
        public void LinqToObjectCatalogueBookNameTest() 
        {
            List<Book> expectedBooks = new() { new("1234433212343", "TestBook1", authors: new List<Author>() { new Author("Test", "Author1") }) };
            List<Book> testbookList = new() { expectedBooks[0], new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08", authors:new List<Author>() { new Author("Test","Author2") }) };
            var result = new Catalogue(testbookList).LinqName("Test", "Author1").ToList();
            CollectionAssert.AreEqual(expectedBooks, result);
        }
        [TestMethod]
        public void LinqToObjectCatalogueTupleAuthorBookCountTest()
        {
            Author testAuthor1 = new("Test", "Author1");
            Author testAuthor2 = new("Test", "Author2");
            Book testBook1 = new ("1234433212343", "TestBook1", authors: new List<Author>() {testAuthor1 });
            Book testBook2 = new ("1237833215643", "TestBook2", authors: new List<Author>() { testAuthor2 });
            Book testBook3 = new ("2835533215643", "TestBook3", authors: new List<Author>() { testAuthor2 });
            List<Book> testBookList = new() { testBook1, testBook2, testBook3 };
            List<(Author, int)> expectedTuples = new() { (testAuthor1, 1), (testAuthor2, 2)};
            var result = new Catalogue(testBookList).LinqTuple().ToList();
            CollectionAssert.AreEqual(expectedTuples, result);
        }
        [TestMethod]
        public void CatalogueEnumenatorTest()
        {
            List<Book> unsortedBookList = new() { new Book("1234433212343", "TestBook1", "2005.07.08"), new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08") };
            List<Book> expectedSortedList = unsortedBookList.OrderBy(book =>book.Title).ToList();
            Catalogue testCatalogue = new Catalogue(unsortedBookList);
            List<Book> result = new List<Book>();
            foreach (var book in testCatalogue)
            {
                result.Add(book);
            }
            CollectionAssert.AreEqual(result, unsortedBookList);
        }
        [TestMethod]
        public void GetBookByISBNTest()
        {
            var expectedBook = new Book("1234433212343", "TestBook1", "2005.07.08");
            List<Book> testBookList = new() { expectedBook, new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08") };
            var result = new Catalogue(testBookList).GetBook("1234433212343");
            Assert.AreEqual(expectedBook, result);
        }
    }
}