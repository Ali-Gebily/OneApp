using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using OneApp.Modules.Styles.Repositories.Mock.Models;

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

        public RuleSummaryDTO()
        {

        }
        public RuleSummaryDTO(MockRule rule)
        {
            this.Id = rule.Id;
            this.Selector = rule.Selector;
            this.Name = rule.Name;
            this.Description = rule.Description;
            this.Category = rule.Category;
        }

    }
}