using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Models
{
    public interface IRuleMapper
    {
        RuleDTO GetRuleDTO(string entityId);
        void SetRuleStyle(StyleDTO dto, string entityId);

    }
}