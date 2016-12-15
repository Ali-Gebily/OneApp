using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Common.Core.DAL;
using OneApp.Common.Core.DAL.Mock;
using OneApp.Modules.Styles.Models;
using OneApp.Modules.Styles.Repositories.Mock.Models;

namespace OneApp.Modules.Styles.Repositories.Mock
{
    public class StylesMockRepository : IStylesRepository
    {
        IMockUnitOfWork _muw = new MockUnitOfWork();

        static StylesMockRepository()
        {
            IMockUnitOfWork muw = new MockUnitOfWork();
            IEntityRepository<MockRule> ruleStore = muw.Repository<MockRule>();

            List<RuleDTO> rules = StylesDataInitializer.GetRules();
            foreach (var item in rules)
            {
                ruleStore.Insert(new MockRule
                {
                    Selector = item.Selector,
                    Name = item.Name,
                    Category = item.Category,
                    Scope = Modules.Styles.Models.RuleEntityScope.Global,
                    DefaultStyle = new MockStyle()
                    {
                        Color = item.Style.Color,
                        BackgroundColor = item.Style.BackgroundColor,
                        BackgroundImage = item.Style.BackgroundImage

                    }

                });

            }
            muw.SaveChanges();
        }



        public async Task<List<RuleDTO>> GetStyles(RuleEntityScope scope, string userId, string entityId)
        {
            return _muw.Repository<MockRule>().GetList(r => r.Scope == scope).Select(r => r.GetRuleDTO(userId, entityId)).ToList();
        }
        public async Task<List<RuleSummaryDTO>> GetRulesSummary(RuleEntityScope scope)
        {
            return _muw.Repository<MockRule>().GetList(r => r.Scope == scope).Select(r => r.GetRuleSummaryDTO()).ToList();
        }
        public async Task<RuleDTO> GetRule(int id, string userId, string entityId)
        {
            return _muw.Repository<MockRule>().GetList(r => r.Id == id).Select(r => r.GetRuleDTO(userId, entityId)).FirstOrDefault();

        }
        public async Task<RuleDTO> UpdateRuleStyle(RuleDTO newRuleDto, IEnumerable<FileDataDTO> fileDtos, string userId, string entityId)
        {

            if (fileDtos != null)
            {
                foreach (var fileData in fileDtos)
                {

                    var mockFile = new MockFile
                    {
                        Data = fileData.Data,
                        ContentType = fileData.ContentType,
                        Length = fileData.Length,
                        Name = fileData.Name
                    };
                    _muw.Repository<MockFile>().Insert(mockFile);
                    _muw.SaveChanges();
                    newRuleDto.Style.SetFilePropertyWithId(fileData.CssProperty, mockFile.Id);

                }
            }

            //remove old files 
            var oldRuleDTO = await this.GetRule(newRuleDto.Id, userId, entityId);
            var clientFilePropertiesValues = newRuleDto.Style.GetFilePropertiesValues();
            var oldFilePropertiesValues = oldRuleDTO.Style.GetFilePropertiesValues();

            foreach (var oldFileIdValue in oldFilePropertiesValues)
            {

                if (!clientFilePropertiesValues.Contains(oldFileIdValue))//the file is delete or replaced with another one, then delete old one
                {
                    _muw.Repository<MockFile>().Delete(_muw.Repository<MockFile>().FirstOrDefault(fd => fd.Id == oldFileIdValue));

                }
            }
            _muw.SaveChanges();
            var oldMockRule = _muw.Repository<MockRule>().GetList(r => r.Id == newRuleDto.Id).FirstOrDefault();
            if (oldRuleDTO == null)
            {
                throw new Exception("No Css rule with Id=" + newRuleDto.Id);
            }
            oldMockRule.SetRuleStyle(newRuleDto.Style, userId, entityId);
            _muw.SaveChanges();

            return oldMockRule.GetRuleDTO(userId, entityId);
        }



        public async Task<FileDataDTO> GetFileData(int id)
        {
            var saveFile = _muw.Repository<MockFile>().FirstOrDefault(fd => fd.Id == id);
            return saveFile == null ? null : saveFile.GetFileDataDTO();
        }



    }
}
