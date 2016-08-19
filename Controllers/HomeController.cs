using System.Web.Mvc;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(User user)
        {
            return RedirectToAction("Index", "Chat", new { userId = user.Id });
        }
        public ActionResult Chat()
        {
            return View();
        }
    }
}