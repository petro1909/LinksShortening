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
        public IActionResult Index()
        {
            return View(context.Links.ToList());
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
                return View(link);
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditOrCreate(Link link)
        {
            if (!LinkValidator.Validate(link.LongURL))
            {
                return View(link);
            }
            //Add new link
            Link actualLink = context.Links.Find(link.Id);
            if(actualLink == null)
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
            return LinkService.GenerateShortedLink(longURL);
        }

        //Accepting link follow
        public void AcceptLinkJump(int id)
        {
            Link link = context.Links.Find(id);
            if (link != null)
            {
                link.Jumps++;
                context.Links.Update(link);
                context.SaveChanges();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
