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
            int hashcode = longURL.GetHashCode();
            if (hashcode < 0) hashcode *= -1;

            List<int> hashTwoDecimalsItems = new List<int>();
            int temp = hashcode;
            while (hashcode > 1)
            {
                int hashItem = hashcode % 100;

                hashTwoDecimalsItems.Add(hashItem);
                hashcode /= 100;
            }

            Random r = new Random();
            for (int i = 0; i < hashTwoDecimalsItems.Count; i++)
            {
                Random r1 = new Random(r.Next(0, hashTwoDecimalsItems[i]));

                hashTwoDecimalsItems[i] = 97 + r1.Next(0, 24);
            }

            string strHashCode = string.Join("", hashTwoDecimalsItems.Select(i => (char)i));
            return $"https://links.by/{strHashCode}";
        }
    }
}
