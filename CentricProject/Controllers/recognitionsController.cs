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
            ViewBag.recognizer = new SelectList(db.userDetails, "ID", "fullName");
            ViewBag.recognizee = new SelectList(db.userDetails, "ID", "fullName");
            return View();
        }

        // POST: recognitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recognitionID,recognizer,recognizee,recognitionCoreValue,description, starPoints, dateTime")] recognition recognition)
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
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionID,recognizer,recognizee,recognitionCoreValue,description, starPoints, dateTime")] recognition recognition)
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

        public IEnumerable<recognition> getAllRecognitions()
        {
            var recognitions = db.recognition;

            IEnumerable<recognition> listOfAllRecognitions = recognitions.ToList();

            return listOfAllRecognitions;
        }

        
    }
}
