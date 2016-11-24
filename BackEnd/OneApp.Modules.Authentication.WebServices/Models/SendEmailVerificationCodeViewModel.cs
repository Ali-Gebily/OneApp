using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Modules.Authentication.WebServices.Models
{
    public class SendEmailVerificationCodeViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}