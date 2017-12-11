using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CentricProject.Models;
using CentricProject.Models.DAL;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;


namespace CentricProject.Controllers
{
    public class recognitionsController : Controller
    {
        private centricContext db = new centricContext();

        // GET: recognitions
        public ActionResult Index()
        {
            var recognition = db.recognition.Include(r => r.Giver).Include(r => r.userDetails);
            return View(recognition.ToList());
        }

        // GET: recognitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // GET: recognitions/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.recognizer = new SelectList(db.userDetails, "ID", "fullName");
                ViewBag.recognizee = new SelectList(db.userDetails, "ID", "fullName");
                return View();
            } else
            {
                return View("NotAuthenticated");
            }
            
        }

        // POST: recognitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recognitionID,recognizer,recognizee,recognitionCoreValue,description,dateTime")] recognition recognition)
        {
            if (ModelState.IsValid)
            {
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                recognition.recognizer = memberID;
                db.recognition.Add(recognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.recognizer = new SelectList(db.userDetails, "ID", "fullName", recognition.recognizer);
            ViewBag.recognizee = new SelectList(db.userDetails, "ID", "fullName", recognition.recognizee);
            return View(recognition);
            emailRecognition(recognition);
        }

        // GET: recognitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            ViewBag.recognizer = new SelectList(db.userDetails, "ID", "fullName", recognition.recognizer);
            ViewBag.recognizee = new SelectList(db.userDetails, "ID", "fullName", recognition.recognizee);
            return View(recognition);
        }

        // POST: recognitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionID,recognizer,recognizee,recognitionCoreValue,description,dateTime")] recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.recognizer = new SelectList(db.userDetails, "ID", "fullName", recognition.recognizer);
            ViewBag.recognizee = new SelectList(db.userDetails, "ID", "fullName", recognition.recognizee);
            return View(recognition);
        }

        // GET: recognitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognition.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // POST: recognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            recognition recognition = db.recognition.Find(id);
            db.recognition.Remove(recognition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult emailRecognition(object recognition)
        {
            //TODO: Add email of the recognizee into the method so it sends an email to the correct person

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential("mis4200team16@gmail.com", "Testing!23");
            MailMessage mailMessage = new MailMessage();
            MailAddress from = new MailAddress("mis4200team16@gmail.com", "Recognition Dev Team");
            mailMessage.From = from;
            mailMessage.To.Add("as785012@ohio.edu");
            mailMessage.Subject = "MVC Email Test";
            mailMessage.Body = "Body of the message, eventually this will display the description of the recognition and the core value";
            mailMessage.Body += "call a method that gets the information above";
            try 
            {
                smtpClient.Send(mailMessage);
                TempData["mailError"] = "";
            }
            catch (Exception e)
            {
                TempData["mailError"] = e.Message;
                
            }
            return View("Email");
        }

        public IEnumerable<recognition> getAllRecognitions()
        {
            var recognitions = db.recognition;

            IEnumerable<recognition> listOfAllRecognitions = recognitions.ToList();

            return listOfAllRecognitions;
        }
    }
}
