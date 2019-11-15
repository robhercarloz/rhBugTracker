using rhBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Helpers
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public void ManageNotifications(Ticket oldTicket,  Ticket newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUser != null;
        }

    }
}