using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class TicketAttachment
    {
        //Keys
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        //Properties
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        //Foreign Key References
        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}