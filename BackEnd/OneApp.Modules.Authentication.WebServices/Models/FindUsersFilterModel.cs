using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Modules.Authentication.WebServices.Models
{
    public class FindUsersFilterModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Username")]
        [JsonProperty("username_part")]
        public string UsernamePart { get; set; }

    }
}