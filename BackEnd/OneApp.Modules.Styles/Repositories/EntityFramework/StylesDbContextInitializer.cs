using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OneApp.Modules.Styles.Repositories.EntityFramework.Models;

namespace OneApp.Modules.Styles.Repositories.EntityFramework
{
    public class StylesDbContextInitializer : CreateDatabaseIfNotExists<StylesDbContext>
    {
        protected override void Seed(StylesDbContext context)
        {
           
        }
    }
}