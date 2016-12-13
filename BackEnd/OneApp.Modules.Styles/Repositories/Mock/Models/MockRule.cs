using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneApp.Common.Core.DAL.Mock;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class MockRule : MockBaseModel, IRuleMapper
    {

        public string Selector { get; set; }

        public RuleEntityScope Scope { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

         

        public MockStyle DefaultStyle{ get; set; }
        List<MockStyleCustomization> StyleCustomizations { get; set; } = new List<MockStyleCustomization>();

        public RuleSummaryDTO GetRuleSummaryDTO()
        {
            var summaryDto = new RuleSummaryDTO();
            setRuleSummaryProperties(summaryDto);
            return summaryDto;
        }
        void setRuleSummaryProperties(RuleSummaryDTO dto)
        {
            dto.Id = this.Id;
            dto.Selector = this.Selector;
            dto.Scope = this.Scope;
            dto.Name = this.Name;
            dto.Description = this.Description;
            dto.Category = this.Category;
        }
        public RuleDTO GetRuleDTO( string entityId)
        { 

            if (string.IsNullOrWhiteSpace(entityId) && this.Scope != RuleEntityScope.Global)
            {
                throw new Exception("entityId can't be empty for not global rules");

            }
            //we need to implement validator handlers so that we can entityId with respect to scope
            var dto = new RuleDTO();
            setRuleSummaryProperties(dto);

            var customStyle = StyleCustomizations.Where(sc => sc.EntityId == entityId).FirstOrDefault();
            if (customStyle == null)
            {
                dto.Style = this.DefaultStyle.GetStyleDTO();
            }
            else
            {
                dto.Style = customStyle.Style.GetStyleDTO();
            }
            return dto;
        }

        public void SetRuleStyle(StyleDTO dto, string entityId)
        {
            switch (this.Scope)
            {
                case RuleEntityScope.Global:
                    this.DefaultStyle.SetStyleProperties(dto);
                    break;
                default:
                    if (string.IsNullOrEmpty(entityId))
                    {
                        throw new Exception("EntityId can't be null for if scope is not All");
                    }
                    var customStyle = StyleCustomizations.Where(sc => sc.EntityId == entityId).FirstOrDefault();
                    if (customStyle == null)
                    {
                        customStyle = new MockStyleCustomization
                        {
                            EntityId = entityId
                        };
                        /*
                        before setting style properties from dto, we need to copy default style first then update 
                        copied object with dto values
                        */
                        customStyle.Style = new MockStyle(this.DefaultStyle);
                        customStyle.Style.SetStyleProperties(dto);
                        StyleCustomizations.Add(customStyle);
                    }
                    else
                    {
                        customStyle.Style.SetStyleProperties(dto);
                    }
                    break;
            }



        }


    }
}