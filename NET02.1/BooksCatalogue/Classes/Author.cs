using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogue.Classes
{
    public class Author
    {
        private const int MaxLength = 200;
        public string FirstName { get; }
        public string LastName { get; }
        public Author(string firstName, string lastName)
        {
            if (firstName.Length > MaxLength || lastName.Length > MaxLength)
            {
                throw new ArgumentOutOfRangeException("Wrong Length!");
            }
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
