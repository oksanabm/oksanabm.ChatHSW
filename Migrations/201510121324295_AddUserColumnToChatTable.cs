namespace SignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserColumnToChatTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Chat_Id", c => c.Int());
            CreateIndex("dbo.Users", "Chat_Id");
            AddForeignKey("dbo.Users", "Chat_Id", "dbo.Chats", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Chat_Id", "dbo.Chats");
            DropIndex("dbo.Users", new[] { "Chat_Id" });
            DropColumn("dbo.Users", "Chat_Id");
        }
    }
}
