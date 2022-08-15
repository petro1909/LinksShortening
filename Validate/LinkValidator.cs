using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinksShortening.Validate
{
    public static class LinkValidator
    {
        public static bool Validate(string link)
        {
            if(string.IsNullOrEmpty(link) || string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Regex regex = new Regex("^(http|https)://\\S+$");
            return regex.IsMatch(link);
        }
    }
}
