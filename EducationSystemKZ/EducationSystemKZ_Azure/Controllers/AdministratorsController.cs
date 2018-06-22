using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EducationSystemKZ_Azure.Models;

namespace EducationSystemKZ_Azure.Controllers
{
    public class AdministratorsController : Controller
    {
        private zheminDBEntities db = new zheminDBEntities();

        // GET: Administrators
        public ActionResult Index()
        {
            return View();
        }

        // GET: Administrators
        public ActionResult GetAll()
        {
            return View(db.Administrators.ToList());
        }

        // GET: Administrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Administrators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("GetAll");
            }

            return View(administrator);
        }

        // GET: Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAll");
            }
            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrators.Find(id);
            db.Administrators.Remove(administrator);
            db.SaveChanges();
            return RedirectToAction("GetAll");
        }

        // GET: Administrators/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: Administrators/Search
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "Id,Username,Password")] Administrator administrator)
        {
            if (administrator.Username == null)
            {
                administrator.Username = "";
            }

            IEnumerable<Administrator> administrators;
            if (ModelState.IsValid)
            {

                administrators = from r in db.Administrators.AsQueryable()
                           where r.Username == administrator.Username
                           select r;



                return View("GetAll", administrators);
            }


            return View(administrator);
        }






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
