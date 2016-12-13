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

            ruleStore.Insert(new MockRule
            {
                Selector = ".page-top",
                Name = "Header",
                Category = "Header",
                Scope = Modules.Styles.Models.RuleEntityScope.Global,
                DefaultStyle = new MockStyle()
                {
                    BackgroundColor = ""

                }

            });
            ruleStore.Insert(new MockRule
            {
                Selector = ".auth-block",
                Name = "Authentication Background",
                Category = "Authentication",
                Description = "Manage background for authentiaction screens like login and register",
                Scope = Modules.Styles.Models.RuleEntityScope.Global,
                DefaultStyle = new MockStyle()
                {
                    BackgroundColor = ""

                }

            });

            ruleStore.Insert(new MockRule
            {
                Selector = ".auth-block a",
                Name = "Authentication Text",
                Category = "Authentication",
                Description = "Manage text color for authentiaction screens like login and register",
                Scope = Modules.Styles.Models.RuleEntityScope.Global,
                DefaultStyle = new MockStyle()
                {
                    Color = ""

                }

            });


            ruleStore.Insert(new MockRule
            {
                Selector = ".al-sidebar",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Side Bar Background",
                Category = "Side Bar",
                DefaultStyle = new MockStyle()
                {
                    BackgroundColor = "",

                }

            });
            ruleStore.Insert(new MockRule
            {
                Selector = "a.al-sidebar-list-link",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Side Bar Text Color",
                Category = "Side Bar",
                DefaultStyle = new MockStyle()
                {
                    Color = ""

                }

            });

            ruleStore.Insert(new MockRule
            {
                Selector = "main::before",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Main Content",
                Category = "Main Content",
                DefaultStyle = new MockStyle()
                {
                    BackgroundImage = 0

                }
            });

            ruleStore.Insert(new MockRule
            {
                Selector = ".pie-charts .pie-chart-item",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Pie Chart Item",
                Category = "Dashboard",
                Description = "Manage text color for pie chart in dashboard screen",
                DefaultStyle = new MockStyle()
                {
                    Color = ""

                }
            });
            ruleStore.Insert(new MockRule
            {
                Selector = ".channels-block",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Channels",
                Category = "Dashboard",
                Description = "Manage text color for channels in dashboard screen",

                DefaultStyle = new MockStyle()
                {
                    Color = ""

                }
            });
            muw.SaveChanges();
        }

        

        public async Task<List<RuleDTO>> GetStyles(RuleEntityScope scope, string entityId)
        {
            return _muw.Repository<MockRule>().GetList(r => r.Scope == scope).Select(r => r.GetRuleDTO(entityId)).ToList();
        }
        public async Task<List<RuleSummaryDTO>> GetRulesSummary(RuleEntityScope scope)
        {
            return _muw.Repository<MockRule>().GetList(r => r.Scope == scope).Select(r => r.GetRuleSummaryDTO()).ToList();
        }
        public async Task<RuleDTO> GetRule(int id, string entityId)
        {
            return _muw.Repository<MockRule>().GetList(r => r.Id == id).Select(r => r.GetRuleDTO(entityId)).FirstOrDefault();

        }
        public async Task<RuleDTO> UpdateRuleStyle(RuleDTO newRule, string entityId)
        {
            var oldRule = _muw.Repository<MockRule>().GetList(r => r.Id == newRule.Id).FirstOrDefault();
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
            _muw.Repository<MockFile>().Insert(mockFile);
            _muw.SaveChanges();
            return mockFile.Id;
        }

        public async Task<FileDataDTO> GetFileData(int id)
        {
            var saveFile = _muw.Repository<MockFile>().FirstOrDefault(fd => fd.Id == id);
            return saveFile == null ? null : new FileDataDTO(saveFile);
        }
        public async Task RemoveFileData(int id)
        {
            _muw.Repository<MockFile>().Delete(_muw.Repository<MockFile>().FirstOrDefault(fd => fd.Id == id));
            _muw.SaveChanges();
        }


    }
}
