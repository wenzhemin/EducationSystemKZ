using EducationSystemKZ_Azure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystemKZ_Azure.Controllers
{
    public class ReportsController : Controller
    {

        private zheminDBEntities db = new zheminDBEntities();


        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }



        // GET: Reports/Monthly
        public ActionResult Monthly()
        {
            return View();
        }

        // POST: Reports/Monthly
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Monthly([Bind(Include = "Period,TotalFeeReceived,TotalExpenses,Salary,ElectricityFee,WaterFee,OtherFee")] Report report)
        {

            IEnumerable<decimal> tuitionFees;
            IEnumerable<Expense> expenses;
            IEnumerable<decimal> salaries;
            IEnumerable<decimal> electricityFees;
            IEnumerable<decimal> waterFees;
            IEnumerable<decimal> otherFees;

            decimal sumTuitionFee = new decimal();
            decimal sumExpenses = new decimal();
            decimal sumSalary = new decimal();
            decimal sumElectricityFee = new decimal();
            decimal sumWaterFee = new decimal();
            decimal sumOtherFee = new decimal();

            if (ModelState.IsValid)
            {
                tuitionFees = from r in db.TuitionFees.AsQueryable()
                              where r.DateTime.Month == report.Period.Month
                              select r.Amount;

                sumTuitionFee = tuitionFees.Sum();

                expenses = from r in db.Expenses.AsQueryable()
                              where r.DateTime.Month == report.Period.Month
                              select r;

                foreach (Expense e in expenses)
                {
                    sumExpenses += e.Amount;
                }

                salaries = from r in expenses
                            where r.Type == "salary"
                           select r.Amount;

                sumSalary = salaries.Sum();

                electricityFees = from r in expenses
                           where r.Type == "electricity fee"
                                  select r.Amount;

                sumElectricityFee = electricityFees.Sum();

                waterFees = from r in expenses
                                  where r.Type == "water fee"
                            select r.Amount;

                sumWaterFee = waterFees.Sum();

                otherFees = from r in expenses
                                  where r.Type == "others"
                            select r.Amount;

                sumOtherFee = otherFees.Sum();


                Report result = new Report() { Period = report.Period, TotalFeeReceived = sumTuitionFee, TotalExpenses = sumExpenses, Salary = sumSalary, ElectricityFee = sumElectricityFee, WaterFee = sumWaterFee, OtherFee = sumOtherFee };
                return View("GetReport", result);
            }
            return View(report);

            

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