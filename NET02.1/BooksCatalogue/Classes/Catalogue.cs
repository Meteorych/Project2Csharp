  using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogue.Classes
{
    public class Catalogue : IEnumerable<Book>
    {
        private List<Book> _books;
        public Catalogue(List<Book> books)
        {
            _books = books;
        }
        public void AddBook(Book book)
        {
            _books.Add(book);
        }
        /// <summary>
        /// Get book by its ISBN.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public Book? GetBook(string isbn)
        {
            foreach (Book book in _books)
            {
                if (book.ISBN == isbn) return book;
            }
            return null;
        }
        /// <summary>
        /// Get books of particular author.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public IEnumerable<Book>? LinqName(string firstName, string lastName)
        {
            var SelectedBooks = from book in _books
                                where book.Authors != null &&
                              book.Authors.Any(author =>
                                  author.FirstName == firstName &&
                                  author.LastName == lastName)
                                select book;
            return SelectedBooks.ToList();
        }
        /// <summary>
        /// Return list of books sorted from the newest to the oldest.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> LinqTime()
        {
            var SelectedBooks = from book in _books
                                orderby book.ReleaseDate descending
                                select book;
            return SelectedBooks.ToList();
        }
        /// <summary>
        /// Return tuple "Author — Number of his/her books".
        /// </summary>
        /// <returns></returns>
        public IEnumerable<(Author author, int BooksCount)> LinqTuple()
        {
            var authorBookCounts = _books
            .Where(book => book.Authors != null)
            .SelectMany(book => book.Authors, (book, author) => new { Book = book, Author = author })
            .GroupBy(x => x.Author)
            .Select(group => (group.Key, BookCount: group.Count()));

            return authorBookCounts.ToList();
        }
        /// <summary>
        /// Method that allows us to enumerate books from catalogue by simple cycle.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Book> GetEnumerator()
        {
            var sortedBooks = _books.OrderBy(book => book.Title).ToList();

            foreach (var book in sortedBooks)
            {
                yield return book;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
