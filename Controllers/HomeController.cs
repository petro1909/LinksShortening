using LinksShortening.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LinksShortening.Database;
using Microsoft.EntityFrameworkCore;
using LinksShortening.Service;
using LinksShortening.Validate;
using LinksShortening.ViewModel;

namespace LinksShortening.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("/Links")]
        public IActionResult Index()
        {
            List<Link> links = context.Links.ToList();
            List<LinkViewModel> linksVM = new List<LinkViewModel>();

            foreach(Link link in links)
            {
                linksVM.Add(new LinkViewModel(link));
            }

            return View(linksVM);
        }

        [HttpGet]
        public IActionResult EditOrCreate(int? id)
        {
            //Create
            if (id == null)
            {
                return View();
            }

            //Edit
            Link link = context.Links.Find(id);
            if (link != null)
            {
                return View(new LinkViewModel(link));
            }

            return View(new LinkViewModel(link));
        }

        [HttpPost]
        public IActionResult EditOrCreate(LinkViewModel linkVM)
        {
            Link link = linkVM.Link;
            if (!LinkValidator.Validate(link.LongURL))
            {
                return View(link);
            }
            //Add new link
            Link actualLink = context.Links.Find(link.Id);
            if (actualLink == null)
            {
                link.Id = 0;
                link.Jumps = 0;
                link.CreationDate = DateTime.Now;
                context.Links.Add(link);
            }
            //Edit existing link
            else
            {
                actualLink.LongURL = link.LongURL;
                actualLink.ShortURL = link.ShortURL;
                context.Links.Update(actualLink);
            }
            context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Link link = context.Links.Find(id);
            if (link != null)
            {
                context.Links.Remove(link);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public string GenerateShortedLink(string longURL)
        {
            return $"https://localhost:44324/{LinkService.GenerateShortedLink(longURL)}";
        }

        //Accepting link follow
        [Route("/{shortLink}")]
        public IActionResult AcceptLinkJump(string shortLink)
        {
            Link link;
            try
            {
                link = context.Links.Single(l => l.ShortURL == shortLink);
                if (link == null)
                {
                    return default;
                }
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }

            link.Jumps++;
            context.Links.Update(link);
            context.SaveChanges();

            return Redirect(link.LongURL);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
