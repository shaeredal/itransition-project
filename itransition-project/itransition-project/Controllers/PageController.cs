using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itransition_project.Models;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;

namespace itransition_project.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        public ActionResult Index(int id)
        {
            var db = HttpContext.GetOwinContext().Get<ProjectDbContext>();
            var comixModel = db.Comixes.FirstOrDefault(x => x.Id == id);
            var comixViewModel = Mapper.Map<Comix, ComixViewModel>(comixModel);
            return View();
        }
    }
}