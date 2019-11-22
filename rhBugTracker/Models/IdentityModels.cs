﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace rhBugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "First name must contain 2 - 10 characters.")]
        public string FName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Last name must contain 2 - 10 characters.")]
        public string LName { get; set; }
        [Display(Name = "Display Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Display Name must be 2 - 10 characters.")]
        public string DisplayName { get; set; }
        public string AvatarPath { get; set; }

        public virtual ICollection<Tickets> Tickets { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        //public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        public ApplicationUser()
        {
            TicketComments = new HashSet<TicketComment>();
            Projects = new HashSet<Project>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketHistories = new HashSet<TicketHistory>();
            //TicketNotifications = new HashSet<TicketNotification>();
            Tickets = new HashSet<Tickets>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<rhBugTracker.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<rhBugTracker.Models.Tickets> Tickets { get; set; }        

        public System.Data.Entity.DbSet<rhBugTracker.Models.TicketPriority> TicketPriorities { get; set; }

        public System.Data.Entity.DbSet<rhBugTracker.Models.TicketStatus> TicketStatus { get; set; }

        public System.Data.Entity.DbSet<rhBugTracker.Models.TicketType> TicketTypes { get; set; }

        public System.Data.Entity.DbSet<rhBugTracker.Models.TicketComment> TicketComments { get; set; }

        public System.Data.Entity.DbSet<rhBugTracker.Models.TicketAttachment> TicketAttachments { get; set; }
       
        public System.Data.Entity.DbSet<rhBugTracker.Models.TicketHistory> TicketHistories { get; set; }

        public System.Data.Entity.DbSet<rhBugTracker.Models.TicketNotification> TicketNotifications { get; set; }

       
    }
}