namespace Horatio_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class helloUserlabortargetdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLabors", "Target", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLabors", "Target");
        }
    }
}
