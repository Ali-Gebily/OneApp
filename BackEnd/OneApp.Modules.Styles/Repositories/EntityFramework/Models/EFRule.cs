using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using OneApp.Common.Core.DAL.EntityFramework;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories.EntityFramework.Models
{
    [Table("Rules")]
    public class EFRule : BaseEFModel, IRuleMapper
    {
        [Required]
        public string Selector { get; set; }

        [Required]
        public RuleEntityScope Scope { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        [Required,ForeignKey("DefaultStyle")]
        public int DefaultStyleId { get; set; }

        public EFStyle DefaultStyle { get; set; }


        public virtual ICollection<EFStyleCustomization> StyleCustomizations { get; set; }

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
        void validateParameters(string userId, string entityId)
        {
            switch (this.Scope)
            {
                case RuleEntityScope.Global:
                    break;
                case RuleEntityScope.User:
                    if (string.IsNullOrWhiteSpace(userId))
                    {
                        throw new Exception("userId can't be empty for not global rules");

                    }
                    break;
                default:

                    if (string.IsNullOrWhiteSpace(entityId))
                    {
                        throw new Exception("entityId can't be empty for not global/user rules");

                    }
                    break;
            }

        }
        public RuleDTO GetRuleDTO(string userId, string entityId)
        {
            validateParameters(userId, entityId);
            //we need to implement validator handlers so that we can entityId with respect to scope
            var dto = new RuleDTO();
            setRuleSummaryProperties(dto);

            var customStyle = StyleCustomizations.Where(sc => sc.UserId == userId && sc.EntityId == entityId).FirstOrDefault();
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

        public void SetRuleStyle(StyleDTO dto, string userId, string entityId)
        {
            validateParameters(userId, entityId);
            switch (this.Scope)
            {
                case RuleEntityScope.Global:
                    this.DefaultStyle.SetStyleProperties(dto);
                    break;
                default:
                    var customStyle = StyleCustomizations.Where(sc => sc.UserId == userId && sc.EntityId == entityId).FirstOrDefault();
                    if (customStyle == null)
                    {
                        customStyle = new EFStyleCustomization
                        {
                            EntityId = entityId
                            ,
                            UserId = userId
                        };
                        /*
                        before setting style properties from dto, we need to copy default style first then update 
                        copied object with dto values
                        */
                        customStyle.Style = new EFStyle(this.DefaultStyle);
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