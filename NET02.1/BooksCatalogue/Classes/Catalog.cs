using System.Collections;


namespace BooksCatalog.Classes
{
    public class Catalog : IEnumerable<Book>
    {
        private List<Book> _books;
        public Catalog(List<Book> books)
        {
            _books = books;
        }
        //Checking if there is such book in catalog
        public void AddBook(Book book)
        {
            if (_books.All(book2 => book2.ISBN != book.ISBN))
            {
                _books.Add(book);
            }
            
        }
        /// <summary>
        /// Get book by its ISBN.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public Book? GetBook(string isbn)
        {
            var selectedBook = _books.FirstOrDefault(book => book.ISBN == isbn);
            return selectedBook;
        }
        /// <summary>
        /// Get books of particular author.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public IEnumerable<Book> GetByAuthorName(string firstName, string lastName)
        {
            var selectedBooks = _books.
                                Where(book => book.Authors != null &&
                              book.Authors.Any(author =>
                                  author.FirstName == firstName &&
                                  author.LastName == lastName));
            return selectedBooks.ToList();
        }
        /// <summary>
        /// Return list of books sorted from the newest to the oldest.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> ReturnBooksSortedByReleaseDate()
        {
            var sortedBooks = _books.
                                OrderByDescending(book => book.ReleaseDate);
            return sortedBooks.ToList();
        }
        /// <summary>
        /// Return tuple "Author — Number of his/her books".
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(Author author, int BooksCount)> GetNumberOfBooksByAuthors()
        {
            var authorBookCounts = _books
            .Where(book => book.Authors != null)
            .SelectMany(book => book.Authors, (book, author) => new { Book = book, Author = author })
            .GroupBy(x => x.Author)
            .Select(group => (group.Key, BookCount: group.Count()));

            return authorBookCounts.ToList();
        }
        /// <summary>
        /// Method that allows us to enumerate books from catalog by simple cycle.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Book> GetEnumerator()
        { 
            return _books.OrderBy(book => book.Title).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
