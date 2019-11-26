using rhBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rhBugTracker.Controllers
{
    public class GraphingController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Graphing
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ProduceChart1Data()
        {
            var myData = new List<MorrisBarData>();
            MorrisBarData data = null;
            foreach(var priority in db.TicketPriorities.ToList())
            {
                data = new MorrisBarData();
                data.label = priority.Name;
                data.value = db.Tickets.Where(t => t.TicketPriority.Name == priority.Name).Count();
                myData.Add(data);
            }
            return Json(myData);
        }

        public JsonResult ProduceChart2Data()
        {
            var myData = new List<MorrisBarData>();
            //MorrisBarData data = null;
            foreach(var status in db.TicketStatus.ToList())
            {
                myData.Add(new MorrisBarData
                {
                    label = status.Name,
                    value = db.Tickets.Where(t => t.TicketStatus.Name == status.Name).Count()
                });
            }
            return Json(myData);
        }


    }
}