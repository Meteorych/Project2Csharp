using System.Text.RegularExpressions;


namespace BooksCatalog.Classes
{
    public class Book
    {
        public string Isbn { get; }
        public string Title { get; }
        public DateOnly? ReleaseDate { get; }
        public List<Author>? Authors { get; }
        public Book(string isbn, string title, string? releaseDate = null, List<Author>? authors = null)
        { 
            if (!RegexIsbnCheck(isbn))
            {
                throw new ArgumentException("Wrong Isbn!");
            }
            Isbn = isbn;
            Title = title;
            if (releaseDate != null)
            {
                ReleaseDate = DateOnly.Parse(releaseDate);
            }
            Authors = authors;
        }

        private static bool RegexIsbnCheck(string isbn)
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
            return Isbn.Replace("-", "") == otherBook.Isbn.Replace("-", "");
        }
        public override int GetHashCode()
        {
            return Isbn.GetHashCode();
        }
    }
}
