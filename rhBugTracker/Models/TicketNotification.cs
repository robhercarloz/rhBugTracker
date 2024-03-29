﻿using System;
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
        public string SenderId { get; set; }

        //properties
        public string NotificationBody { get; set; }
        public Boolean IsRead { get; set; }
        public string RecipientId { get; set; }
        public DateTime Created { get; set; }


        //Foreign Key Reference
        public virtual Tickets Ticket { get; set; }
        public virtual ApplicationUser Recipient { get; set; }
        public virtual ApplicationUser Sender { get; set; }


    }
}