using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinksShortening.Service
{
    public static class LinkService
    {
        public static string GenerateShortedLink(string longURL)
        {
            // Get positive hash code
            int hashcode = longURL.GetHashCode();
            if (hashcode < 0) hashcode *= -1;

            
            List<int> hashTwoDigitsItems = new List<int>();
            
            // Split hashcode to two-digit numbers
            while (hashcode > 1)
            {
                int hashItem = hashcode % 100;

                hashTwoDigitsItems.Add(hashItem);
                hashcode /= 100;
            }

            Random r = new Random();

            //Randomize each two-digit hashcode part
            for (int i = 0; i < hashTwoDigitsItems.Count; i++)
            {
                //Create random entity with random seed
                Random r1 = new Random(r.Next(0, hashTwoDigitsItems[i]));
                //Generate random number that is more then 97 and less then 123
                hashTwoDigitsItems[i] = 97 + r1.Next(0, 26);
            }

            //Split randomized hashcode digits to streing
            string strHashCode = string.Join("", hashTwoDigitsItems.Select(i => (char)i));
            return $"https://links.by/{strHashCode}";
        }
    }
}
