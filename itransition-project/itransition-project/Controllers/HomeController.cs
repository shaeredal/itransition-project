using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itransition_project.Models;

namespace itransition_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {      
            return View();
        }
    }

}