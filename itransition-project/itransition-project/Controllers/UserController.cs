using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using itransition_project.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace itransition_project.Controllers
{
    public class UserController : Controller
    {
        public static string UserPageId;
        // GET: User
        [HttpGet]
        public ActionResult UserInfo(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (db.Users.Any(o => o.UserName == id))
            {
                UserPageId = id;
                var model = new UserInfoViewModel
                {
                    Comments = db.Comments.Where(o => o.Profile.User.UserName == id).OrderByDescending(x => x.Time).ToList(),
                    Profile = db.Users.First(o => o.UserName == id),
                    Medals = db.Medals.Where(o => o.Profile.User.UserName == id).ToList()
                };
                return View(model);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult UserInfo(string text, string userId)
        {
            var dbContext = new ApplicationDbContext();
            var currentUserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var currentAppUser = dbContext.Users.First(x => x.Id == currentUserId);
            var user = dbContext.Users.First(x => x.Id == userId);
            var comment = new Comment
            {
                Time = DateTime.Now,
                Author = currentAppUser,
                Text = text
            };
            user.Profile.Comments.Add(comment);
            if (!dbContext.Comments.Any(x => x.Author.Id == currentUserId))
            {
                currentAppUser.Profile.Medals.Add(new Medal
                {
                    Image = "http://res.cloudinary.com/da40pd4iw/image/upload/v1462468044/medal_comments_vquan0.png",
                    Name = "First Comment",
                    Profile = currentAppUser.Profile
                });
            }
            dbContext.SaveChanges();
            return RedirectToAction("UserInfo", "User");
        }

        [HttpGet]
        public ActionResult EditDetails()
        {
            ApplicationUser appUser = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.
                Current.User.Identity.GetUserId());
            return View(appUser.Profile);
        }

        [HttpPost]
        public ActionResult EditDetails(Profile profile)
        {
            ApplicationUser appUser = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.
                Current.User.Identity.GetUserId());
            appUser.Profile.About = profile.About;
            System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().Update(appUser);
            return RedirectToAction("UserInfo", "User");
        }

        [HttpPost]
        public ActionResult DelComment(int data)
        {
            var dbContext = new ApplicationDbContext();
            Comment comment = dbContext.Comments.First(x => x.Id == data);
            dbContext.Comments.Remove(comment);
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