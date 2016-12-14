using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OneApp.Common.Core.DAL.EntityFramework;
using OneApp.Common.Core.Managers.Settings;
using OneApp.Modules.Styles.Repositories.EntityFramework.Models;

namespace OneApp.Modules.Styles.Repositories.EntityFramework
{
    public class StylesDbContext : BaseDbContext
    {
        public StylesDbContext()
        {
            Database.SetInitializer<StylesDbContext>(new StylesDbContextInitializer());

        }
        public DbSet<EFRule> Rules { get; set; }

      
        //They should not't be accessed directly from context, they will be accessed through Rules
        //public DbSet<EFStyle> Styles { get; set; }
        //public DbSet<EFStyleCustomization> StyleCustomizations { get; set; }

        public DbSet<EFFile> Files { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("Styling");

        }
    }
}