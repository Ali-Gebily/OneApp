using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.EntityFramework.Models
{

    public class EFStyleCustomization : BaseEFModel
    { 
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public EFStyle Style { get; set; }
        
    }
}