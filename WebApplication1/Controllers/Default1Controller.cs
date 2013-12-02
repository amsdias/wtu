using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Default1Controller : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: /Default1/
        public ActionResult Index()
        {
            var stories = db.stories.Include(s => s.user);
            return View(stories.ToList());
        }

        // GET: /Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            story story = db.stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // GET: /Default1/Create
        public ActionResult Create()
        {
            ViewBag.user_Id = new SelectList(db.users, "Id", "Password");
            return View();
        }

        // POST: /Default1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idstory,user_Id,description,text,image1,image2,image3,rating")] story story)
        {
            if (ModelState.IsValid)
            {
                db.stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_Id = new SelectList(db.users, "Id", "Password", story.user_Id);
            return View(story);
        }

        // GET: /Default1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            story story = db.stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_Id = new SelectList(db.users, "Id", "Password", story.user_Id);
            return View(story);
        }

        // POST: /Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idstory,user_Id,description,text,image1,image2,image3,rating")] story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_Id = new SelectList(db.users, "Id", "Password", story.user_Id);
            return View(story);
        }

        // GET: /Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            story story = db.stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: /Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            story story = db.stories.Find(id);
            db.stories.Remove(story);
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
