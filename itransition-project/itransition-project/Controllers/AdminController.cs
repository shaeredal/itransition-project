using itransition_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace itransition_project.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult EditDetails(string id)
        {
            var dbContext = new ApplicationDbContext();
            var user = dbContext.Users.First(x => x.UserName == id);
            return View(user.Profile);
        }

        [HttpPost]
        public ActionResult EditDetails(string about, string id)
        {
            var dbContext = new ApplicationDbContext();
            var user = dbContext.Users.First(x => x.UserName == id);
            user.Profile.About = about;
            dbContext.SaveChanges();
            return RedirectToAction("UserInfo", "User");
        }

        [HttpPost]
        public JsonResult UpdateImage(string data)
        {
            var dbContext = new ApplicationDbContext();
            var account = new Account(
                "da40pd4iw",
            "878111261769614",
            "d_UzO32EJIqhtFnshPcdgalOFeg");

            var cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(data)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            var user = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.
                Current.User.Identity.GetUserId());
            if (user.Profile.Photo == "http://res.cloudinary.com/da40pd4iw/image/upload/v1460917537/%D0%9F%D0%B8%D0%B2%D0%BE_y9a59r.jpg")
            {
                user.Profile.Medals.Add(new Medal
                {
                    Image = "http://res.cloudinary.com/da40pd4iw/image/upload/v1462468044/medal_profile_t9bltt.png",
                    Name = "Photo add",
                    Profile = user.Profile
                });
            }
            user.Profile.Photo = uploadResult.SecureUri.ToString();


            System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().Update(user);
            dbContext.SaveChanges();
            return new JsonResult();
        }
    }
}