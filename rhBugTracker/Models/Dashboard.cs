using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class Dashboard
    {
        public virtual ICollection<Project> myProjects { get; set; }
        public virtual ICollection<Ticket> myTickets { get; set; }
        public virtual ICollection<ApplicationUser> myUsers { get; set; }
        
        public Dashboard()
        {
            myProjects = new HashSet<Project>();
            myTickets = new HashSet<Ticket>();
            myUsers = new HashSet<ApplicationUser>();
        }
    }
}