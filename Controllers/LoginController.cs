using System;
using System.Linq;
using System.Web.Mvc;
using SignalRChat.DAL;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    public class LoginController : Controller
    {
        private SiteContext db = new SiteContext();

         [AllowAnonymous]
        public ActionResult Index([Bind(Include = "Nickname, Password")] User user)
        {
            if (user.Nickname != null)
            {
                
                var currentUser = db.Users.FirstOrDefault(x => x.Nickname == user.Nickname && x.Password == user.Password);
                if (currentUser != null)
                {
                    return RedirectToAction("Index", "Home", currentUser);
                }
                return Content("Wrong nickname");
            }
            return View();
        }

         [AllowAnonymous]
         public ActionResult Register([Bind(Include = "Nickname,Age, Password")] User user)
        {
            if (user.Nickname != null)
            {
                user.LastVisit = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}