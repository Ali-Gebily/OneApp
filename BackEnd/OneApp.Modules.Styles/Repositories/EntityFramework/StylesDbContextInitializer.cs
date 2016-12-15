using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                context.Rules.AddOrUpdate(a => new { a.Selector }, new EFRule
                {
                    Selector = item.Selector,
                    Name = item.Name,
                    Category = item.Category,
                    Scope = item.Scope,
                    DefaultStyle = new EFStyle()
                    {
                        Color = item.Style.Color,
                        BackgroundColor = item.Style.BackgroundColor,
                        BackgroundImage = item.Style.BackgroundImage

                    }

                });

            }
            context.SaveChanges();
        }
          
    }
}