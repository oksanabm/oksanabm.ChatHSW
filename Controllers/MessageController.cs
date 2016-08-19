using System.Linq;
using System.Net;
using System.Web.Mvc;
using SignalRChat.Models;
using SignalRChat.DAL;

namespace SignalRChat.Controllers
{
    public class MessageController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: /Message/
        public ActionResult Index(int? chatId, int? userId)
        {
            if (userId != (int)HttpContext.Session["Id"])
                return RedirectToAction("Index", "Login");

            if (chatId != null)
            {
                var messagesInChat = db.Messages.Where(x => x.Chat.Id == chatId).ToList();
                if (messagesInChat.FirstOrDefault() == null)
                {
                    messagesInChat.Add(new Message() { Chat = db.Chats.FirstOrDefault(x => x.Id == chatId) });
                }
                var cookie = new BasicCookie(db.Users.FirstOrDefault(x => x.Id == userId));
                cookie.SetMessageList(messagesInChat);
                return View(cookie);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

    }
}
