using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class AdminUserViewModel
    {


    }
    
    //MODEL FOR MANAGE ROLES
    public class ManageRolesViewModel
    {
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public ManageRolesViewModel()
        {
            Projects = new HashSet<Project>();
        }

    }

    //MODEL FOR MANAGE USERS
    public class ManageUsersViewModel
    {
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public virtual ApplicationUser User { get; set; }
       

    }

    //MODEL FOR MYPROJECT
    public class ProjectViewModel
    {
        public string projName { get; set; }
        public string projDescription { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; } 
        public ProjectViewModel()
        {
            Users = new HashSet<ApplicationUser>();
        }
    }

    //MODEL FOR MYTICKET
    public class TicketViewModel
    {
        public string tProjectName { get; set; }
        public string tTicketPriority { get; set; }
        public string tTicketStatus { get; set; }
        public string tTicketType { get; set; }
               
        public string tTitle { get; set; }
        public string tDescription { get; set; }
        public DateTime tCreated { get; set; }
        public DateTime tUpdated { get; set; }
        //submitter
        public string OwnerUserId { get; set; }
        //developer
        public string AssignedToUserId { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }
        public TicketViewModel()
        {
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
        }



    }


}