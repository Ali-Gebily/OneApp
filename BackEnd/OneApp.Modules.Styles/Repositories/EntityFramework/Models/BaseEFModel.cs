using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.EntityFramework.Models
{
    public class BaseEFModel
    {
        [Key]
        public int Id { get; set; }


    }
}