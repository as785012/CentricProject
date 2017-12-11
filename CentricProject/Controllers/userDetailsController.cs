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
    public class userDetailsController : Controller
    {
        private centricContext db = new centricContext();

        // GET: userDetails
        public ActionResult Index(string searchString)
        {
            if (User.Identity.IsAuthenticated)
            {
                var testUsers = from u in db.userDetails select u;
                if (!String.IsNullOrEmpty(searchString))
                {
                    testUsers = testUsers.Where(u => u.firstName.Contains(searchString) || u.lastName.Contains(searchString));
                    return View(testUsers.ToList());
                }
                return View(db.userDetails.ToList());
            }
            else
            {
                return View("NotAuthenticated");
            }
        }

        // GET: userDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            recognitionsController userRecognitions = new recognitionsController();
            IEnumerable<recognition> userRecognitionList = userRecognitions.getAllRecognitions();
            userRecognitionList = userRecognitionList.Where(u => u.recognizer.Equals(id));

            IEnumerable<recognition> userRecognitionListReceiver = userRecognitions.getAllRecognitions();
            userRecognitionListReceiver = userRecognitionListReceiver.Where(r => r.recognizee.Equals(id));
            if (userRecognitionList != null || userRecognitionListReceiver != null)
            {
                foreach (var item in userRecognitionList)
                {
                    ViewBag.recognizer = getFullName(item.recognizer);
                    ViewBag.recognizee = getFullName(item.recognizee);
                    ViewBag.coreValue = item.recognitionCoreValue;
                    ViewBag.description = item.description;
                    ViewBag.dateTime = item.dateTime;
                }

                foreach(var item in userRecognitionListReceiver)
                {
                    ViewBag.recognizerRec = getFullName(item.recognizer);
                    ViewBag.recognizeeRec = getFullName(item.recognizee);
                    ViewBag.coreValueRec = item.recognitionCoreValue;
                    ViewBag.descriptionRec = item.description;
                    ViewBag.dateTimeRec = item.dateTime;
                }
               
                ViewBag.MyList = userRecognitionList;
                ViewBag.MyListRec = userRecognitionListReceiver;
                
            }
            return View(userDetails);
        }

        // GET: userDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,email,firstName,lastName,phoneNumber,office,currentRole,anniversary,yearsWithCentric,photo")] userDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                userDetails.ID = memberID;
                db.userDetails.Add(userDetails);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View("DuplicateUser");
                }

            }

            return View(userDetails);
        }

        // GET: userDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: userDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,email,firstName,lastName,phoneNumber,office,currentRole,anniversary,yearsWithCentric,photo")] userDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetails);
        }

        // GET: userDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userDetails userDetails = db.userDetails.Find(id);
            if (userDetails == null)
            {
                return HttpNotFound();
            }
            return View(userDetails);
        }

        // POST: userDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            userDetails userDetails = db.userDetails.Find(id);
            db.userDetails.Remove(userDetails);
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

        private string getFullName(Guid? id)
        {
            userDetails userDetails = db.userDetails.Find(id);
            string fullName = userDetails.fullName;
            return fullName;
        }
        
    }
}
