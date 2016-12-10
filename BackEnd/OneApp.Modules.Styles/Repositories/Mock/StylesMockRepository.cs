using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Modules.Styles.Models;
using OneApp.Modules.Styles.Repositories.Mock.Models;

namespace OneApp.Modules.Styles.Repositories.Mock
{
    public class StylesMockRepository : IStylesRepository
    { 
        public async Task<RuleDTO> GetRule(int id, string entityId)
        {
            var rule = StylesMockContext.Rules.FirstOrDefault(r => r.Id == id);
            return rule.GetRuleDTO(entityId);

        }

        public async Task<List<RuleDTO>> GetStyles(string entityId)
        {
            return StylesMockContext.Rules.Select(r => r.GetRuleDTO(entityId)).ToList();
        }

        public async Task<RuleDTO> UpdateRuleStyle(RuleDTO newRule, string entityId)
        {
            var oldRule = StylesMockContext.Rules.Find(r => r.Id == newRule.Id);
            if (oldRule == null)
            {
                throw new Exception("No Css rule with Id=" + newRule.Id);
            }
            oldRule.SetRuleStyle(newRule.Style, entityId);
          
            return oldRule.GetRuleDTO(entityId);
        }

        public async Task<int> InsertFileData(FileDataDTO fileData)
        {
            var mockFile = new MockFile
            {
                Data = fileData.Data,
                ContentType = fileData.ContentType,
                Length = fileData.Length,
                Name = fileData.Name
            };
            StylesMockContext.Files.Add(mockFile);
            return mockFile.Id;
        }

        public async Task<FileDataDTO> GetFileData(int id)
        {
            var saveFile = StylesMockContext.Files.FirstOrDefault(fd => fd.Id == id);
            return saveFile == null ? null : new FileDataDTO(saveFile);
        }
        public async Task RemoveFileData(int id)
        {
            StylesMockContext.Files.Remove(StylesMockContext.Files.FirstOrDefault(fd => fd.Id == id));
        }
    }
}
