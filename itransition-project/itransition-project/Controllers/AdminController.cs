﻿using itransition_project.Models;
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
        [Authorize(Roles = "admin")]
        public ActionResult EditDetails(string id)
        {
            var dbContext = new ApplicationDbContext();
            if (dbContext.Users.Any(x => x.UserName == id))
            {
                var user = dbContext.Users.First(x => x.UserName == id);
                return View(user.Profile);
            }
            return View("Error");
            
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
        public JsonResult UpdateImage(string data, string id)
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
            var user = dbContext.Users.First(x => x.UserName == id);
            user.Profile.Photo = uploadResult.SecureUri.ToString();
            dbContext.SaveChanges();
            return new JsonResult();
        }
    }
}