using System.Text;


namespace BooksCatalog.AddMethods
{
    public static class RandomStringCreation
    {
        public static string RandomString(int length)
        {
            StringBuilder stringBuilder = new();
            Random rand = new();
            for (int i = 0; i < length; i++)
            {
                var randValue = rand.Next(26);
                var letter = Convert.ToChar(randValue + 65);
                stringBuilder.Append(letter);
            }
            return stringBuilder.ToString();
        }
    }
}
