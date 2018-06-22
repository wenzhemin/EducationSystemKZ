using EducationSystemKZ_Azure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationSystemKZ_Azure.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(EducationSystemKZ_Azure.Models.Administrator userModel)
        {
            using (zheminDBEntities db = new zheminDBEntities())
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
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}