using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Modules.Authentication.WebServices.Models
{
    /// <summary>
    /// We will confirm email before adding it the database, so that we avoid registering users with invalid emails
    /// </summary>
    public class UserViewModel: ResetPasswordViewModel
    {

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {4} characters long.", MinimumLength = 4)]
        [Display(Name = "Name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Display(Name = "Phone No")]
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Username")]
        [JsonProperty("username")]
        public string Username { get; set; }
    

    }
}