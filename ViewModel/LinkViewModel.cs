using LinksShortening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinksShortening.ViewModel
{
    public class LinkViewModel
    {
        public Link Link;
        
        
        public LinkViewModel() {
            Link = new Link();
        }
        public LinkViewModel(Link link)
        {
            Link = link;
        }

        public int Id 
        { 
            set 
            {
                Link.Id = value;
            } 
            get 
            {
                return Link.Id;  
            } 
        }
        public string LongURL
        {
            set
            {
                Link.LongURL = value;
            }
            get
            {
                return Link.LongURL;
            }
        }
        public string ShortURL
        {
            set
            {
                Link.ShortURL = value[(value.LastIndexOf("/")+1)..];
            }
            get
            {
                
                return $"https://localhost:44324/{Link.ShortURL}";
            }
        }
        public DateTime CreationDate
        {
            set
            {
                Link.CreationDate = value;
            }
            get
            {
                return Link.CreationDate;
            }
        }
        public int Jumps
        {
            set
            {
                Link.Jumps = value;
            }
            get
            {
                return (int)Link.Jumps;
            }
        }
    }
}
