using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace AlliedRenewableEnergyLTD.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name, string email, string phone, string message)
        {
            try
            {
                //Create the msg object to be sent
                MailMessage msg = new MailMessage();
                //Add your email address to the recipients
                msg.To.Add("sales@myalliedbd.com");
                //Configure the address we are sending the mail from
                MailAddress address = new MailAddress("website@myalliedbd.com");
                msg.From = address;
                msg.Subject = "Website Feedback";
                string body = "From: " + name + "\n";
                body += "Email: " + email + "\n";
                body += "phone: " + phone + "\n\n";
                body += message + "\n";
                msg.Body = body;

                SmtpClient client = new SmtpClient();
                client.Host = "relay-hosting.secureserver.net";
                client.Port = 25;
                //Send the msg
                client.Send(msg);

                ViewBag.msg = "Mail Sent. Thank You For Contacting Us.";
                return View();
            }
            catch(Exception)
            {
                ViewBag.msg = "Something Is Wrong. Try Again.";
                return View();
            }
        }
    }
}
