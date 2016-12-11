using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using OneApp.Modules.Styles.Models;
using OneApp.Modules.Styles.Repositories;

namespace OneApp.Modules.Styles.WebServices.Repositories.EntityFramework
{
    public class StylesEFRepository : IStylesRepository
    {
         

        public System.Threading.Tasks.Task<List<RuleDTO>> GetStyles(RuleEntityScope scope, string entityId)
        {
            throw new NotImplementedException();
        }

        public Task<FileDataDTO> GetFileData(int id )
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<RuleDTO> GetRule(int id,   string entityId)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertFileData(FileDataDTO fileData)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFileData(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RuleDTO> UpdateRuleStyle(RuleDTO rule, string entityId)
        {
            throw new NotImplementedException();
        }

        public Task<List<RuleSummaryDTO>> GetRulesSummary(RuleEntityScope scope)
        {
            throw new NotImplementedException();
        }
    }
}