using RentARide.Models;
using RentARide.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Rotativa;
using System.Web.Mvc;

namespace RentARide.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ExportPDF() {
            return new ActionAsPdf("Contact")
            {
                FileName = Server.MapPath("~/Content/contact.pdf")
            };
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Send_Email()
        {
            return View(new Email());
        }





        [HttpPost]
        
        public async Task<ActionResult> Send_Email(Email model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    HttpPostedFileBase upload = model.Upload[0];
                    EmailSender es = new EmailSender();
                    if (upload != null)
                    {
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        upload.SaveAs(path + Path.GetFileName(upload.FileName));
                    }
                    if (upload != null)
                        await es.Send(toEmail, subject, contents, Server.MapPath("~/Uploads/" + Path.GetFileName(upload.FileName)), upload.FileName);
                    else
                        await es.Send(toEmail, subject, contents, null, null);
                    ViewBag.Result = "Email has been sent.";

                    ModelState.Clear();

                    return View(new Email());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}