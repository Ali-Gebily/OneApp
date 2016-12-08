using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OneApp.Modules.Styles.Models.CSSAttributes;

namespace OneApp.Modules.Styles.Models
{
    public class RuleDTO : RuleSummaryDTO
    {

        [JsonProperty("style")]
        public StyleDTO Style { get; set; } = new StyleDTO();

        public string Format(string baseUrl)
        { 
            return new StringBuilder(Selector + " {\n").Append(Style.GetFormattedStyle(baseUrl,this.Id)).Append("}").ToString();
        }




    }
}
