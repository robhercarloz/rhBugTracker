using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class TicketComment
    {
        //Key
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string OwnerUserId { get; set; }

        //Properties
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        
        //Foreign Key Reference
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
    }
}