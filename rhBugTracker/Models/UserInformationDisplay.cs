using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhBugTracker.Models
{
    public class UserInformationDisplay
    {
        public string FName { get; set;}
        public string LName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public string ImagePath { get; set; }
        public string Role { get; set; }

        public string FullName { get; set; }

    }
}