using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.EntityFramework.Models
{
    [Table("StyleCustomizations")]
    public class EFStyleCustomization : BaseEFModel
    {

        public string UserId { get; set; }

        public string EntityId { get; set; }

        [Required,ForeignKey("Style")]
        public int StyleId { get; set; }
        public EFStyle Style { get; set; }

        [Required,ForeignKey("Rule")]
        public int RuleId { get; set; }
        public EFRule Rule { get; set; }

    }
}