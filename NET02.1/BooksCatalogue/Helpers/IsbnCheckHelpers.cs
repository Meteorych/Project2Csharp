using System.Text.RegularExpressions;

namespace BooksCatalog.Helpers
{
    public class Helper
    {
        private static readonly Regex Pattern = new(@"^\d{13}$|^\d{13}$|^\d{3}-\d-\d{2}-\d{6}-\d$");
        public static bool IsbnCheck(string isbn)
        {
            return Pattern.IsMatch(isbn);
        }
    }
}
