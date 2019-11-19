using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class TicketNotification
    {
        //Key
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        //properties
        public string NotificationBody { get; set; }
        public Boolean IsRead { get; set; }

        //Foreign Key Reference
        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}