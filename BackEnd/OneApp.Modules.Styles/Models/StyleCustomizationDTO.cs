using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models
{
    public class StyleCustomizationDTO
    {
        public string TenantId { get; set; }

        public string UserId { get; set; }

        public int? EntityType { get; set; }

        public int? EntityId { get; set; }

        public int RuleId { get; set; }

        public int StyleId { get; set; }

    }
}