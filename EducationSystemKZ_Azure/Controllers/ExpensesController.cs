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
    public class ExpensesController : Controller
    {
        private zheminDBEntities1 db = new zheminDBEntities1();


        // Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Expenses
        public ActionResult GetAll()
        {
            var expenses = db.Expenses.Include(e => e.Staff);
            return View(expenses.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Description,Amount,DateTime,StaffId")] Expense expense)
        {
            
            if (ModelState.IsValid)
            {
                expense.DateTime = DateTime.Now;
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("GetAll");
            }

            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", expense.StaffId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", expense.StaffId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Description,Amount,DateTime,StaffId")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetAll");
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", expense.StaffId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("GetAll");
        }

        // GET: Expenses/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: Expenses/Search
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "Id,Type,Description,Amount,DateTime,StaffId")] Expense expense)
        {
            if (expense.Type == null)
            {
                expense.Type = "";
            }

            IEnumerable<Expense> expenses;
            if (ModelState.IsValid)
            {

                expenses = from r in db.Expenses.AsQueryable()
                              where r.DateTime.Month == expense.DateTime.Month && r.Type.Contains(expense.Type)
                              select r;



                return View("GetAll", expenses);
            }

            
            return View(expense);
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
