using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinksShortening.Models
{
    public class Link
    {
        public int Id { set; get; }
        public string LongURL { set; get; }
        public string ShortURL { set; get; }
        public int Jumps { set; get; }

        public Link() { }
        public Link(string longURL)
        {
            LongURL = longURL;
        }
    }
}
