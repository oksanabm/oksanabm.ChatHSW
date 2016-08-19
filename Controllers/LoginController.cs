using System;
using System.Linq;
using System.Web.Mvc;
using SignalRChat.DAL;
using SignalRChat.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SignalRChat.Models;
using SignalRChat.DAL;


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
                    HttpContext.Session["Id"] = currentUser.Id;
                    currentUser.LastVisit = DateTime.Now;
                    db.Users.AddOrUpdate(currentUser);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home", currentUser);
                }
                var cookie = new BasicCookie(currentUser);
                cookie.SetErrorList(new List<string>() { "Неправильный логин или пароль" });
                return View(cookie);
            }
            return View(new BasicCookie(null));
        }

         [AllowAnonymous]
        public ActionResult Register([Bind(Include = "Nickname,Age, Password")] User user)
        {
            var cookie = new BasicCookie(null);
            if (user.Nickname != null)
            {
                var errorList = new List<string>();
                var userWithSameNick = db.Users.FirstOrDefault(x => x.Nickname == user.Nickname);
                if (userWithSameNick != null)
                    errorList.Add("Пользователь с таким именем уже существует");
                if (user.Age < 7)
                    errorList.Add("Ты слишком мал, для того, чтобы регистрироваться");
                if (user.Age > 100)
                    errorList.Add("Ты слишком стар, для того, чтобы регистрироваться");
                if (user.Age > 100)
                    errorList.Add("Ты слишком стар, для того, чтобы регистрироваться");
                if (user.Password == null)
                    errorList.Add("Введи пароль");
                cookie.SetErrorList(errorList);
                if (errorList.Count == 0)
                {
                    user.LastVisit = DateTime.Now;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else return View(cookie);
            }
            return View(cookie);
        }
        public ActionResult Logout(int? userId)
        {
            HttpContext.Session["Id"] = null;
            var currentUser = db.Users.FirstOrDefault(x => x.Id == userId);
            if (currentUser != null)
            {
                currentUser.LastVisit = DateTime.Now;
                db.Users.AddOrUpdate(currentUser);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}