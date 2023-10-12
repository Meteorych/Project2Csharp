namespace BooksCatalog.Classes
{
    public class Author
    {
        private const int NameMaxLength = 200;
        public string FirstName { get; }
        public string LastName { get; }
        public Author(string firstName, string lastName)
        {
            if (firstName.Length > NameMaxLength || lastName.Length > NameMaxLength)
            {
                throw new ArgumentOutOfRangeException("Wrong Length!");
            }
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
