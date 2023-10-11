using BooksCatalog.Classes;

namespace BooksCatalogTests
{
    [TestClass]
    public class UnitTest1
    {
        private const int MaxLength = 200;

        [TestMethod]
        public void AuthorConstructor_ThrowsExceptionWhenFirstNameExceedsMaxLength()
        {
            string firstName = new ('A', MaxLength + 1); 
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Author(firstName, "LastName"));
        }
        [TestMethod]
        public void CatalogBooks_ReleaseDateParsingTest () 
        {
            DateOnly expected = new(2003, 9, 23);
            const string releaseDate = "2003.09.23";
            Book testBook = new("1234567890123", "Title", releaseDate);
            Assert.AreEqual (expected, testBook.ReleaseDate);
        }
        [TestMethod]
        public void CatalogBooks_SortingBooksByTimeTest()
        {
            List<Book> testBookList = new() { new Book("1234433212343", "TestBook1", "2005.07.08"), new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08") };
            var expected = testBookList.OrderByDescending(book => book.ReleaseDate).ToList();
            var result = new Catalog(testBookList).ReturnBooksSortedByReleaseDate().ToList();
            CollectionAssert.AreEqual(expected, result);
        }
        [TestMethod]
        public void CatalogBooks_SelectBookByNameOfAuthorTest() 
        {
            List<Book> expectedBooks = new() { new("1234433212343", "TestBook1", authors: new List<Author>() { new Author("Test", "Author1") }) };
            List<Book> testBookList = new() { expectedBooks[0], new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08", authors:new List<Author>() { new Author("Test","Author2") }) };
            var result = new Catalog(testBookList).GetByAuthorName("Test", "Author1").ToList();
            CollectionAssert.AreEqual(expectedBooks, result);
        }
        [TestMethod]
        public void CatalogBooks_CatalogReturnTupleAuthorBookCountTest()
        {
            Author testAuthor1 = new("Test", "Author1");
            Author testAuthor2 = new("Test", "Author2");
            Book testBook1 = new ("1234433212343", "TestBook1", authors: new List<Author>() {testAuthor1 });
            Book testBook2 = new ("1237833215643", "TestBook2", authors: new List<Author>() { testAuthor2 });
            Book testBook3 = new ("2835533215643", "TestBook3", authors: new List<Author>() { testAuthor2 });
            List<Book> testBookList = new() { testBook1, testBook2, testBook3 };
            List<(Author, int)> expectedTuples = new() { (testAuthor1, 1), (testAuthor2, 2)};
            var result = new Catalog(testBookList).GetNumberOfBooksByAuthors().ToList();
            CollectionAssert.AreEqual(expectedTuples, result);
        }
        [TestMethod]
        public void CatalogBooks_EnumenatorTest()
        {
            List<Book> unsortedBookList = new() { new Book("1234433212343", "TestBook1", "2005.07.08"), new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08") };
            Catalog testCatalog = new(unsortedBookList);
            List<Book> result = new();
            foreach (var book in testCatalog)
            {
                result.Add(book);
            }
            CollectionAssert.AreEqual(result, unsortedBookList);
        }
        [TestMethod]
        public void CatalogBooks_GetBookByISBNTest()
        {
            var expectedBook = new Book("1234433212343", "TestBook1", "2005.07.08");
            List<Book> testBookList = new() { expectedBook, new Book("1237833215643", "TestBook2", "2001.07.08"),
               new Book("2835533215643", "TestBook3", "1999.07.08") };
            var result = new Catalog(testBookList).GetBook("1234433212343");
            Assert.AreEqual(expectedBook, result);
        }
    }
}