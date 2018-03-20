using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Horatio_2._0.Models;

namespace Horatio_2._0.Controllers
{
    public class UserLaborsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserLabors
        public ActionResult Index()
        {
            var userLabors = db.UserLabors.Include(u => u.AspNetUser).Include(u => u.Labor).Include(u => u.UserQuest);
            return View(userLabors.ToList());
        }

        // GET: UserLabors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLabor userLabor = db.UserLabors.Include(x => x.Labor).Include(z => z.UserQuest).FirstOrDefault();
            if (userLabor == null)
            {
                return HttpNotFound();
            }
            return View(userLabor);
        }

        // GET: UserLabors/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Email");
            ViewBag.LaborID = new SelectList(db.Labors, "LaborID", "Title");
            ViewBag.UserQuestID = new SelectList(db.UserQuests, "UserQuestID", "Id");
            return View();
        }

        // POST: UserLabors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserLaborID,LaborID,Id,UserQuestID,Target,isComplete")] UserLabor userLabor)
        {
            if (ModelState.IsValid)
            {
                db.UserLabors.Add(userLabor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Email", userLabor.Id);
            ViewBag.LaborID = new SelectList(db.Labors, "LaborID", "Title", userLabor.LaborID);
            ViewBag.UserQuestID = new SelectList(db.UserQuests, "UserQuestID", "Id", userLabor.UserQuestID);
            return View(userLabor);
        }

        // GET: UserLabors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLabor userLabor = db.UserLabors.Find(id);
            if (userLabor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", userLabor.Id);
            ViewBag.LaborID = new SelectList(db.Labors, "LaborID", "Title", userLabor.LaborID);
            ViewBag.UserQuestID = new SelectList(db.UserQuests, "UserQuestID", "Id", userLabor.UserQuestID);
            return View(userLabor);
        }

        // POST: UserLabors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserLaborID,LaborID,Id,UserQuestID,Target,isComplete")] UserLabor userLabor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userLabor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", userLabor.Id);
            ViewBag.LaborID = new SelectList(db.Labors, "LaborID", "Title", userLabor.LaborID);
            ViewBag.UserQuestID = new SelectList(db.UserQuests, "UserQuestID", "Id", userLabor.UserQuestID);
            return View(userLabor);
        }

        public ActionResult Complete(int? id)
        {
            UserLabor userlabor = db.UserLabors.Find(id);
            userlabor.isComplete = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: UserLabors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLabor userLabor = db.UserLabors.Find(id);
            if (userLabor == null)
            {
                return HttpNotFound();
            }
            return View(userLabor);
        }

        // POST: UserLabors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserLabor userLabor = db.UserLabors.Find(id);
            db.UserLabors.Remove(userLabor);
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
