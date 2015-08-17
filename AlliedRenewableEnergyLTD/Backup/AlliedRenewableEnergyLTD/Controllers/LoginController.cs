using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlliedRenewableEnergyLTD.Models;

namespace AlliedRenewableEnergyLTD.Controllers
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
        public ActionResult Index(string UsernameTB, string PasswordTB)
        {
            if (UsernameTB != null || PasswordTB != null)
            {
                string username = UsernameTB;
                string password = PasswordTB;
                AlliedEntities db = new AlliedEntities();
                var q = (from p in db.LoginInfos
                         where p.username.Equals(username) && p.password.Equals(password)
                         select p);
                if (q.Count() == 1)
                {
                    Session["user"] = q.FirstOrDefault().username.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    @ViewBag.msg = "Incorrect Username/Password";
                    return View();
                }
            }
            else
            {
                @ViewBag.msg = "Incorrect Username/Password";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
