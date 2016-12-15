namespace OneApp.Modules.Styles.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Repositories.EntityFramework.Models;
    using Repositories.EntityFramework;
    using System.Collections.Generic;
    using Repositories;
    /// <summary>
    ///For more details about migrations check: https://msdn.microsoft.com/en-us/data/jj591621
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<OneApp.Modules.Styles.Repositories.EntityFramework.StylesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

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
