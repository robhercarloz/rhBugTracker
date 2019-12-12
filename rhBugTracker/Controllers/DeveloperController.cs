using Microsoft.AspNet.Identity;
using rhBugTracker.Helpers;
using rhBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhBugTracker.Controllers
{
    public class DeveloperController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectHelper projHelper = new ProjectHelper();


        // GET: Developer
        public ActionResult Index()
        {
            var data = new Dashboard();
            var userId = User.Identity.GetUserId();
            data.myProjects = projHelper.ListProjectsUserIsOn(userId);
            data.myTickets = db.Tickets.Where(t => t.AssignedToUserId == userId).ToList();

            return View(data);
        }
    }
}