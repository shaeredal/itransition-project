using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itransition_project.Models;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace itransition_project.Controllers
{
    public class ComixController : Controller
    {
        // GET: Page
        public ActionResult Index(int id)
        {
            var db = new ApplicationDbContext();
            var comixModel = db.Comixes.FirstOrDefault(x => x.Id.Equals(id));
            //var comixViewModel = Mapper.Map<Comix, ComixViewModel>(comixModel);
            //return View(comixViewModel);
            return View(comixModel);
        }

        public ActionResult AddComix()
        {
            return View();
        }
        
        public ActionResult ComixPage()
        {
            return View();
        }

        public ActionResult ViewComix()
        {
            return View();
        }

        public ActionResult EditComix()
        {
            return View();
        }

        public void ReceiveComix(JsonComixViewModel comix)
        {
            var db = new ApplicationDbContext();

            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var currentAppUser = db.Users.First(x => x.Id == currentUserId);

            Comix c = new Comix() { 
                Pages = new List<Page>()
            }; //TODO: add author, ratings and other

            foreach (var page in comix.Pages)
            {
                Page p = new Page()
                {
                    Template = db.Templates.First(x => x.Type == page.Template),
                    Frames = new List<Frame>()
                };

                foreach(var image in page.FrameImages)
                {
                    Frame f = new Frame()
                    {
                        BackgroundImage = image.BackgroundImage, //TODO: download to cloudinary and set link
                        Top = image.Top,
                        Left = image.Left,
                        Width = image.Width,
                        Height = image.Height,
                        Balloons = new List<Balloon>()
                    };

                    foreach(var balloon in image.Balloons)
                    {
                        Balloon b = new Balloon()
                        {
                            Text = balloon.Text,
                            Top = balloon.Top,
                            Left = balloon.Left,
                            Width = balloon.Width,
                            Height = balloon.Height
                        };
                        f.Balloons.Add(b);
                        //db.Balloons.Add(b);
                    }

                    p.Frames.Add(f);
                    //db.Frames.Add(f);
                }
                c.Pages.Add(p);
                //db.Pages.Add(p);
            }

            //db.Comixes.Add(c);

            currentAppUser.Profile.Comixes.Add(c);

            db.SaveChanges();
        }
    }
}