namespace Horatio_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HelloInitial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quests", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Quests", "Descritpion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quests", "Descritpion", c => c.String(nullable: false));
            DropColumn("dbo.Quests", "Description");
        }
    }
}
