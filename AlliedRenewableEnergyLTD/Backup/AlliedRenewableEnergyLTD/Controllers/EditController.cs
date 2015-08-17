using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlliedRenewableEnergyLTD.Models;
using System.IO;


namespace AlliedRenewableEnergyLTD.Controllers
{
    public class EditController : Controller
    {
        //
        // GET: /Edit/

        public ActionResult Index(int id)
        {
            if (Session["user"] != null)
            {
                using (AlliedEntities db = new AlliedEntities())
                {
                    var q = (from p in db.Products
                             where p.id == id
                             select p).FirstOrDefault();
                    return View(q);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Index(Product product, HttpPostedFileBase FileUpload)
        {
            using (AlliedEntities db = new AlliedEntities())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ViewBag.Msg = null;
                        if (FileUpload != null)
                        {
                            if (FileUpload.ContentLength <= (512 * 512) && (FileUpload.ContentType.Contains("/jpg") || FileUpload.ContentType.Contains("/jpeg") || FileUpload.ContentType.Contains("/png") || FileUpload.ContentType.Contains("/bmp")))
                            {
                                product.imagetype = FileUpload.ContentType;
                                product.imagebytes = ConverToByte(FileUpload);
                                ViewBag.Msg = null;
                            }
                            else
                            {
                                ViewBag.Msg = "Invalid File";
                            }
                        }
                        if (ViewBag.Msg == null)
                        {
                            db.Products.Attach(product);
                            db.ObjectStateManager.ChangeObjectState(product, System.Data.EntityState.Modified);
                            db.SaveChanges();
                            return RedirectToAction("Index", "Manage");
                        }
                        else
                        {
                            var q = (from p in db.Products
                                     where p.id == product.id
                                     select p).FirstOrDefault();
                            ViewBag.Msg = "Something went wrong";
                            return View(q);
                        }
                        
                    }
                    else
                    {
                        var q = (from p in db.Products
                                 where p.id == product.id
                                 select p).FirstOrDefault();
                        ViewBag.Msg = "Something went wrong";
                        return View(q);
                    }
                }
                catch (Exception e)
                {
                    var q = (from p in db.Products
                             where p.id == product.id
                             select p).FirstOrDefault();
                    ViewBag.Msg = "Something went wrong";
                    return View(q);
                }
            }
        }
        private byte[] ConverToByte(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }
    }
}
