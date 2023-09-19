using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogue
{
    class Catalogue : IEnumerable<Book>
    {
        private List<Book> _books = new();
        public Catalogue(List<Book> books)
        {
            _books = books;
        }
        public void AddBook(Book book)
        {
            _books.Add(book);
        }
        public Book? GetBook(string isbn) 
        {
            foreach (Book book in _books)
            {
                if (book.ISBN == isbn) return book;
            }
            return null;
        }
        public List<Book> LinqName(string firstName, string lastName) 
        { 
            
        }
        public List<Book> LinqTime(string time)
        {

        }
        public List<Book> LinqTuple()
        {

        }
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
