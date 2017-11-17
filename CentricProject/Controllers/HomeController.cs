using CentricProject.Models;
using CentricProject.Models.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentricProject.Controllers
{
    public class HomeController : Controller
    {
        private centricContext db = new centricContext();
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
        public new ActionResult Profile()
        {
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            userDetails userDetails = new userDetails();
            if (userDetails.ID == memberID)
            {
                return View("~/Views/userDetails/Details.cshtml", new { ID = memberID });
            }
            else
            {
                return View();

            }
        }
    }
}