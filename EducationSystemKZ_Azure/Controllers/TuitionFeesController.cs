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
    public class TuitionFeesController : Controller
    {
        private zheminDBEntities1 db = new zheminDBEntities1();

        // Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: TuitionFees
        public ActionResult GetAll()
        {
            var tuitionFees = db.TuitionFees.Include(t => t.Student);
            return View(tuitionFees.ToList());
        }

        // GET: TuitionFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuitionFee tuitionFee = db.TuitionFees.Find(id);
            if (tuitionFee == null)
            {
                return HttpNotFound();
            }
            return View(tuitionFee);
        }

        // GET: TuitionFees/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name");
            return View();
        }

        // POST: TuitionFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,Amount,DateTime,FeePeriod,StudentId")] TuitionFee tuitionFee)
        {

            IEnumerable<TuitionFee> tuitionFees;
            if (ModelState.IsValid)
            {

                tuitionFees = from r in db.TuitionFees.AsQueryable()
                              where r.StudentId == tuitionFee.StudentId
                              orderby r.FeePeriod descending
                              select r;

                TuitionFee t = tuitionFees.FirstOrDefault();

                tuitionFee.DateTime = DateTime.Now;
                
                if ((tuitionFee.FeePeriod.Month - t.FeePeriod.Month) <= 1)
                {
                    db.TuitionFees.Add(tuitionFee);
                    db.SaveChanges();
                    return RedirectToAction("GetAll");
                }
                else
                {
                    return RedirectToAction("ErrorDate");
                }


            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", tuitionFee.StudentId);
            return View(tuitionFee);
        }

        // GET: TuitionFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuitionFee tuitionFee = db.TuitionFees.Find(id);
            if (tuitionFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", tuitionFee.StudentId);
            return View(tuitionFee);
        }

        // POST: TuitionFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,Amount,DateTime,FeePeriod,StudentId")] TuitionFee tuitionFee)
        {
            if (ModelState.IsValid)
            {


                db.Entry(tuitionFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAll");
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", tuitionFee.StudentId);
            return View(tuitionFee);
        }

        // GET: TuitionFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuitionFee tuitionFee = db.TuitionFees.Find(id);
            if (tuitionFee == null)
            {
                return HttpNotFound();
            }
            return View(tuitionFee);
        }

        // POST: TuitionFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TuitionFee tuitionFee = db.TuitionFees.Find(id);
            db.TuitionFees.Remove(tuitionFee);
            db.SaveChanges();
            return RedirectToAction("GetAll");
        }

        // GET: TuitionFees/Check
        public ActionResult Check()
        {
            return View();
        }

        // POST: TuitionFees/Check
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Check([Bind(Include = "InvoiceId,Amount,DateTime,FeePeriod,StudentId")] TuitionFee tuitionFee)
        {


            List<Student> students = db.Students.ToList();

            IEnumerable<TuitionFee> tuitionFees;
            List<int> paidStudentIds = new List<int>();
            List<Student> studentList = db.Students.ToList();

            if (ModelState.IsValid)
            {
                tuitionFees = from r in db.TuitionFees.AsQueryable()
                              where r.FeePeriod.Month >= tuitionFee.FeePeriod.Month && r.FeePeriod.Year == tuitionFee.FeePeriod.Year
                              select r;

                foreach (TuitionFee t in tuitionFees)
                {
                    paidStudentIds.Add(t.StudentId);
                }


                foreach (Student s in students)
                {
                    foreach (int i in paidStudentIds)
                    {
                        if (s.Id == i)
                        {
                            studentList.Remove(s);
                            break;
                        }
                    }
                }
                return View("GetStudents", studentList);
            }
            return View(tuitionFee);

        }


        // GET: TuitionFees/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: TuitionFees/Search
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "InvoiceId,DateTime,Amount,FeePeriod,StudentId")] TuitionFee tuitionFee)
        {

            IEnumerable<TuitionFee> tuitionFees;

            if (ModelState.IsValid)
            {
                tuitionFees = from r in db.TuitionFees.AsQueryable()
                              where r.StudentId == tuitionFee.StudentId
                              orderby r.FeePeriod descending
                              select r;




                return View("GetAll", tuitionFees);
            }
            return View(tuitionFee);

        }

        // ErrorDate
        public ActionResult ErrorDate()
        {
            return View();
        }

        // GET: TuitionFees/SearchByMonth
        public ActionResult SearchByMonth()
        {
            return View();
        }

        // POST: TuitionFees/SearchByMonth
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByMonth([Bind(Include = "InvoiceId,DateTime,Amount,FeePeriod,StudentId")] TuitionFee tuitionFee)
        {

            IEnumerable<TuitionFee> tuitionFees;

            if (ModelState.IsValid)
            {
                tuitionFees = from r in db.TuitionFees.AsQueryable()
                              where r.DateTime.Month == tuitionFee.DateTime.Month
                              select r;




                return View("GetAll", tuitionFees);
            }
            return View(tuitionFee);

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
