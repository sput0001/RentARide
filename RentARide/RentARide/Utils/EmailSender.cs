using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RentARide.Utils
{
    public class EmailSender
    {
        private const String API_KEY = "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";

        public async Task Send(String toEmail, String subject, String contents, string upload, string fileName)
        {
            var apiKey = Environment.GetEnvironmentVariable(API_KEY);
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("shriyarox@gmail.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var to1 = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var message = new SendGridMessage();
            message.From = from;

            message.AddTo(to);
            message.AddTo(to1);
            message.Subject = subject;
            message.HtmlContent = htmlContent;
            message.PlainTextContent = plainTextContent;
            if (upload != null)
            {
                var bytes = File.ReadAllBytes(upload);
                var file = Convert.ToBase64String(bytes);
                message.AddAttachment(fileName, file);
            }
            var response = await client.SendEmailAsync(message);
        }
    }
}