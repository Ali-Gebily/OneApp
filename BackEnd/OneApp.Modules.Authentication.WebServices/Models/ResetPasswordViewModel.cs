using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Modules.Authentication.WebServices.Models
{
    public class ResetPasswordViewModel : VerifyForgotPasswordEmailViewModel

    {
        [Required]
        [Display(Name = "verification_code")]
        [JsonProperty("verification_code")]
        public string VerificationCode { get; set; }

        [Required]
        [Display(Name = "hash_key")]
        [JsonProperty("hash_key")]
        public string HashKey { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [JsonProperty("password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [JsonProperty("confirm_password")]
        public string ConfirmPassword { get; set; }

    }
}