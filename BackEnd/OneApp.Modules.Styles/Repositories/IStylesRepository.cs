using System;
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
        Task<List<RuleDTO>> GetAllStyles();
        Task<RuleDTO> GetRule(int id);
        //we will return RuleDTO again to make sure that data is saved successfully
        Task<RuleDTO> UpdateRuleStyle(RuleDTO rule);

        Task<string> InsertFileData(FileDataDTO fileData);
        Task<FileDataDTO> GetFileData(string id);
        Task RemoveFileData(string id);
    }
}
