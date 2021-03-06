﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.WebServices.Models;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories
{
    public interface IStylesRepository
    {
        Task<List<RuleDTO>> GetStyles(RuleEntityScope scope, string userId, string entityId);

        Task<List<RuleSummaryDTO>> GetRulesSummary(RuleEntityScope scope);
        Task<RuleDTO> GetRuleDetails(int id, string userId, string entityId);
        //we will return RuleDTO again to make sure that data is saved successfully
        Task<RuleDTO> UpdateRuleStyle(RuleDTO newRuleDto, IEnumerable<FileDataDTO> newFilesDtos, string userId, string entityId);

        Task<FileDataDTO> GetFileData(int id);
    }
}
