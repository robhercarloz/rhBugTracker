using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using rhBugTracker.Helpers;
using rhBugTracker.Models;

namespace rhBugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper proHelper = new ProjectHelper();
        private TicketHelper ticketHelper = new TicketHelper();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);       
            
            
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);

            

            
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] Project project)
        {
            if (ModelState.IsValid)
            {
                var proj = project.Name;
                if (string.IsNullOrEmpty(proj))
                {
                    ModelState.AddModelError("Name", "Invalid Title");
                    return View(project);
                }

                project.ProjectOwnerId = User.Identity.GetUserId();
                project.Created = DateTime.Now;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
               
        //----------------MY PROJECTS-------------------//
        //GET:
        public ActionResult myProjects()
        {
            var user = User.Identity.GetUserId();
            var mydata = proHelper.ListUserProjects(user);
             
            var projects = new List<ProjectViewModel>();
            foreach (var pro in mydata.ToList())
            {
                projects.Add(new ProjectViewModel
                {
                    projName = pro.Name,
                    projDescription = pro.Description,
                    Users = proHelper.UsersOnProject(pro.Id)
                });
            }

            return View(projects);
        }
        
        //POST:
        


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
