using EducationSystemKZ_Azure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystemKZ_Azure.Controllers
{
    public class HomeController : Controller
    {
        private zheminDBEntities db = new zheminDBEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(EducationSystemKZ_Azure.Models.Administrator userModel)
        {

            var userDetails = db.Administrators.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
            if (userDetails == null)
            {
                userModel.LoginErrorMessage = "Wrong username or password.";
                return View("Index", userModel);
            }
            else
            {
                Session["userID"] = userDetails.Id;
                Session["username"] = userDetails.Username;
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult NotYet()
        {
            return View();
        }

    }
}