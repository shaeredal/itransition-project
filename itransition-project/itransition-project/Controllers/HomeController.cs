using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itransition_project.Filters;
using itransition_project.Lucene;
using itransition_project.Models;

namespace itransition_project.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            List<Comix> model = db.Comixes.OrderByDescending(c => c.CreationTime).Take(5).ToList();
            return View(model);
        }
    }
}