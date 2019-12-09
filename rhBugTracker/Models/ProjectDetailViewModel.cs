using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class ProjectDetailViewModel
    {
        public string projectName { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Role { get; set; }
        public string ImagePath { get; set; }
        public string TicketCount { get; set; }
        public string projectUser { get; set; }

    }
}