using System.Data.Entity;
using SignalRChat.Models;
  
namespace SignalRChat.DAL
{

    public class SiteContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Chat> Chats { get; set; }
    }
}