using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class TicketHistory
    {
        //Keys
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        //properties
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Changed { get; set; }

        //Foregin Key Referemce
        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}