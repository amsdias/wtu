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

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        // GET: /user/
        public ActionResult Index()
        {

            var UserListDB = db.users.ToList();
            IList<User_List_ViewModel> viewmodel = new List<User_List_ViewModel>();

            foreach (var User in UserListDB) {
                viewmodel.Add(new User_List_ViewModel {
                    Id = User.Id,
                    Name = User.Name + " " + User.Surname,
                    Country = User.Country,
                    University = User.HomeU,
                    Course = User.Course

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
                Name = user.Name + " " + user.Surname,
                Country = user.Country,
                University = user.HomeU,
                Course = user.Course,
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
        //[Bind(Include = "Id,studentId,Password,Name,Surname,Country,HomeU,Dob,Course,Avatar")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(user user)
        {

            if (ModelState.IsValid)
            {
                user Uti = db.users.FirstOrDefault(model => model.studentId == user.studentId);
                
                if (Uti == null && user.studentId > 0)
                {
                    user.Password = Helpers.SHA1.Encode(user.Password);
                    db.users.Add(user);
                    db.SaveChanges();
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
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "That Student ID is invalid or already registered!");
                }


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
        // [Bind(Include = "Id,studentId,Password,Name,Surname,Country,HomeU,Dob,Course,Avatar")]
        public ActionResult Edit(user user)
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
                if (user.Password == "")
                {
                    user Uti = db.users.FirstOrDefault(model => model.Id == user.Id);
                    user.Password = Uti.Password; }
                else
                { user.Password = Helpers.SHA1.Encode(user.Password); }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

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

                if (Uti.Password == Helpers.SHA1.Encode(user.Password))
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
