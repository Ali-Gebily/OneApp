using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using OneApp.Common.Core.DAL.EntityFramework;
using OneApp.Modules.Styles.Models;
using OneApp.Modules.Styles.Repositories;
using OneApp.Modules.Styles.Repositories.EntityFramework;
using OneApp.Modules.Styles.Repositories.EntityFramework.Models;

namespace OneApp.Modules.Styles.WebServices.Repositories.EntityFramework
{
    public class StylesEFRepository : IStylesRepository, IDisposable
    {
        IEFUnitOfWork<StylesDbContext> _muw = new EFUnitOfWork<StylesDbContext>(new StylesDbContext());


        public async Task<List<RuleDTO>> GetStyles(RuleEntityScope scope, string userId, string entityId)
        {
            return _muw.Repository<EFRule>().GetList(r => r.Scope == scope, null, 
                new Expression<Func<EFRule, object>>[] { (r => r.DefaultStyle)}).Select(r => r.GetRuleDTO(userId, entityId)).ToList();

        }
        public async Task<List<RuleSummaryDTO>> GetRulesSummary(RuleEntityScope scope)
        {
            return _muw.Repository<EFRule>().GetList(r => r.Scope == scope).Select(r => r.GetRuleSummaryDTO()).ToList();
        }
        public async Task<RuleDTO> GetRuleDetails(int id, string userId, string entityId)
        {
            return _muw.Repository<EFRule>().GetList(r => r.Id == id, null, new Expression<Func<EFRule, object>>[] { (r => r.DefaultStyle)}).Select(r => r.GetRuleDTO(userId, entityId)).FirstOrDefault();

        }
        public async Task<RuleDTO> UpdateRuleStyle(RuleDTO newRuleDto, IEnumerable<FileDataDTO> fileDtos, string userId, string entityId)
        {
            try
            {
                _muw.BeginTransaction();
                if (fileDtos != null)
                {
                    foreach (var fileData in fileDtos)
                    {

                        var EFFile = new EFFile
                        {
                            Data = fileData.Data,
                            ContentType = fileData.ContentType,
                            Length = fileData.Length,
                            Name = fileData.Name
                        };
                        _muw.Repository<EFFile>().Insert(EFFile);
                        _muw.SaveChanges();
                        newRuleDto.Style.SetFilePropertyWithId(fileData.CssProperty, EFFile.Id);

                    }
                }

                //remove old files 
                var oldRuleDTO = await this.GetRuleDetails(newRuleDto.Id, userId, entityId);
                var clientFilePropertiesValues = newRuleDto.Style.GetFilePropertiesValues();
                var oldFilePropertiesValues = oldRuleDTO.Style.GetFilePropertiesValues();

                foreach (var oldFileIdValue in oldFilePropertiesValues)
                {

                    if (!clientFilePropertiesValues.Contains(oldFileIdValue))//the file is delete or replaced with another one, then delete old one
                    {
                        _muw.Repository<EFFile>().Delete(_muw.Repository<EFFile>().FirstOrDefault(fd => fd.Id == oldFileIdValue));

                    }
                }
                _muw.SaveChanges();
                var oldEFRule = _muw.Repository<EFRule>().GetList(r => r.Id == newRuleDto.Id).FirstOrDefault();
                if (oldRuleDTO == null)
                {
                    throw new Exception("No Css rule with Id=" + newRuleDto.Id);
                }
                oldEFRule.SetRuleStyle(newRuleDto.Style, userId, entityId);
                _muw.SaveChanges();
                _muw.Commit();
                return oldEFRule.GetRuleDTO(userId, entityId);
            }
            catch (Exception ex)
            {
                _muw.Rollback();
                throw ex;
            }
        }

        public async Task<FileDataDTO> GetFileData(int id)
        {
            var saveFile = _muw.Repository<EFFile>().FirstOrDefault(fd => fd.Id == id);
            return saveFile == null ? null : saveFile.GetFileDataDTO();
        }


        public void Dispose()
        {
            _muw.Dispose();
        }
    }
}