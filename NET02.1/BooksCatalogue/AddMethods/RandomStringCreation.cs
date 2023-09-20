using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogue.AddMethods
{
    public static class RandomStringCreation
    {
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
