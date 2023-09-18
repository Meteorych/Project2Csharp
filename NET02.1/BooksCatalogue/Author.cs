using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogue
{
    class Author
    {
        private const int _maxLength = 200;
        public string FirstName { get; }
        public string LastName { get; } 
        public Author(string firstName, string lastName) 
        { 
            FirstName = firstName[.._maxLength];
            LastName = lastName[.._maxLength];
        } 
    }
}
