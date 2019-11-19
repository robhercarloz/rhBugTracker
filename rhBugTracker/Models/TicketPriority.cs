using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class TicketPriority
    {   
        //Key
        public int Id { get; set; }

        //Properties
        public string Name { get; set; }
        public string Description { get; set; }

        //Children
        public virtual ICollection<Tickets> Tickets { get; set; } 

        public TicketPriority()
        {
            Tickets = new HashSet<Tickets>();
        }

    }
}