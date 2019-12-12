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
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private TicketHelper tickHelper = new TicketHelper();

        // GET: ProjectManager dashboard
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //var myProjects = projHelper.ListUserProjects(userId);            
            var data = new Dashboard();
            data.myProjects = db.Projects.ToList();
            data.createdProjects = db.Projects.Where(p => p.ProjectOwnerId == userId).ToList();
            data.myTickets = data.createdProjects.SelectMany(p => p.Tickets).ToList();

            return View(data);
        }

        public ActionResult Users()
        {
            var users = new List<ManageUsersViewModel>();
            foreach (var user in db.Users.ToList())
            {
                users.Add(new ManageUsersViewModel
                {
                    FullName = $"{user.LName}, {user.FName}",
                    Email = user.Email,
                    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault(),
                    ImagePath = user.AvatarPath
                });
            }

            return View(users);
        }


        
    }
}