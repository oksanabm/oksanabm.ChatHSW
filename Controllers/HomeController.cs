using System.Web.Mvc;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(User user)
        {
            var basicCookie = new BasicCookie(user);

            return View(basicCookie);
        }
        public ActionResult Chat()
        {
            return View();
        }
    }
}