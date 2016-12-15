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
            
            this.Database.Log = Console.Write;

            //Configure default schema
            modelBuilder.HasDefaultSchema("Styling");

            var stylesTable = modelBuilder.Entity<EFStyle>().ToTable("Styles");
            stylesTable.HasKey<int>(r => r.Id);


            var rulesTable = modelBuilder.Entity<EFRule>().ToTable("Rules");
            rulesTable.HasKey<int>(r => r.Id); 
            rulesTable.Property(p => p.Selector).IsRequired();
            rulesTable.Property(p => p.Name).IsRequired();
            rulesTable.Property(p => p.Scope).IsRequired();
            rulesTable.HasRequired(r => r.DefaultStyle).WithOptional().Map( s => s.MapKey("DefaultStyleId"));
            rulesTable.HasMany(r => r.StyleCustomizations).WithRequired().Map( sc =>sc.MapKey("RuleId")) ;

            

            var styleCustomizationsTable = modelBuilder.Entity<EFStyleCustomization>().ToTable("StyleCustomizations");
            styleCustomizationsTable.HasKey<int>(r => r.Id);
            styleCustomizationsTable.HasRequired(r => r.Style).WithOptional().Map(sc => sc.MapKey("StyleId")).WillCascadeOnDelete(false);




            var filesTable = modelBuilder.Entity<EFFile>().ToTable("Files");
            filesTable.HasKey<int>(r => r.Id);



            base.OnModelCreating(modelBuilder);
        }
    }
}