namespace Horatio_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Helloinit : DbMigration
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
                        Description = c.String(nullable: false),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLabors",
                c => new
                    {
                        UserLaborID = c.Int(nullable: false, identity: true),
                        LaborID = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                        UserQuestID = c.Int(nullable: false),
                        Target = c.DateTime(),
                        isComplete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserLaborID)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Labors", t => t.LaborID, cascadeDelete: true)
                .ForeignKey("dbo.UserQuests", t => t.UserQuestID, cascadeDelete: true)
                .Index(t => t.LaborID)
                .Index(t => t.Id)
                .Index(t => t.UserQuestID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Labors", "QuestID", "dbo.Quests");
            DropForeignKey("dbo.Quests", "TopicID", "dbo.Topics");
            DropIndex("dbo.UserQuests", new[] { "Id" });
            DropIndex("dbo.UserQuests", new[] { "QuestID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserLabors", new[] { "UserQuestID" });
            DropIndex("dbo.UserLabors", new[] { "Id" });
            DropIndex("dbo.UserLabors", new[] { "LaborID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Quests", new[] { "TopicID" });
            DropIndex("dbo.Labors", new[] { "QuestID" });
            DropTable("dbo.UserQuests");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserLabors");
            DropTable("dbo.RoleViewModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Topics");
            DropTable("dbo.Quests");
            DropTable("dbo.Labors");
        }
    }
}
