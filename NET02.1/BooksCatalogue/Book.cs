using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BooksCatalogue
{
    class Book
    {
        public string ISBN { get; }
        public string Name { get; }
        public string? ReleaseDate { get; }
        public List<Author>? Authors { get; }
        public Book (string isbn, string name, string? releaseDate, List<Author>? authors) 
        {
            if (RegexISBNCheck(isbn))
            {
                ISBN = isbn;
            }
            else
            {
                throw new ArgumentException("Wrong ISBN format!");
            }
            Name = name;
            ReleaseDate = releaseDate;
            Authors = authors;
        }  

        private static bool RegexISBNCheck(string isbn)
        {
            Regex pattern = new("^/d{13}$|^/d{3}-/d-/d{2}-/d{6}-/d$");
            return pattern.IsMatch(isbn);
        }
        public bool Equals (Book other)
        {
            return other.ISBN == ISBN;
        }
    }
}
