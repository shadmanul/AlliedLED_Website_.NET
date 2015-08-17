using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlliedRenewableEnergyLTD.Models;

namespace AlliedRenewableEnergyLTD.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            using (AlliedEntities db = new AlliedEntities())
            {
                var q = (from p in db.Products
                         orderby p.id descending
                         select p).Take(6);

                return View(q.ToList());
            }
        }

    }
}
