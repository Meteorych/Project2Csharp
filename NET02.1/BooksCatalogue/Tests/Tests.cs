using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogue.Tests
{
    static class Tests
    {
        /// <summary>
        /// Book test in which created books have no authors
        /// </summary>
        public static void BookTest1()
        {
            Book testBook1 = new("1234567891017", "Test Book", "2012-05-09");
            Console.WriteLine($"{testBook1.GetType().Name} ISBN: {testBook1.ISBN}, Release Date: {testBook1.ReleaseDate}, Authors: {testBook1.Authors}");
            //book with wrong isbn to check exception call and regex check
            try 
            { 
                Book testBook2 = new("121215--25124-21", "Wrong-ISBN Book");
            } catch (ArgumentException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Check of book with authors's creation
        /// </summary>
        public static void BookTest2() 
        {
            Author testAuthor = new("Mahatma", "Bugandi");
            Book testBook3 = new("1234567891011", "Test Book", "2012-05-09", new() { testAuthor });
            Console.WriteLine($"{testBook3.GetType().Name} ISBN: {testBook3.ISBN}, Release Date: {testBook3.ReleaseDate}, Authors: {string.Join("", testBook3.Authors.Select(author => $"{author.FirstName} {author.LastName}"))}");
        }
        public static void CatalogueTest1()
        {

        }
        public static void CatalogueTest2() 
        { 
        
        }
        public static void AuthorTest()
        {
            Author testAuthor1 = new("Mahatma", "Buandi");
            Author testAuthor2 = new($"{RandomString(205)}", $"{RandomString(205)}");
            Console.WriteLine($"Author: {testAuthor1.FirstName} {testAuthor1.LastName}");
            Console.WriteLine($"Author: {testAuthor2.FirstName.Length} {testAuthor2.LastName.Length}");
        }
        public static string RandomString(int length)
        {
            StringBuilder stringBuilder = new();
            Random rand = new();
            for (int i = 0; i < length; i++)
            {
                int randvalue = rand.Next(26);
                char letter = Convert.ToChar(randvalue + 65);
                stringBuilder.Append(letter);
            }
            return stringBuilder.ToString();
        }
    }
}
