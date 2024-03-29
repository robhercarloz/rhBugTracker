﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class Tickets
    {
        //Keys
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }
        public int TicketStatusId { get; set; }
        //submitter
        public string OwnerUserId { get; set; }
        //developer
        public string AssignedToUserId { get; set; }



        //properties
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Title must contain 2 - 30 characters.")]
        public string Title { get; set; }
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Description must be 5 - 40 characters.")]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }


        //Foreign Key reference
        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }               
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; } 

        //Children
        public virtual ICollection<TicketAttachment> TicketAttachments {get; set;}
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        //constructor
        public Tickets()
        {
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
        }
    }
}