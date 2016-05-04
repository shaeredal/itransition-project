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
    }
}