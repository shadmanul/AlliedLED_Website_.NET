using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlliedRenewableEnergyLTD.Models;

namespace AlliedRenewableEnergyLTD.Controllers
{
    public class ProductsController : Controller
    {
        //
        // GET: /Products/

        public ActionResult Index(string category, string search)
        {
            using(AlliedEntities db = new AlliedEntities())
            {
                if (search != null)
                {
                    ViewBag.Title = "Allied - " + search;
                    var q = from p in db.Products
                            where p.title.Contains(search)
                            orderby p.id descending
                            select p;
                    return View("Index", q.ToList());
                }
                if (category != null)
                {
                    string Category = category;
                    ViewBag.Title = "Allied - " + category;

                    var m = from p in db.Products
                            where p.category.Equals(Category)
                            orderby p.id descending
                            select p;
                    return View(m.ToList());
                }
                else
                {
                    ViewBag.Title = "Allied - Products";

                    var m = from p in db.Products
                            orderby p.id descending
                            select p;
                    return View(m.ToList());
                }

            }
        }
        public FileContentResult GetImage(int id)
        {
            AlliedEntities db = new AlliedEntities();
            var q = from p in db.Products
                    where p.id == id
                    select p;
            byte[] byteArray = q.FirstOrDefault().imagebytes;
            if (byteArray != null)
            {
                return new FileContentResult(byteArray, q.FirstOrDefault().imagetype);
            }
            else
            {
                return null;
            }
        }

    }

}
