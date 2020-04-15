namespace Test_Chatting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chatv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.ChatBox",
                c => new
                    {
                        ChatBoxId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ChatBoxId)
                .ForeignKey("dbo.Account", t => t.ChatBoxId)
                .Index(t => t.ChatBoxId);
            
            CreateTable(
                "dbo.Chat",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreatedDate = c.DateTime(),
                        PathImage = c.String(),
                        Account_AccountId = c.Int(),
                        ChatBox_ChatBoxId = c.Int(),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.Account", t => t.Account_AccountId)
                .ForeignKey("dbo.ChatBox", t => t.ChatBox_ChatBoxId)
                .Index(t => t.Account_AccountId)
                .Index(t => t.ChatBox_ChatBoxId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chat", "ChatBox_ChatBoxId", "dbo.ChatBox");
            DropForeignKey("dbo.Chat", "Account_AccountId", "dbo.Account");
            DropForeignKey("dbo.ChatBox", "ChatBoxId", "dbo.Account");
            DropIndex("dbo.Chat", new[] { "ChatBox_ChatBoxId" });
            DropIndex("dbo.Chat", new[] { "Account_AccountId" });
            DropIndex("dbo.ChatBox", new[] { "ChatBoxId" });
            DropTable("dbo.Chat");
            DropTable("dbo.ChatBox");
            DropTable("dbo.Account");
        }
    }
}
