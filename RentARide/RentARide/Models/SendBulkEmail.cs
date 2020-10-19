using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentARide.Models
{
    public class SendBulkEmail
    {
        [Display(Name = "To Emails")]
        [Required(ErrorMessage = "Please enter emails seperated by commas.")]
        public string ToEmails { get; set; }

        [Display(Name = "Your Email")]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string FromEmail { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Please enter a email contents.")]
        public string Content { get; set; }

        public HttpPostedFileBase Upload { get; set; }
    }
}