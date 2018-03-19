namespace Horatio_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hellotablestodatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Labors",
                c => new
                    {
                        LaborID = c.Int(nullable: false, identity: true),
                        QuestID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 509),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.LaborID)
                .ForeignKey("dbo.Quests", t => t.QuestID, cascadeDelete: true)
                .Index(t => t.QuestID);
            
            CreateTable(
                "dbo.Quests",
                c => new
                    {
                        QuestID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Descritpion = c.String(nullable: false),
                        TopicID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestID)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .Index(t => t.TopicID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        Theme = c.String(),
                    })
                .PrimaryKey(t => t.TopicID);
            
            CreateTable(
                "dbo.UserLabors",
                c => new
                    {
                        UserLaborID = c.Int(nullable: false, identity: true),
                        LaborID = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                        UserQuestID = c.Int(nullable: false),
                        isComplete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserLaborID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Labors", t => t.LaborID, cascadeDelete: true)
                .ForeignKey("dbo.UserQuests", t => t.UserQuestID, cascadeDelete: false)
                .Index(t => t.LaborID)
                .Index(t => t.Id)
                .Index(t => t.UserQuestID);
            
            CreateTable(
                "dbo.UserQuests",
                c => new
                    {
                        UserQuestID = c.Int(nullable: false, identity: true),
                        QuestID = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                        isActive = c.Boolean(nullable: false),
                        isComplete = c.Boolean(nullable: false),
                        Target = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserQuestID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Quests", t => t.QuestID, cascadeDelete: false)
                .Index(t => t.QuestID)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLabors", "UserQuestID", "dbo.UserQuests");
            DropForeignKey("dbo.UserQuests", "QuestID", "dbo.Quests");
            DropForeignKey("dbo.UserQuests", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserLabors", "LaborID", "dbo.Labors");
            DropForeignKey("dbo.UserLabors", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Labors", "QuestID", "dbo.Quests");
            DropForeignKey("dbo.Quests", "TopicID", "dbo.Topics");
            DropIndex("dbo.UserQuests", new[] { "Id" });
            DropIndex("dbo.UserQuests", new[] { "QuestID" });
            DropIndex("dbo.UserLabors", new[] { "UserQuestID" });
            DropIndex("dbo.UserLabors", new[] { "Id" });
            DropIndex("dbo.UserLabors", new[] { "LaborID" });
            DropIndex("dbo.Quests", new[] { "TopicID" });
            DropIndex("dbo.Labors", new[] { "QuestID" });
            DropTable("dbo.UserQuests");
            DropTable("dbo.UserLabors");
            DropTable("dbo.Topics");
            DropTable("dbo.Quests");
            DropTable("dbo.Labors");
        }
    }
}
