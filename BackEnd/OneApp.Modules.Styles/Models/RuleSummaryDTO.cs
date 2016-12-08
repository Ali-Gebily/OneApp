using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OneApp.Modules.Styles.Models
{
    public class RuleSummaryDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// css selector like div, #id,....
        /// </summary>
        [JsonProperty("selector")]
        public string Selector { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        public RuleSummaryDTO CopyRuleSummaryDTO()
        {
            return new RuleSummaryDTO
            {
                Id = this.Id,
                Selector = this.Selector,
                Name = this.Name,
                Category = this.Category,
                Description = this.Description,
            };
         }

    }
}