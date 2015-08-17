using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlliedRenewableEnergyLTD.Models;
using System.IO;

namespace AlliedRenewableEnergyLTD.Controllers
{
    public class AddProductsController : Controller
    {
        //
        // GET: /AddProducts/

        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Index(Product item, HttpPostedFileBase FileUpload)
        {
            if (ModelState.IsValid){
                try{
                    using(AlliedEntities db = new AlliedEntities()){
                        if (FileUpload != null)
                        {
                            if (FileUpload.ContentLength <= (512 * 512) && (FileUpload.ContentType.Contains("/jpg") || FileUpload.ContentType.Contains("/jpeg") || FileUpload.ContentType.Contains("/png") || FileUpload.ContentType.Contains("/bmp")))
                            {
                                item.imagetype = FileUpload.ContentType;
                                item.imagebytes = ConverToByte(FileUpload);
                                ViewBag.Msg = null;
                            }
                            else
                            {
                                ViewBag.Msg = "Invalid File";
                            }
                        }
                        if (ViewBag.Msg == null)
                        {
                            item.dateadded = DateTime.Now.Date;
                            db.Products.AddObject(item);
                            db.SaveChanges();
                            ViewBag.Msg = "Successfully Added";
                            return View();
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
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
        //if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            contact.Username = Session["tempuser"].ToString();

        //            using (ProjectDatabaseContext db = new ProjectDatabaseContext())
        //            {
        //                if (FileUpload != null)
        //                {
        //                    if (FileUpload.ContentLength < (1024 * 1024) && (FileUpload.ContentType.Contains("/jpg") || FileUpload.ContentType.Contains("/jpeg") || FileUpload.ContentType.Contains("/png") || FileUpload.ContentType.Contains("/bmp")))
        //                    {
        //                        contact.ImageContentType = FileUpload.ContentType;
        //                        contact.ImageBytes = ConverToByte(FileUpload);
        //                        ViewBag.Msg = null;
        //                    }
        //                    else
        //                    {
        //                        ViewBag.Msg = "<span align='center'>Invalid File</span><br /><br />";
        //                    }
        //                }
        //                if (ViewBag.Msg == null)
        //                {
        //                    db.Contacts.AddObject(contact);
        //                    db.SaveChanges();
        //                    return RedirectToAction("Success", "Registration");
        //                }
        //                else
        //                {
        //                    return View();
        //                }
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            return View();
        //        }
        //    }
    
