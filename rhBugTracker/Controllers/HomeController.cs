using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rhBugTracker.Models;


namespace rhBugTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //make and if else to see which role is being logged in 
        private ApplicationDbContext db = new ApplicationDbContext();
               
        public ActionResult Index()
        {
            if (User.IsInRole("Submitter"))
            {
                return View();
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
                return View();
            }           
            
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //Get
        public ActionResult EditProfile(string Id)
        {
            //Get specific user ID 
            var userId = db.Users.Find(Id);
            //Creating view model with info of user
            var user = new UserInformationDisplay();
            //assigning the value of properties
            user.FName = userId.FName;
            user.LName = userId.LName;
            user.DisplayName = userId.DisplayName;
            user.Email = userId.Email;
            user.Password = userId.PasswordHash.ToString();
            //all info passed in as user
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post
        public ActionResult EditProfile(ApplicationUser user)
        {
            var editUser = db.Users.Find(user.Id);
            editUser.Id = user.Id;
            editUser.FName = user.FName;
            editUser.LName = user.LName;
            editUser.DisplayName = user.DisplayName;
            editUser.Email = user.Email;
            editUser.UserName = user.Email;
            //db.Entry(editUser);
            db.SaveChanges();
            return RedirectToAction("EditProfile", "Home");
        }
    }
}