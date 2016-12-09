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
                DefaultStyle = new MockStyle()
                {
                    BackgroundColor = ""

                }

            });
            StylesMockContext.Rules.Add(new MockRule
            {
                Selector = ".al-sidebar",
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
                Name = "Main Content Background image",
                Category = "Main Content",
                DefaultStyle = new MockStyle()
                {
                    BackgroundImage = 0

                }
            }

            );
        }



    }
}