using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SignalRChat.Models;
using SignalRChat.DAL;

namespace SignalRChat.Controllers
{
    public class UserController : Controller
    {
        private SiteContext db = new SiteContext();


        public ActionResult Index(int? userId)
        {
            var currentUserFriends = db.Users.Where(x => x.Id == userId)
                                     .Select(t => t.Friends).First().Distinct().ToList();
            var cookie = new BasicCookie(db.Users.FirstOrDefault(x => x.Id == userId));
            cookie.SetUserList(currentUserFriends);
            return View(cookie);
        }

        public ActionResult AllUsers(int? userId)
        {
            var currentUserFriends = db.Users.FirstOrDefault(x => x.Id == userId).Friends;
            var cookie = new BasicCookie(db.Users.FirstOrDefault(x => x.Id == userId));
            cookie.SetUserList(db.Users.Distinct().ToList().Where(x => !currentUserFriends.Contains(x) && x.Id != userId).ToList());
            return View(cookie);
        }

        public ActionResult AddToFriends(int? id, int? userId)
        {
            var currentUser = db.Users.FirstOrDefault(x => x.Id == userId);
            var newFriend = db.Users.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                db.Entry(newFriend).Entity.Friends.Add(currentUser);
                db.Entry(currentUser).Entity.Friends.Add(newFriend);
                db.Entry(newFriend).State = EntityState.Modified;
                db.Entry(currentUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", new {userId});
        }

        public ActionResult DeleteFriend(int? id, int? userId)
        {
            var currentUser = db.Users.FirstOrDefault(x => x.Id == userId);
            var deletingFriend = db.Users.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                db.Entry(deletingFriend).Entity.Friends.Remove(currentUser);
                db.Entry(currentUser).Entity.Friends.Remove(deletingFriend);
                db.Entry(deletingFriend).State = EntityState.Modified;
                db.Entry(currentUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { userId });
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nickname,Age,LastVisit,Password, Sex")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }


        public ActionResult RedirectToCreateChat(int? id, int? userId)
        {
            return RedirectToAction("CreateFromUserList", "Chat", new { id, userId });
        }

        // GET: /User/Edit/5
        public ActionResult Edit(int? userId)
        {
            if (userId == null)
            {
                return HttpNotFound();
            }
            User user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            var cookie = new BasicCookie(user);
            return View(cookie);
        }

        // POST: /User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nickname,Age, Password, Sex")] User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.Find(user.Id);
                if (existingUser.Nickname != user.Nickname) existingUser.Nickname = user.Nickname;
                if (existingUser.Age != user.Age) existingUser.Age = user.Age;
                if (existingUser.Sex != user.Sex) existingUser.Sex = user.Sex;
                if (existingUser.Password != user.Password) existingUser.Password = user.Password;
                db.Users.AddOrUpdate(existingUser);
                db.Entry(existingUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            var cookie = new BasicCookie(user);
            return View(cookie);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
