using Microsoft.AspNet.Identity;
using rhBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Helpers
{
    public class TicketHelper
    {
        //connection to db
        ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();

        //Method to return default status on a ticket "Open"
        public int SetDefaultTicketStatus()
        {
            return db.TicketStatus.FirstOrDefault(ts => ts.Name == "Open").Id;
        }

        //Method to list the tickets that belong to the user that is signed in
        public List<Ticket> ListMyTickets()
        {
            //List of all tickets
            var myTickets = new List<Ticket>();
            //Get the id of the current user id whoever is signed in
            var userId = HttpContext.Current.User.Identity.GetUserId();
            //Get the current user by id and assign to his role 
            var user = db.Users.Find(userId);            
            //Step 1: obtain my role:
            var myRole = roleHelper.ListUserRoles(HttpContext.Current.User.Identity.GetUserId()).FirstOrDefault();

            ////step 2:
            //if (myRole == "Admin")
            //{
            //    myTickets.AddRange(db.Tickets);
            //} else if (myRole == "Project Manager")
            //{
            //    myTickets.AddRange(user.Projects.SelectMany(p => p.Tickets));
            //}else if (myRole == "Developer")
            //{
            //    myTickets.AddRange(db.Tickets.Where(t => t.AssignedToUserId == userId));
            //}else if(myRole == "Submitter")
            //{
            //    myTickets.AddRange(db.Tickets.Where(t => t.OwnerUserId == userId));
            //}
            //else
            //{
            //    return myTickets;
            //}
            //return myTickets;

            switch (myRole)
            {
                case "Admin":
                case "DemoAdmin":
                    myTickets.AddRange(db.Tickets);
                    break;
                case "Project Manager":
                    myTickets.AddRange(user.Projects.SelectMany(p => p.Tickets));
                    break;
                case "Developer":
                    myTickets.AddRange(db.Tickets.Where(t => t.AssignedToUserId == userId));
                    break;
                case "Submitter":
                    myTickets.AddRange(db.Tickets.Where(t => t.OwnerUserId == userId));
                    break;                
            }

            return myTickets;



        }

        public ICollection<Ticket> ListProjectTickets(int projectId)
        {
            Project project = db.Projects.Find(projectId);

            var Tickets = project.Tickets.ToList();
            return (Tickets);
        }
    }
}