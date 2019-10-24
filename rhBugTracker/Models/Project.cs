using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class Project
    {
        //Keys
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        //ICollection
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }


        //Constructor
        public Project()
        {
            Tickets = new HashSet<Ticket>();
            Users = new HashSet<ApplicationUser>();
        }
    }
}