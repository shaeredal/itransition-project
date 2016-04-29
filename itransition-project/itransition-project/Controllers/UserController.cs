using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

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


            return View();
        }

    }
}