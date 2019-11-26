using rhBugTracker.Helpers;
using rhBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace rhBugTracker.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectHelper projHelper = new ProjectHelper();
        private TicketHelper ticketHelper = new TicketHelper();

        //--------------MANAGE ROLES----------------------
        // GET: Admin
        [Authorize(Roles = "Admin")]
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
                    RoleName = roleHelper.ListUserRoles(user.Id).FirstOrDefault(),

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

        //--------------MANAGE USERS----------------------
        //GET: ADMIN
        [Authorize(Roles = ("Admin, Project Manager"))]
        public ActionResult ManageUsers()
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

        //=-------------MANAGE PROJECT USERS--------------
        //GET : 
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult ManageProjectsUsers()
        {
            //displaying all projects on the system in select list
            ViewBag.Projects = new MultiSelectList(db.Projects, "Id", "Name");
            ViewBag.Developers = new MultiSelectList(roleHelper.UsersInRole("Developer"), "Id", "Email");
            ViewBag.Submitters = new MultiSelectList(roleHelper.UsersInRole("Submitter"), "Id", "Email");

            //Allow assign to user to project only admin
            if (User.IsInRole("Admin"))
            {
                ViewBag.ProjectManagerId = new SelectList(roleHelper.UsersInRole("Project Manager"), "Id", "Email");

            }

            //Create a view model purposes of displaying Users and their associated projects
            var myData = new List<UserProjectListViewModel>(); //new list of type model 

            UserProjectListViewModel userVm = null; //temp variable 

            //creating instance of model assigning properties and adding it to list called myData
            foreach (var user in db.Users.ToList())
            {
                userVm = new UserProjectListViewModel //new instance with properties
                {
                    Name = $"{user.LName}, {user.FName}",
                    ProjectNames = projHelper.ListUserProjects(user.Id).Select(p => p.Name).ToList()
                };
                //if the person is not on any project then display not available 
                if (userVm.ProjectNames.Count() == 0)
                    userVm.ProjectNames.Add("N/A");

                //add to the list
                myData.Add(userVm);
            }
            //passing into the view            
            return View(myData);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectsUsers(List<int> projects, string projectManagerId, List<string> developers, List<string> submitters)
        {
            //Remove users from every project I have selected
            if (projects != null)
            {
                foreach (var projectId in projects)
                {
                    //Remove everone from this project
                    foreach (var user in projHelper.UsersOnProject(projectId).ToList())
                    {
                        projHelper.RemoveUserFromProject(user.Id, projectId);
                    }

                    //add back project manager to list if chose manager
                    if (!string.IsNullOrEmpty(projectManagerId))
                    {
                        projHelper.AddUserToProject(projectManagerId, projectId);
                    }

                    if (developers != null)
                    {
                        foreach (var developerId in developers)
                        {
                            projHelper.AddUserToProject(developerId, projectId);
                        }
                    }

                    if (submitters != null)
                    {
                        foreach (var submittersId in submitters)
                        {
                            projHelper.AddUserToProject(submittersId, projectId);
                        }
                    }
                }
            }
            return RedirectToAction("ManageProjectsUsers");
        }

        //----------------MANAGE TICKET USERS-----------------
        //GET:
        [Authorize(Roles = "Admin")]
        public ActionResult ManageTicketsUsers()
        {
            //Display all tickets in selectList
            ViewBag.Tickets = new SelectList(db.Tickets.ToList(), "Id", "Title");
            //Display developer to choose from 
            if (User.IsInRole("Admin"))
            {
                ViewBag.DeveloperId = new SelectList(roleHelper.UsersInRole("Developer"), "Id", "Email");
            }
            return View();
        }

        //POST:
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageTicketsUsers(int tickets, string DeveloperId)
        {
            //assigning variables
            var ticket = db.Tickets.Find(tickets);
            //helper to assign to ticket or unassign
            ticket.AssignedToUserId = DeveloperId;

            if (ticket.AssignedToUserId != null)
            {
                ticket.TicketStatusId = db.TicketStatus.FirstOrDefault(t => t.Name == "Assigned").Id;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Tickets");
        }
        
        [Authorize(Roles = "Admin, Project Manager")]
        public ActionResult MyCreatedProjects()
        {
            var myData = new Dashboard();
            var userId = User.Identity.GetUserId();
            myData.createdProjects = db.Projects.Where(p => p.ProjectOwnerId == userId).ToList();                
            return View(myData);
        }




        //    return RedirectToAction("ManageTicketsUsers", "Admin");
        //}
        
        [Authorize]
        public ActionResult index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var data = new Dashboard();
            data.myProjects = db.Projects.ToList();
            data.createdProjects = db.Projects.Where(p => p.ProjectOwnerId == user.Id).ToList();

            return View(data);
        }
            
    }
}
