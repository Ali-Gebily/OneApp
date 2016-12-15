namespace OneApp.Modules.Styles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreationDateToRule : DbMigration
    {
        public override void Up()
        {
            AddColumn("Styling.Rules", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Styling.Rules", "CreationDate");
        }
    }
}
