using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using rhBugTracker.Helpers;
using rhBugTracker.Models;

namespace rhBugTracker.Controllers
{
    
    public class ProjectManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper projHelper = new ProjectHelper();

        // GET: ProjectManager dashboard
        public ActionResult Index()
        {
            //var userId = User.Identity.GetUserId();
            //var myProjects = projHelper.ListUserProjects(userId);            
            var data = new Dashboard();
            data.myProjects = db.Projects.ToList();

            return View(data);
        }
    }
}