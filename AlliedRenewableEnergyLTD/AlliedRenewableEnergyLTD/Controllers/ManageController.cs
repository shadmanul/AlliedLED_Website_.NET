using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlliedRenewableEnergyLTD.Models;

namespace AlliedRenewableEnergyLTD.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/

        public ActionResult Index(string search)
        {
            if (Session["user"] != null)
            {
                using (AlliedEntities db = new AlliedEntities())
                {
                    if (search != null)
                    {
                        var q = from p in db.Products
                                where p.title.Contains(search)
                                orderby p.id descending
                                select p;
                        return View("Index", q.ToList());
                    }
                    var m = from p in db.Products
                            orderby p.id descending
                            select p;
                    return View(m.ToList());
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["user"] != null)
            {
                using (AlliedEntities db = new AlliedEntities())
                {
                    Product p = db.Products.Single(e => e.id == id);
                    db.Products.DeleteObject(p);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Manage");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
