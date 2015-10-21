namespace SignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Context = c.String(),
                        Author_Id = c.Int(),
                        Chat_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Chats", t => t.Chat_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Chat_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nickname = c.String(),
                        Age = c.Int(nullable: false),
                        LastVisit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Chat_Id", "dbo.Chats");
            DropForeignKey("dbo.Messages", "Author_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "Chat_Id" });
            DropIndex("dbo.Messages", new[] { "Author_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
        }
    }
}
