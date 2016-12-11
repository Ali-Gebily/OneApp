using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneApp.Modules.Styles.Repositories.Mock.Models
{
    public class StylesMockContext
    {

        public static List<MockRule> Rules { get; private set; } = new List<MockRule>();
        public static List<MockStyle> Styles { get; private set; } = new List<MockStyle>();
        public static List<MockStyleCustomization> StyleCustomizations { get; private set; } = new List<MockStyleCustomization>();
        public static List<MockFile> Files { get; private set; } = new List<MockFile>();

        static StylesMockContext()
        {

            StylesMockContext.Rules.Add(new MockRule
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
            StylesMockContext.Rules.Add(new MockRule
            {
                Selector = ".auth-block",
                Name = "Authentication Background",
                Category = "Authentication",
                Description="Manage background for authentiaction screens like login and register",
                Scope = Modules.Styles.Models.RuleEntityScope.Global,
                DefaultStyle = new MockStyle()
                {
                    BackgroundColor = ""

                }

            });

            StylesMockContext.Rules.Add(new MockRule
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

             
            StylesMockContext.Rules.Add(new MockRule
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
            StylesMockContext.Rules.Add(new MockRule
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

            StylesMockContext.Rules.Add(new MockRule
            {
                Selector = "main::before",
                Scope = Modules.Styles.Models.RuleEntityScope.User,
                Name = "Main Content",
                Category = "Main Content",
                DefaultStyle = new MockStyle()
                {
                    BackgroundImage = 0

                }
            } );

            StylesMockContext.Rules.Add(new MockRule
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
            StylesMockContext.Rules.Add(new MockRule
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
        }
         

    }
}