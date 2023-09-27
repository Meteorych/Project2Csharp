using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BooksCatalogue.Classes
{
    public class Book
    {
        public string ISBN { get; }
        public string Title { get; }
        public DateOnly? ReleaseDate { get; }
        public List<Author>? Authors { get; }
        public Book(string isbn, string title, string? releaseDate = null, List<Author>? authors = null)
        { 
            if (!RegexISBNCheck(isbn))
            {
                throw new ArgumentException("Wrong ISBN!");
            }
            ISBN = isbn;
            Title = title;
            if (releaseDate != null)
            {
                ReleaseDate = DateOnly.Parse(releaseDate);
            }
            Authors = authors;
        }

        private static bool RegexISBNCheck(string isbn)
        {
            Regex pattern = new("^\\d{13}$|^\\d{13}$|^\\d{3}-\\d-\\d{2}-\\d{6}-\\d$");
            return pattern.IsMatch(isbn);
        }
        //Переделать Equals
        public override bool Equals(object? otherObject)
        {
            if (otherObject == null || otherObject is not Book)
            {
                return false;
            }

            // Cast the otherObject to Book type for property comparison.
            Book otherBook = (Book)otherObject;

            // Compare ISBN properties for equality.
            return ISBN == otherBook.ISBN;
        }
        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }
    }
}
