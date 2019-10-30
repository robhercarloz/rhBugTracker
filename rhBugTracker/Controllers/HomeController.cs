using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhBugTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //make and if else to see which role is being logged in 




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
                return RedirectToAction("", "", null);
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}