using rhBugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace rhBugTracker.Helpers
{
    public class ProjectHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();
        UserRolesHelper rolesHelper = new UserRolesHelper();
        
        public ICollection<Project> ListProjectsUserIsOn(string userId)
        {
            var projects = db.Projects.ToList();            

            var projList = new List<Project>();
            foreach(var proj in projects)
            {
                if (IsUserOnProject(userId, proj.Id))
                {
                    projList.Add(proj);
                }                               
            }
            return projList;
        }

        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }

        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            var projects = user.Projects.ToList();           
            
            return (projects);
        }

        public void AddUserToProject(string userId, int projectId)
        {
            if(!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);

                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        public void RemoveUserFromProject(string userId, int projectId)
        {
            if(IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);

                proj.Users.Remove(delUser);
                db.Entry(proj).State = EntityState.Modified; //Saves instance
                db.SaveChanges();

            }
        }

        public ICollection<ApplicationUser>UsersOnProject(int projectId)
        {
            return db.Projects.Find(projectId).Users;
        }
        public ICollection<ApplicationUser>UsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
        }
        public ICollection<Project> ListAllProjects()
        {
            var projects = db.Projects.ToList();
            return projects;
        }

        public List<Project> ListMyProjects()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            var myProjects = new List<Project>();
            var myRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Admin":
                case "Demo_Admin":
                    myProjects.AddRange(db.Projects);
                    break;
                case "Project Manager":
                case "Demo_ProjectMangaer":
                    myProjects.AddRange(db.Projects);
                    break;
                case "Developer":
                case "Demo_Developer":
                    myProjects.AddRange(user.Projects);
                    break;
                case "Submitter":
                case "Demo_Submitter":
                    myProjects.AddRange(user.Projects);
                    break;

            }
            return myProjects;
                    
        }

    }
}