using BooksCatalog.Helpers;


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
            if (!Helper.IsbnCheck(isbn))
            {
                throw new ArgumentException("Wrong Isbn!");
            }
            Isbn = isbn.Replace("-", "");
            Title = title;
            if (releaseDate != null)
            {
                ReleaseDate = DateOnly.Parse(releaseDate);
            }
            Authors = authors;
        }
        
        
        
        public override bool Equals(object? otherObject)
        {
            if (otherObject is not Book otherBook)
            {
                return false;
            }

            // Cast the otherObject to Book type for property comparison.
            // Compare ISBN properties for equality.
            return Isbn == otherBook.Isbn;
        }
        public override int GetHashCode()
        {
            return Isbn.GetHashCode();
        }
    }
}
