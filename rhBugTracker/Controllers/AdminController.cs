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

        // GET: Admin
        public ActionResult ManageRoles()
        {
            ViewBag.UserIds = new MultiSelectList(db.Users, "Id","Email");
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");

            var users = new List<ManageRolesViewModel>();
            foreach(var user in db.Users.ToList())
            {
                users.Add(new ManageRolesViewModel
                {
                    UserName = $"{user.LName}, {user.FName}",
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
            foreach(var userId in userIds)
            {
                //what is the role of this person
                var userRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
                if(userRole != null)
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



    }
}