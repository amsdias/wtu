using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers
{
    public class storyController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: /story/
        public ActionResult Index()
        {
            var stories = db.stories.Include(s => s.user);
            return View(stories.ToList());
        }

        // GET: /story/Details/5
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

        // GET: /story/Create
        public ActionResult Create()
        {
                int sID = int.Parse(User.Identity.Name);
                user IDgajo = db.users.FirstOrDefault(i => i.studentId == sID);
                int id = IDgajo.Id;

                story viewmodel = new story
                {
                    user_Id = id
                };
            
            return View(viewmodel);
        }

        // POST: /story/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idstory,user_Id,description,text,image1")] story story)
        {
            if (ModelState.IsValid)
            {
                db.stories.Add(story);
                db.SaveChanges();
                // Código de upload das imagens
                foreach (string uploadFile in Request.Files)
                {
                    if ((!(Request.Files[uploadFile] != null && Request.Files[uploadFile].ContentLength > 0)) || (Request.Files[uploadFile].ContentLength > 1048576)) { continue; };
                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images/Story/";
                    // apanhar só extensão do ficheiro
                    var fileext = Request.Files[uploadFile].FileName.Substring(Request.Files[uploadFile].FileName.LastIndexOf(".") + 1);
                    string filename = "Story_" + story.idstory + "." + fileext;
                    Request.Files[uploadFile].SaveAs(Path.Combine(path, filename));

                    story.image1 = "~/Images/Story/" + filename;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.user_Id = new SelectList(db.users, "Id", "Password", story.user_Id);
            return View(story);
        }

        // GET: /story/Edit/5
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

        // POST: /story/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idstory,user_Id,description,text,image1")] story story)
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

        // GET: /story/Delete/5
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

        // POST: /story/Delete/5
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
