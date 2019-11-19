using Microsoft.AspNet.Identity;
using rhBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace rhBugTracker.Helpers
{
    public class NotificationsHelper
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications(Tickets oldTicket, Tickets newTicket)
        {
            var ticketHasBeenAssigned = oldTicket.AssignedToUserId == null && newTicket.AssignedToUserId != null;
            var ticketHasBeenUnassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;
            var ticketHasBeenReassigned = oldTicket.AssignedToUserId != null && newTicket.AssignedToUserId == null;
            if (ticketHasBeenAssigned)
            {
                AddAssignmentNotification(oldTicket, newTicket);
            }
            else if (ticketHasBeenUnassigned)
            {
                AddUnassignmentNotification(oldTicket, newTicket);
            }
            else if (ticketHasBeenReassigned)
            {
                AddAssignmentNotification(oldTicket, newTicket);
                AddUnassignmentNotification(oldTicket, newTicket);
            }
        }
        private void AddAssignmentNotification(Tickets oldTicket, Tickets newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                RecipientId = newTicket.AssignedToUserId,
                NotificationBody = $"You have been assigned to a ticket Id {newTicket.Id} on project {newTicket.Project.Name}. The ticket title is {newTicket.Title}."
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
        private void AddUnassignmentNotification(Tickets oldTicket, Tickets newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                RecipientId = newTicket.AssignedToUserId,
                NotificationBody = $"You have been unassigned to a ticket Id {newTicket.Id} on project {newTicket.Project.Name}. The ticket title is {newTicket.Title}."
            };
            db.TicketNotifications.Remove(notification);
            db.SaveChanges();
        }
        public static List<TicketNotification> GetUnreadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Include("Sender").Include("Recipient").Where(t => t.RecipientId == currentUserId && !t.IsRead).ToList();
        }
    }
}