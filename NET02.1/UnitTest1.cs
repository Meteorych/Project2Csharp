using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BooksCatalogue.AddMethods;
using System.ComponentModel.Design;
using BooksCatalogue.Classes;

namespace BooksCataloguesTests
{
    [TestClass]
    static class Tests
    {
        /// <summary>
        /// Book test in which created books have no authors
        /// </summary>
        [TestMethod]
        public static void BookTest1()
        {
            Book testBook1 = new("1234567891017", "Test Book", "2012-05-09");
            Console.WriteLine($"{testBook1.GetType().Name} ISBN: {testBook1.ISBN}, Release Date: {testBook1.ReleaseDate}, Authors: {testBook1.Authors}");
            //book with wrong isbn to check exception call and regex check
            try
            {
                Book testBook2 = new("121215--25124-21", "Wrong-ISBN Book");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Assert.Fail(); }
            
        }
        /// <summary>
        /// Check of book with authors's creation
        /// </summary>
        [TestMethod]
        public static void BookTest2()
        {
            Author testAuthor = new("Mahatma", "Bugandi");
            Book testBook3 = new("1234567891011", "Test Book", "2012-05-09", new() { testAuthor });
            Console.WriteLine($"{testBook3.GetType().Name} ISBN: {testBook3.ISBN}, Release Date: {testBook3.ReleaseDate}, Authors: {string.Join("", testBook3.Authors.Select(author => $"{author.FirstName} {author.LastName}"))}");
        }
        [TestMethod]
        public static void CatalogueTest1()
        {

        }
        [TestMethod]
        public static void CatalogueTest2()
        {

        }
        [TestMethod]
        public static void AuthorTest()
        {
            Author testAuthor1 = new("Mahatma", "Buandi");
            Author testAuthor2 = new($"{RandomStringCreation.RandomString(205)}", $"{RandomStringCreation.RandomString(205)}");
            Console.WriteLine($"Author: {testAuthor1.FirstName} {testAuthor1.LastName}");
            Console.WriteLine($"Author: {testAuthor2.FirstName.Length} {testAuthor2.LastName.Length}");
        }
        
    }
}