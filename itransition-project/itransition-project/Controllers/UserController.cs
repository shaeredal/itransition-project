using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itransition_project.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace itransition_project.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult UserInfo(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (db.Users.Any(o => o.UserName == id))
            {
                ApplicationUser appUser = db.Users.First(o => o.UserName == id);
                var first = db.Users.First(o => o.UserName == id);
                var second = new Comment();
                return View(Tuple.Create(first.Profile,second));
            }
            else
            {
                return View("Error");
            }
            
        }

        [HttpPost]
        public ActionResult UserInfo(itransition_project.Models.Comment comment)
        {
            ApplicationUser user;
            using (var manager = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>())
            {
                user = manager.FindById(System.Web.HttpContext.
                    Current.User.Identity.GetUserId());
            }
            using (var dbContext = new ProjectDbContext())
            {
                comment.Author = user;
                comment.Time = DateTime.Now;
                dbContext.Comments.Add(comment);
                dbContext.SaveChanges();
            }
            
            return RedirectToAction("UserInfo", "User", user.UserName);
        }

        [HttpGet]
        public ActionResult EditDetails()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser appUser = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.
                Current.User.Identity.GetUserId());
            return View(appUser.Profile);
        }

        [HttpPost]
        public ActionResult EditDetails(itransition_project.Models.Profile profile)
        {
            ProjectDbContext db = new ProjectDbContext();
            ApplicationUser appUser = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.
                Current.User.Identity.GetUserId());
            appUser.Profile.About = profile.About;
            System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().Update(appUser);
            return RedirectToAction("UserInfo", "User");
        }



    }
}