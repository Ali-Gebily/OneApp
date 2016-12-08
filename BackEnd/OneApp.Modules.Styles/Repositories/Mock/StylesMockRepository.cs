﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories.Mock
{
    public class StylesMockRepository : IStylesRepository
    {
        static List<RuleDTO> _rules = new List<RuleDTO>();
        static List<FileDataDTO> _filesData = new List<FileDataDTO>();
        static StylesMockRepository()
        {
            _rules.Add(new RuleDTO
            {
                Id = 1,
                Selector = ".page-top",
                Name = "Header",
                Category = "Header",
                Style = new StyleDTO()
                {
                    BackgroundColor = ""

                }

            });
            _rules.Add(new RuleDTO
            {
                Id = 2,
                Selector = ".al-sidebar",
                Name = "Side Bar Background",
                Category = "Side Bar",
                Style = new StyleDTO()
                {
                    BackgroundColor = "",

                }

            });
            _rules.Add(new RuleDTO
            {
                Id = 3,
                Selector = "a.al-sidebar-list-link",
                Name = "Side Bar Text Color",
                Category = "Side Bar",
                Style = new StyleDTO()
                {
                    Color = ""

                }

            });

            _rules.Add(new RuleDTO
            {
                Id = 4,
                Selector = "main::before",
                Name = "Main Content Background image",
                Category = "Main Content",
                Style = new StyleDTO()
                {
                    BackgroundImage = ""

                }
            }

            );
        }

        public async Task<RuleDTO> GetRule(int id)
        {
            return _rules.FirstOrDefault(style => style.Id == id);
        }

        public async Task<List<RuleDTO>> GetAllStyles()
        {
            return _rules;
        }

        public async Task<RuleDTO> UpdateRuleStyle(RuleDTO newRule)
        {
            var oldRule = _rules.Find(r => r.Id == newRule.Id);
            if (oldRule == null)
            {
                throw new Exception("No Css rule with Id=" + newRule.Id);
            }
            //we may use reflection too loop through all properties 
            oldRule.Style.Color = newRule.Style.Color;
            oldRule.Style.BackgroundColor = newRule.Style.BackgroundColor;
            oldRule.Style.BackgroundImage = newRule.Style.BackgroundImage;


            return oldRule;
        }

        public async Task<string> InsertFileData(FileDataDTO fileData)
        {
            _filesData.Add(fileData);
            fileData.Id = DateTime.Now.Ticks.ToString();
            return fileData.Id;
        }

        public async Task<FileDataDTO> GetFileData(string id)
        {
            return _filesData.FirstOrDefault(fd => fd.Id == id);
        }
        public async Task RemoveFileData(string id)
        {
            _filesData.Remove(_filesData.Find(it => it.Id == id));
        }
    }
}
