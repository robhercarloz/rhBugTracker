using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class Dashboard
    {
        public virtual ICollection<Project> myprojects { get; set; }
        public virtual ICollection<Ticket> mytickets { get; set; }
        public virtual ICollection<ApplicationUser> myusers { get; set; }
        public Dashboard()
        {
            myprojects = new HashSet<Project>();
            mytickets = new HashSet<Ticket>();
            myusers = new HashSet<ApplicationUser>();
        }
    }
}