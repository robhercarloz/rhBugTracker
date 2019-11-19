using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class TicketType
    {
        //Keys
        public int Id { get; set; }

        //Properties
        public string Name { get; set; }
        public string Description { get; set; }

        //Foreign Key
        public virtual ICollection<Tickets> Tickets{ get; set; }

        public TicketType()
        {
            Tickets = new HashSet<Tickets>();
        }
    }
}