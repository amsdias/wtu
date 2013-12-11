using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.IO;


namespace WebApplication1.Controllers
{
    public class userController : Controller
    {
        private mydbEntities db = new mydbEntities();

        // GET: /user/
        public ActionResult Index()
        {

            var UserListDB = db.users.ToList();
            IList<User_List_ViewModel> viewmodel = new List<User_List_ViewModel>();

            foreach (var User in UserListDB) {
                viewmodel.Add(new User_List_ViewModel {
                    Id = User.Id,
                    Nome = User.Name + " " + User.Surname,
                    Pais = User.Country,
                    Univ = User.HomeU,
                    Curso = User.Course

                });
            }

            
            return View(viewmodel);
        }

        // GET: /user/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            User_Details_ViewModel viewmodel = new User_Details_ViewModel
            {
                Id = user.Id,
                Nome = user.Name + " " + user.Surname,
                Pais = user.Country,
                Univ = user.HomeU,
                Curso = user.Course,
                Foto = user.Avatar
            };

            return View(viewmodel);
        }

        // GET: /user/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /user/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,studentId,Password,Name,Surname,Country,HomeU,Dob,Course,Avatar")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // GET: /user/Edit/5
        [Authorize]
        public ActionResult Edit(int? id, int? sID)
        {
            if (sID != null)
            {
                user IDgajo = db.users.FirstOrDefault(i => i.studentId == sID);
                id = IDgajo.Id;
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /user/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,studentId,Password,Name,Surname,Country,HomeU,Dob,Course,Avatar")] user user)
        {

            // Código de upload das imagens
            foreach (string uploadFile in Request.Files)
            {
                if ((!(Request.Files[uploadFile] != null && Request.Files[uploadFile].ContentLength > 0)) || (Request.Files[uploadFile].ContentLength > 1048576)) { continue; };
                string path = AppDomain.CurrentDomain.BaseDirectory + "Images/Users/";
                // apanhar só extensão do ficheiro
                var fileext = Request.Files[uploadFile].FileName.Substring(Request.Files[uploadFile].FileName.LastIndexOf(".") + 1);
                string filename = "Avatar_" + user.Id + "." + fileext;
                Request.Files[uploadFile].SaveAs(Path.Combine(path, filename));

                user.Avatar = "~/Images/Users/" + filename;
            }




            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /user/Login/5
        /*[HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

         * 
         * 
         * 
        [HttpPost]
        public ActionResult Login(user user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.studentId, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.studentId.ToString(), user.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }*/




        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(user user)
        {
            if (ModelState.IsValid)
            {
                user Uti = db.users.FirstOrDefault(model => model.studentId == user.studentId);

                if (Uti.Password == user.Password)
                {
                    FormsAuthentication.SetAuthCookie(user.studentId.ToString(), true);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /user/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // POST: /user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
