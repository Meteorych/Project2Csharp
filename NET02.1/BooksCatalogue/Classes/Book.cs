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
            if (RegexISBNCheck(isbn))
            {
                ISBN = isbn;
            }
            else
            {
                throw new ArgumentException("");
            }

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
        public override bool Equals(object? otherobject)
        {
            if (otherobject != null && otherobject is Book)
            {
                return true;
            }
            else { return false; }
            
        }
    }
}
