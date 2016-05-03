using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using itransition_project.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using itransition_project;

namespace itransition_project.Controllers
{
    public class UserController : Controller
    {
        public static ApplicationUser curUser;
        public static string UserPageId;
        // GET: User
        [HttpGet]
        public ActionResult UserInfo(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            
            if (db.Users.Any(o => o.UserName == id))
            {
                ApplicationUser appUser = System.Web.HttpContext.Current.GetOwinContext().
                    GetUserManager<ApplicationUserManager>().
                    FindById(System.Web.HttpContext.
                    Current.User.Identity.GetUserId());
                //var first = db.Users.First(o => o.UserName == id);
                //var second = new Comment();
                UserPageId = id;
                curUser = appUser;
                //return View(Tuple.Create(first.Profile,second));

                var model = new UserInfoViewModel()
                {
                    Comments = db.Comments.Where(o=>o.Profile.User.UserName == id).ToList(),
                    Profile = db.Users.First(o => o.UserName == id)
                };
                return View(model);
            }
            else
            {
                return View("Error");
            }
            
        }

        [HttpPost]
        public ActionResult UserInfo(itransition_project.Models.Comment comment)
        {
            //ApplicationUser user;
            //using (var manager = System.Web.HttpContext.Current.GetOwinContext().
            //    GetUserManager<ApplicationUserManager>())
            //{
            //    user = manager.FindById(System.Web.HttpContext.
            //        Current.User.Identity.GetUserId());
            //}
            //using (var dbContext = new ApplicationDbContext())
            //{
            //    comment.Author = user;
            //    comment.Time = DateTime.Now;
            //    dbContext.Comments.Add(comment);
            //    dbContext.SaveChanges();
            //}

            var dbContext = new ApplicationDbContext();
            //ApplicationUser appUser = System.Web.HttpContext.Current.GetOwinContext().
            //    GetUserManager<ApplicationUserManager>().
            //    FindById(System.Web.HttpContext.
            //    Current.User.Identity.GetUserId());

            var first = dbContext.Users.First(o => o.UserName == UserPageId);
            comment.Time = DateTime.Now;
            comment.Author = dbContext.Users.First(o => o.UserName == curUser.UserName);
            first.Profile.Comments.Add(comment);

            
            //appUser.Profile.Comments.Add(comment);
            dbContext.SaveChanges();
            return RedirectToAction("UserInfo", "User"/*, user.UserName*/);
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
            ApplicationDbContext db = new ApplicationDbContext();
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