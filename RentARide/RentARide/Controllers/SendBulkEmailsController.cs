using RentARide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RentARide.Controllers
{
    public class SendBulkEmailsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Send_Email(SendBulkEmail bulkEmail)
        {
            if (ModelState.IsValid)
            {
                List<string> toEmailList = bulkEmail.ToEmails.Split(',').ToList();
                var content = new MailMessage();
                content.From = new MailAddress(bulkEmail.FromEmail);
                for (int i = 0; i < toEmailList.Count(); i++)
                {
                    content.To.Add(new MailAddress(toEmailList.ElementAt<String>(i)));
                }
                if (bulkEmail.Upload != null && bulkEmail.Upload.ContentLength > 0)
                {
                    content.Attachments.Add(new Attachment(bulkEmail.Upload.InputStream, System.IO.Path.GetFileName(bulkEmail.Upload.FileName)));
                }

                content.Body = bulkEmail.Content;

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.monash.edu.au";
                    ViewBag.Result = "Email has been send.";
                    await smtp.SendMailAsync(content);
                }
                
            }
            return View("Index");
        }
    }
}