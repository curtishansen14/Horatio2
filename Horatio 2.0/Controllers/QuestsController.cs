using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Horatio_2._0.Models;
using Microsoft.AspNet.Identity;

namespace Horatio_2._0.Controllers
{
    public class QuestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quests
        public ActionResult Index(string searchString)
        {
            var quests = from q in db.Quests.Include("Topic")
                         select q;

            if (!string.IsNullOrEmpty(searchString))
            {
                quests = quests.Where(y => y.Title.Contains(searchString) || y.Topic.Theme.Contains(searchString));
            }

            return View(quests);
        }

        public ActionResult AddToProfile([Bind(Include = "QuestID, Title, Description,TopicID,Labors")] int? id)
        {
            string userId = User.Identity.GetUserId();
            UserQuest userquest = new UserQuest();
            userquest.QuestID = (int)id;
            userquest.Id = userId;
            userquest.isActive = true;
            userquest.isComplete = false;
            userquest.Target = null;

            db.UserQuests.Add(userquest);
            db.SaveChanges();

            List<Labor> LaborsToAdd = (from x in db.Labors where x.QuestID == id select x).ToList();

            foreach(Labor labor in LaborsToAdd)
            {
                UserLabor userlabor = new UserLabor();
                userlabor.LaborID = labor.LaborID;
                userlabor.Id = userId;
                userlabor.UserQuestID = userquest.UserQuestID;
                userlabor.isComplete = false;
                userlabor.Target = null;

                db.UserLabors.Add(userlabor);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "UserQuests", new { Id = userId });
        }

        // GET: Quests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quests.Include(x => x.Labors).FirstOrDefault(q => q.QuestID == id);
            if (quest == null)
            {
                return HttpNotFound();
            }

            var locations = (from q in db.Labors.Include("Quest")
                             where q.Location != null && id == q.QuestID
                             select q).ToList();
            return View(quest);
        }

        // GET: Quests/Create
        public ActionResult Create()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Theme");
            return View();
        }

        // POST: Quests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestID,Title,Description,TopicID,Labors")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                db.Quests.Add(quest);
                db.SaveChanges();
                return RedirectToAction("Create", "Labors", new { id = quest.QuestID });
            }

            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Theme", quest.TopicID);
            return View(quest);
        }

        // GET: Quests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Theme", quest.TopicID);
            return View(quest);
        }

        // POST: Quests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestID,Title,Descritpion,TopicID")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "Theme", quest.TopicID);
            return View(quest);
        }

        // GET: Quests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quest quest = db.Quests.Find(id);
            if (quest == null)
            {
                return HttpNotFound();
            }
            return View(quest);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quest quest = db.Quests.Find(id);
            db.Quests.Remove(quest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
