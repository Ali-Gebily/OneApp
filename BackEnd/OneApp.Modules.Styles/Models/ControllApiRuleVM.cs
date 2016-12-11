using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Modules.Styles.Models
{
    public class GetRuleVM
    {
          public int id { get; set; }

          public string entity_id { get; set; }
         
 
    }
    public class GetFormattedRuleVM
    {

        public RuleEntityScope Scope { get; set; }

        public string entity_id { get; set; }

      
        public string base_url { get; set; }


    }
}