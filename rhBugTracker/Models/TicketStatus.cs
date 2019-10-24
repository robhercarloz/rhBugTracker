using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class TicketStatus
    {
        //Keys
        public int Id { get; set; }

        //Properties
        public string Name { get; set; }
        public string Description { get; set; }

        //Key Reference
        public virtual ICollection<Ticket> Tickets { get; set; }

        //Constructor 
        public TicketStatus()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}