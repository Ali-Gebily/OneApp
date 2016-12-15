namespace OneApp.Modules.Styles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialStylesDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Styling.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.Binary(),
                        ContentType = c.String(),
                        Name = c.String(),
                        Length = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Styling.Rules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Selector = c.String(nullable: false),
                        Scope = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Category = c.String(),
                        DefaultStyleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Styling.Styles", t => t.DefaultStyleId, cascadeDelete: true)
                .Index(t => t.DefaultStyleId);
            
            CreateTable(
                "Styling.Styles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(),
                        BackgroundColor = c.String(),
                        BackgroundImage = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Styling.StyleCustomizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EntityId = c.String(),
                        StyleId = c.Int(nullable: false),
                        RuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Styling.Styles", t => t.StyleId)
                .ForeignKey("Styling.Rules", t => t.RuleId, cascadeDelete: true)
                .Index(t => t.StyleId)
                .Index(t => t.RuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Styling.StyleCustomizations", "RuleId", "Styling.Rules");
            DropForeignKey("Styling.StyleCustomizations", "StyleId", "Styling.Styles");
            DropForeignKey("Styling.Rules", "DefaultStyleId", "Styling.Styles");
            DropIndex("Styling.StyleCustomizations", new[] { "RuleId" });
            DropIndex("Styling.StyleCustomizations", new[] { "StyleId" });
            DropIndex("Styling.Rules", new[] { "DefaultStyleId" });
            DropTable("Styling.StyleCustomizations");
            DropTable("Styling.Styles");
            DropTable("Styling.Rules");
            DropTable("Styling.Files");
        }
    }
}
