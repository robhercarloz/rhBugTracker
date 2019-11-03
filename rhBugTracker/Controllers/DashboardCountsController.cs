
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace rhBugTracker.Controllers
{
    public class DashboardCountsController : Controller
    {
        // GET: DashboardCounts
        public ActionResult Index()
        {
            return View();
        }
    }
}