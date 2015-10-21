namespace SignalRChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSexToTheUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Sex");
        }
    }
}
