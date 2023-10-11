using System.Text.RegularExpressions;


namespace BooksCatalog.Classes
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
            Regex pattern = new(@"^\d{13}$|^\d{13}$|^\d{3}-\d-\d{2}-\d{6}-\d$");
            return pattern.IsMatch(isbn);
        }
        
        public override bool Equals(object? otherObject)
        {
            if (otherObject is not Book otherBook)
            {
                return false;
            }

            // Cast the otherObject to Book type for property comparison.
            // Compare ISBN properties for equality.
            return ISBN.Replace("-", "") == otherBook.ISBN.Replace("-", "");
        }
        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }
    }
}
