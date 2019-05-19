using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace Transact.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoggedOut()
        {

            Session.Remove("UserID");

            //Session.Abandon();
            return RedirectToAction("Login" , "Login");


        }
    }
}