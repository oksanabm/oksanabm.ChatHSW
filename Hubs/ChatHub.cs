using System.Linq;
using Microsoft.AspNet.SignalR;
using SignalRChat.DAL;
using SignalRChat.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private SiteContext db = new SiteContext();
        public void Send(string nickname, string message)
        {
            var dbMessage = new Message
            {
                Author = db.Users.FirstOrDefault(x => x.Nickname == nickname),
                Chat = db.Chats.FirstOrDefault(),
                Context = message
            };
            if ((dbMessage.Author != null) && (dbMessage.Chat != null))
            {
                db.Messages.Add(dbMessage);
                db.SaveChanges();
            
                // Call the addNewMessageToPage method to update clients.
                Clients.All.addNewMessageToPage(nickname, message);
            }
        }

        public void Init(int chatId, bool firstTime)
        {
            if (firstTime)
            {
                var messagesInChat = db.Messages.Where(x => x.Chat.Id == chatId).ToList();
                messagesInChat.ForEach(x => Clients.Caller.addNewMessageToPage(x.Author.Nickname, x.Context));
            }
        }
    }
}