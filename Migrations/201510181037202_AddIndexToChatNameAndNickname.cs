namespace SignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToChatNameAndNickname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Nickname", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Users", new[] { "Nickname" }, unique: true, name: "NicknameIndex");
            AlterColumn("dbo.Chats", "Name", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Chats", new[] { "Name" }, unique: true, name: "NameIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "NicknameIndex");
            AlterColumn("dbo.Users", "Nickname", c => c.String(nullable: false));
            DropIndex("dbo.Chats", "NameIndex");
            AlterColumn("dbo.Chats", "Name", c => c.String(nullable: false));
        }
    }
}
