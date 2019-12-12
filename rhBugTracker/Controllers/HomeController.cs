using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rhBugTracker.Models;
using Microsoft.AspNet.Identity;
using rhBugTracker.Helpers;
using System.IO;

namespace rhBugTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //make and if else to see which role is being logged in 
        private ApplicationDbContext db = new ApplicationDbContext();
        private TicketHelper ticketHelper = new TicketHelper();
        private ProjectHelper projectHelper = new ProjectHelper();
        private UserRolesHelper roleHelper = new UserRolesHelper();


        public ActionResult Index()
        {
            //if (User.IsInRole("Submitter"))
            //{
            //    var userId = User.Identity.GetUserId();

            //    var data = new Dashboard();
            //    data.myTickets = db.Tickets.Where(t => t.OwnerUserId == userId).ToList();
            //    data.myProjects = db.Projects.Where(t => t.ProjectOwnerId == userId).ToList();
            //    data.myUsers = db.Users.ToList();
            //    return View(data);
            //}
            //else if (User.IsInRole("Developer"))
            //{
            //    var userId = User.Identity.GetUserId();

            //    var data = new Dashboard();
            //    data.myTickets = db.Tickets.Where(t => t.OwnerUserId == userId).ToList();
            //    data.myProjects = db.Projects.Where(t => t.ProjectOwnerId == userId).ToList();                
            //    return View(data);
            //}
            //else if (User.IsInRole("Project Manager"))
            //{
            //    var userId = User.Identity.GetUserId();
            //    var data = new Dashboard();
            //    data.myTickets = db.Tickets.Where(t => t.OwnerUserId == userId).ToList();
            //    data.myProjects = db.Projects.Where(t => t.ProjectOwnerId == userId).ToList();
            //    data.myUsers = db.Users.ToList();                
            //    return View(data);
            //}
            //else if (User.IsInRole("Admin"))
            //{
            //    var userId = User.Identity.GetUserId();
            //    var data = new Dashboard();

            //    data.myProjects = db.Projects.ToList();
            //    data.myTickets = db.Tickets.ToList();
            //    data.myUsers = db.Users.ToList();
            //    return View(data);
            //}

            if (User.IsInRole("Submitter"))
            {
                var data = new Dashboard();
                var userId = User.Identity.GetUserId();
                data.myProjects = projectHelper.ListProjectsUserIsOn(userId);
                data.myTickets = db.Tickets.Where(t => t.OwnerUserId == userId).ToList();

                return View(data);
            }
            else if (User.IsInRole("Developer"))
            {
                return RedirectToAction("Index", "Developer", null);
            }
            else if (User.IsInRole("Project Manager"))
            {
                return RedirectToAction("Index", "ProjectManager", null);
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin", null);
            }
            else
            {
                return RedirectToAction("GuestIndex", "Account");
            }


        }

        //GET
        public ActionResult About()
        {

            var userId = User.Identity.GetUserId();
            //Get specific user ID 
            var user = db.Users.Find(userId);
            //Creating view model with info of user
            var profile = new UserInformationDisplay();
            //assigning the value of properties
            profile.FName = user.FName;
            profile.LName = user.LName;
            profile.DisplayName = user.DisplayName;
            profile.Email = user.Email;
            profile.Role = roleHelper.ListUserRole(userId).FirstOrDefault();
            //profile.Password = user.PasswordHash.ToString();
            //all info passed in as user
            return View(profile);
        }

        //Get
        public ActionResult EditProfile(string Id)
        {
            var userId = User.Identity.GetUserId();
            //Get specific user ID 
            var user = db.Users.Find(Id);           
            //Creating view model with info of user
            var profile = new UserInformationDisplay();
            //assigning the value of properties
            profile.FName = user.FName;
            profile.LName = user.LName;
            profile.DisplayName = user.DisplayName;
            profile.Email = user.Email;
            profile.Role = roleHelper.ListUserRole(userId).FirstOrDefault();
            //profile.Password = user.PasswordHash.ToString();
            //all info passed in as user
            return View(profile);
        }
        
        //Post
        
        [HttpPost]           
        public ActionResult EditProfile(UserInformationDisplay model, HttpPostedFileBase avatarImage)
        {

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);            
            user.FName = model.FName;
            user.LName = model.LName;
            user.DisplayName = model.DisplayName;
            user.Email = model.Email;
            user.UserName = model.Email;
            //db.Entry(editUser);

            //if(avatarImage != null)
            //{
            //    if (ImageUploadValidator.IsWebFriendlyImage(avatarImage))
            //    {
            //        var filename = Path.GetFileName(avatarImage.FileName);
            //        avatarImage.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), filename));
            //        user.AvatarPath = "/Avatars/" + filename;
            //    }
            //}

            if (avatarImage != null)
            {
                if (ImageUploadValidator.IsWebFriendlyImage(avatarImage))
                {
                    var fileName = Path.GetFileName(avatarImage.FileName);
                    var justFileName = Path.GetFileNameWithoutExtension(fileName);
                    justFileName = StringUtilities.URLFriendly(justFileName);
                    fileName = $"{justFileName}_{DateTime.Now.Ticks}{Path.GetExtension(fileName)}";

                    avatarImage.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), fileName));
                    user.AvatarPath = "/Avatars/" + fileName;
                }
            }


            db.SaveChanges();
            return RedirectToAction("EditProfile", "Home");
        }
    }
}