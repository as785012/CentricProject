using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentricProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The recognition application is an internal system that has capabilities to recognize excellent work of Centric’s employees based on the Core Values. ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us for your next engagement or for the best career move you'll ever make.";

            return View();
        }
    }
}