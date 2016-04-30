using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
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
        public ActionResult UserInfo(string id)
        {
            Account account = new Account(
            "da40pd4iw",
            "878111261769614",
            "d_UzO32EJIqhtFnshPcdgalOFeg");
            Cloudinary cloudinary = new Cloudinary(account);
            ApplicationDbContext db = new ApplicationDbContext();
            if (db.Users.Any(o => o.UserName == id))
            {
                ApplicationUser appUser = db.Users.First(o => o.UserName == id);
                return View(appUser.Profile);
            }
            else
            {
                return View("Error");
            }
            
        }

        public ActionResult EditDetails()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser appUser = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.
                Current.User.Identity.GetUserId());
            return View();

        }

    }
}