using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SignalRChat.DAL;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class ChatController : Controller
    {
        private readonly SiteContext db = new SiteContext();

        public ActionResult Index(int? userId)
        {
            var currentUserChats =
                db.Chats.ToList().Where(x => x.Users.Contains(db.Users.FirstOrDefault(y => y.Id == userId))).ToList();
            var cookie = new BasicCookie(db.Users.FirstOrDefault(x => x.Id == userId));
            cookie.SetChatList(currentUserChats);
            return View(cookie);
        }

        public ActionResult CreateFromUserList(int? id, int? userId)
        {
            var firstUser = db.Users.FirstOrDefault(x => x.Id == id);
            var chatCreater = db.Users.FirstOrDefault(x => x.Id == userId);
            if (firstUser != null && chatCreater != null)
            {
                var existingChat =
                    db.Chats.ToList().FirstOrDefault(x => x.Users.Contains(firstUser) && x.Users.Contains(chatCreater));
                if (existingChat == null)
                {
                    var newChat = db.Chats.Add(new Chat
                    {
                        Name = firstUser.Nickname + chatCreater.Nickname,
                        Users = new List<User> {firstUser, chatCreater}
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index", "Message", new { chatId = newChat.Id, userId });
                }
                return RedirectToAction("Index", "Message", new { chatId = existingChat.Id, userId });
            }
            return RedirectToAction("Index", new { userId });
        }

        
        public ActionResult Message(int? chatId, int? userId)
        {
            if (chatId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index", "Message", new { chatId, userId });
        }

        public ActionResult Delete(int? chatId, int? userId)
        {
            if (chatId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var chat = db.Chats.Find(chatId);
            if (chat == null)
            {
                return HttpNotFound();
            }
            db.Chats.Remove(chat);
            db.SaveChanges();
            return RedirectToAction("Index", new {userId});
        }
    }
}