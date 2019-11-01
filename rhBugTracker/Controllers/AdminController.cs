using rhBugTracker.Helpers;
using rhBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhBugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectHelper projHelper = new ProjectHelper();
       
        //--------------------------
        // GET: Admin
        [Authorize]
        public ActionResult ManageRoles()
        {
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id", "Email");
            ViewBag.DisplayName = new MultiSelectList(db.Users, "Id", "Username");
            ViewBag.Email = new MultiSelectList(db.Users, "Id", "Email");
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");

            var users = new List<ManageRolesViewModel>();
            foreach (var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    FullName = $"{user.LName}, {user.FName}",
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault()
                });
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //POST : Admin
        public ActionResult ManageRoles(List<string> userIds, string role)
        {

            //step 1 : Unenroll all the selected users from any roles
            //the may currently occupy
            foreach (var userId in userIds)
            {
                //what is the role of this person
                var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                if (userRole != null)
                {
                    roleHelper.RemoveUserFromRole(userId, userRole);
                }
            }

            //Step 2 : Add them back to the selected roles 
            if (!string.IsNullOrEmpty(role))
            {
                foreach (var userId in userIds)
                {
                    roleHelper.AddUserToRole(userId, role);
                }
            }

            //Redirect to dashboard
            return RedirectToAction("ManageRoles", "Admin");
        }
        
        //=----------------------------------------
        //GET : 
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult ManageProjectsUsers()
        {
            //displaying all projects on the system in select list
            ViewBag.Projects = new MultiSelectList(db.Projects, "Id", "Name");
            ViewBag.Developers = new MultiSelectList(roleHelper.UsersInRole("Developer"), "Id", "Email");
            ViewBag.Submitters = new MultiSelectList(roleHelper.UsersInRole("Submitter"), "Id", "Email");

            if (User.IsInRole("Admin"))
            {
                ViewBag.ProjectManagerId = new SelectList(roleHelper.UsersInRole("Project_Manager"), "Id", "Email");

            }

            //Create a view model purposes of displaying Users and their associated projects
            var myData = new List<UserProjectListViewModel>();
            UserProjectListViewModel userVm = null;
            foreach (var user in db.Users.ToList())
            {
                userVm = new UserProjectListViewModel
                {
                    Name = $"{user.LName}, {user.FName}",
                    ProjectNames = projHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList()
                };
            }

            if (userVm.ProjectNames.Count() == 0)
                userVm.ProjectNames.Add("N/A");

            myData.Add(userVm);


            return View(myData);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(List<int> projects, string projectManagerId, List<string> developers, List<string> submitters)
        {
            //Remove users from every project I have selected
            if (projects != null)
            {
                foreach(var projectId in projects)
                {
                    //Remove everone from this project
                    foreach(var user in projHelper.UsersOnProject(projectId).ToList())
                    {
                        projHelper.RemoveUserFromProject(user.Id, projectId);
                    }
                    //add back project manager if possible
                    if (!string.IsNullOrEmpty(projectManagerId))
                    {
                        projHelper.AddUserToProject(projectManagerId, projectId);
                    }

                    if(developers!= null)
                    {
                        foreach(var developerId in developers)
                        {
                            projHelper.AddUserToProject(developerId, projectId);
                        }
                    }

                    if(submitters != null)
                    {
                        foreach(var submittersId in submitters)
                        {
                            projHelper.AddUserToProject(submittersId, projectId);
                        }
                    }
                }
            }
            return RedirectToAction("ManageProjectUsers");
        }
        
        //------------------------------------------------
        [Authorize]
        public ActionResult index()
        {
            return View();
        }
            
    }
}
