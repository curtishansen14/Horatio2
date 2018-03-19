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
    public class LaborsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Labors
        public ActionResult Index(int? id)
        {
            if(id != null)
            {
                return View(db.Labors.Include(x => x.Quest).Include(y => y.QuestID == id).ToList());
            }
            var labors = db.Labors.Include(l => l.Quest);
            return View(labors.ToList());
        }

        // GET: Labors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // GET: Labors/Create
        public ActionResult Create(int? id)
        {
            ViewBag.QuestID = new SelectList(db.Quests, "QuestID", "Title");
            return View();
        }

        // POST: Labors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LaborID,QuestID,Title,Description,Location")] Labor labor, int? id)
        {
            if (ModelState.IsValid)
            {
                labor.QuestID = (int)id;
                db.Labors.Add(labor);
                db.SaveChanges();

                TempData["Message"] = "Labor Added";

                return RedirectToAction("Create", "Labors", new { id = labor.QuestID });
            }

            ViewBag.QuestID = new SelectList(db.Quests, "QuestID", "Title", labor.QuestID);
            return View(labor);
        }

        // GET: Labors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestID = new SelectList(db.Quests, "QuestID", "Title", labor.QuestID);
            return View(labor);
        }

        // POST: Labors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LaborID,QuestID,Title,Description,Location")] Labor labor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestID = new SelectList(db.Quests, "QuestID", "Title", labor.QuestID);
            return View(labor);
        }

        // GET: Labors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labor labor = db.Labors.Find(id);
            if (labor == null)
            {
                return HttpNotFound();
            }
            return View(labor);
        }

        // POST: Labors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Labor labor = db.Labors.Find(id);
            db.Labors.Remove(labor);
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
