using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OneApp.Modules.Styles.Models;
using OneApp.Modules.Styles.Repositories.EntityFramework.Models;

namespace OneApp.Modules.Styles.Repositories.EntityFramework
{
    public class StylesDbContextInitializer : CreateDatabaseIfNotExists<StylesDbContext>
    {
        protected override void Seed(StylesDbContext context)
        {
            List<RuleDTO> rules = StylesDataInitializer.GetRules();
            
            foreach (var item in rules)
            {
                context.Rules.Add(new EFRule
                {
                    Selector = item.Selector,
                    Name = item.Name,
                    Category = item.Category,
                    Scope =item.Scope,
                    DefaultStyle = new EFStyle()
                    {
                        Color = item.Style.Color,
                        BackgroundColor = item.Style.BackgroundColor,
                        BackgroundImage = item.Style.BackgroundImage

                    }

                });

            }
        }
    }
}