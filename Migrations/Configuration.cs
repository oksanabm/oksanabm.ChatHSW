using SignalRChat.Models;

namespace SignalRChat.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SignalRChat.DAL.SiteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SignalRChat.DAL.SiteContext";
        }

        protected override void Seed(SignalRChat.DAL.SiteContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            
            //Remove after seeding
            /*var user1 = new User {Nickname = "Andrew Peters", Age = 21, LastVisit = DateTime.Today};
            var user2 = new User {Nickname = "Brice Lambson", Age = 21, LastVisit = DateTime.Today};
            var user3 = new User {Nickname = "Rowan Miller", Age = 21, LastVisit = DateTime.Today};
            context.Users.AddOrUpdate(user1, user2, user3);
            var chat1 = new Chat {Name = "Cool people"};
            var chat2 = new Chat { Name = "Bad people" };
            context.Chats.AddOrUpdate(chat1, chat2);
            var message1 = new Message { Author = user1, Chat = chat1, Context = "Hey There!"};
            var message2 = new Message { Author = user3, Chat = chat1, Context = "Lovely chat!" };
            var message3 = new Message { Author = user2, Chat = chat2, Context = "Are you here?" };
            var message4 = new Message { Author = user1, Chat = chat2, Context = "Yes!!!!" };
            context.Messages.AddOrUpdate(message1, message2, message3, message4);*/
        }
    }
}
