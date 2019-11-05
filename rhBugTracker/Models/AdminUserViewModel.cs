using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class AdminUserViewModel
    {


    }
    
    //Model for MANAGE ROLES
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

    //Model for MANAGE USERS
    public class ManageUsersViewModel
    {
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

    }

}