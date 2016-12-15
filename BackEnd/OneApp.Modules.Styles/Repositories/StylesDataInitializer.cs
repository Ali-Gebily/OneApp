using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneApp.Modules.Styles.Models;

namespace OneApp.Modules.Styles.Repositories
{
    public class StylesDataInitializer
    {
        public static List<RuleDTO> GetRules()
        {
            List<RuleDTO> masterRules = new List<RuleDTO>();
            masterRules.Add(new RuleDTO
            {
                Selector = ".page-top",
                Name = "Header",
                Category = "Header",
                Scope = Modules.Styles.Models.RuleEntityScope.Global,
                Style = new StyleDTO()
                {
                    BackgroundColor = ""

                }

            });
            masterRules.Add(new RuleDTO
            {
                Selector = ".auth-block",
                Name = "Authentication Background",
                Category = "Authentication",
                Description = "Manage background for authentiaction screens like login and register",
                Scope = Modules.Styles.Models.RuleEntityScope.Global,
                Style = new StyleDTO()
                {
                    BackgroundColor = ""

                }

            });

            masterRules.Add(new RuleDTO
            {
                Selector = ".auth-block a",
                Name = "Authentication Text",
                Category = "Authentication",
                Description = "Manage text color for authentiaction screens like login and register",
                Scope = Modules.Styles.Models.RuleEntityScope.Global,
                Style = new StyleDTO()
                {
                    Color = ""

                }

            });


            masterRules.Add(new RuleDTO
            {
                Selector = ".al-sidebar",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Side Bar Background",
                Category = "Side Bar",
                Style = new StyleDTO()
                {
                    BackgroundColor = "",

                }

            });
            masterRules.Add(new RuleDTO
            {
                Selector = "a.al-sidebar-list-link",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Side Bar Text Color",
                Category = "Side Bar",
                Style = new StyleDTO()
                {
                    Color = ""

                }

            });

            masterRules.Add(new RuleDTO
            {
                Selector = "main::before",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Main Content",
                Category = "Main Content",
                Style = new StyleDTO()
                {
                    BackgroundImage = 0

                }
            });

            masterRules.Add(new RuleDTO
            {
                Selector = ".pie-charts .pie-chart-item",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Pie Chart Item",
                Category = "Dashboard",
                Description = "Manage text color for pie chart in dashboard screen",
                Style = new StyleDTO()
                {
                    Color = ""

                }
            });
            masterRules.Add(new RuleDTO
            {
                Selector = ".channels-block",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Channels",
                Category = "Dashboard",
                Description = "Manage text color for channels in dashboard screen",

                Style = new StyleDTO()
                {
                    Color = ""

                }
            });
            return masterRules;

        }
    }
}