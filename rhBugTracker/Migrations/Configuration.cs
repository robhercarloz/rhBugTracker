namespace rhBugTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using rhBugTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<rhBugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(rhBugTracker.Models.ApplicationDbContext context)
        {
            //Role Creation 
            #region
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //Admin
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            //Project Manager
            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }
            //Developer
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            //Submitter/User
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            //Guest
            if (!context.Roles.Any(r => r.Name == "Guest"))
            {
                roleManager.Create(new IdentityRole { Name = "Guest" });
            }

            #endregion

            //Creating the User
            #region
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //AdminUser
            if (!context.Users.Any(u => u.Email == "rboHernandez@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "rboHernandez@Mailinator.com",
                    Email = "rboHernandez@Mailinator.com",
                    FName = "Roberto",
                    LName = "Hernandez",
                    DisplayName = "Admin",
                    AvatarPath = "/Avatars/profile_Placeholder.png"
                }, WebConfigurationManager.AppSettings["AdminPassword"]);
            }
            //ProjectManagerUser
            if (!context.Users.Any(u => u.Email == "coderhcarlos@Gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "coderhcarlos@Gmail.com",
                    Email = "coderhcarlos@Gmail.com",
                    FName = "Rob",
                    LName = "Carlos",
                    DisplayName = "DemoProjectManager",
                    AvatarPath = "/Avatars/profile_Placeholder.png"

                }, WebConfigurationManager.AppSettings["ProjectManagerPassword"]);
            }
            //DeveloperUser
            if (!context.Users.Any(u => u.Email == "Dev@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Dev@Mailinator.com",
                    Email = "Dev@Mailinator.com",
                    FName = "Dev",
                    LName = "Eloper",
                    DisplayName = "Developer",
                    AvatarPath = "/Avatars/profile_Placeholder.png"

                }, WebConfigurationManager.AppSettings["DeveloperPassword"]);
            }
            //Submitter
            //user1
            if (!context.Users.Any(u => u.Email == "User1@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "User1@Mailinator.com",
                    Email = "User1@Mailinator.com",
                    FName = "Bill",
                    LName = "Nye",
                    DisplayName = "User1",
                    AvatarPath = "/Avatars/profile_Placeholder.png"

                }, WebConfigurationManager.AppSettings["User1Password"]);
            }
            //user2
            if (!context.Users.Any(u => u.Email == "User2@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "User2@Mailinator.com",
                    Email = "User2@Mailinator.com",
                    FName = "Rich",
                    LName = "Quan",
                    DisplayName = "User2"
                }, WebConfigurationManager.AppSettings["User2Password"]);
            }
            //user3
            if (!context.Users.Any(u => u.Email == "User3@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "User3@Mailinator.com",
                    Email = "User3@Mailinator.com",
                    FName = "Tech",
                    LName = "Lead",
                    DisplayName = "User3",
                    AvatarPath = "/Avatars/profile_Placeholder.png"

                }, WebConfigurationManager.AppSettings["User3Password"]);
            }

            //----------------------------------------DEMO LOGIN USERS------------------------------------------
            //Admin DEMO
            if (!context.Users.Any(u => u.Email == "_Demo_AdminEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "_Demo_AdminEmail@Mailinator.com",
                    Email = "_Demo_AdminEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "Admin",
                    DisplayName = "DemoAdmin",
                    AvatarPath = "/Avatars/adminAvatar.png"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }
            //PM DEMO
            if (!context.Users.Any(u => u.Email == "_Demo_PMEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "_Demo_PMEmail@Mailinator.com",
                    Email = "_Demo_PMEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "PM",
                    DisplayName = "DemoPM",
                    AvatarPath = "/Avatars/pmAvatar.png"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }
            //Developer Demo
            if (!context.Users.Any(u => u.Email == "_Demo_DeveloperEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "_Demo_DeveloperEmail@Mailinator.com",
                    Email = "_Demo_DeveloperEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "Developer",
                    DisplayName = "DemoDeveloper",
                    AvatarPath = "/Avatars/developerAvatar.png"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "_Demo_SubmitterEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "_Demo_SubmitterEmail@Mailinator.com",
                    Email = "_Demo_SubmitterEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "Submitter",
                    DisplayName = "DemoSubmitter",
                    AvatarPath = "/Avatars/submitterAvatar.png"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }


            context.SaveChanges();
            #endregion

            //Assigning Roles
            #region
            var adminId = userManager.FindByEmail("rboHernandez@Mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");            

            var proMgrId = userManager.FindByEmail("coderhcarlos@Gmail.com").Id;
            userManager.AddToRole(proMgrId, "Project Manager");            

            var developerId = userManager.FindByEmail("Dev@Mailinator.com").Id;
            userManager.AddToRole(developerId, "Developer");            

            var user1Id = userManager.FindByEmail("User1@Mailinator.com").Id;
            userManager.AddToRole(user1Id, "Submitter");

            var user2Id = userManager.FindByEmail("User2@Mailinator.com").Id;
            userManager.AddToRole(user2Id, "Submitter");

            var user3Id = userManager.FindByEmail("User3@Mailinator.com").Id;
            userManager.AddToRole(user3Id, "Submitter");

            //DEMO
            //Demo Admin
            var dAdminId = userManager.FindByEmail("_Demo_AdminEmail@Mailinator.com").Id;
            userManager.AddToRole(dAdminId, "Admin");
            //////Demo Project Manager
            var demoProMgrId = userManager.FindByEmail("_Demo_PMEmail@Mailinator.com").Id;
            userManager.AddToRole(demoProMgrId, "Project Manager");
            //Demo Developer
            var demoDeveloperId = userManager.FindByEmail("_Demo_DeveloperEmail@Mailinator.com").Id;
            userManager.AddToRole(demoDeveloperId, "Developer");
            //DemoSubmitter
            var DemoUserId = userManager.FindByEmail("_Demo_SubmitterEmail@Mailinator.com").Id;
            userManager.AddToRole(DemoUserId, "Submitter");


            #endregion

            //Seeding in ticket status, priority  and typess
            #region 
            //load up a few other  tables 
            context.TicketStatus.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "Open", Description = "A newly created or simply unassigned Ticket" },
                    new TicketStatus { Name = "Assigned", Description = "A ticket that has been created to work on." },
                    new TicketStatus { Name = "In Progress", Description = "The ticket is currently being worked on." },
                    new TicketStatus { Name = "Resolved", Description = "A Ticket that has been completed"},
                    new TicketStatus { Name = "Archived", Description = "A ticket that has been archived"}
                );

            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Immediate", Description = "This priority levels requiers completion within 2 days"},
                    new TicketPriority { Name = "Highest", Description = "This priority levels requires completion within 1 week" },
                    new TicketPriority { Name = "High", Description = "This priority levels requires completion within 2 weeks" },
                    new TicketPriority { Name = "Medium", Description = "This priority levels requieres completion within 3 weeks" },
                    new TicketPriority { Name = "Low", Description = "This priority levels requiers completion within 4 weeks" },
                    new TicketPriority { Name = "None", Description = "This priority levels does not require any attention" }

                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Name = "Defect", Description = "A defect in the software has been identified"},
                    new TicketType { Name = "Feature Request", Description = "The client has called and requested a new feature be added"},
                    new TicketType { Name = "Documentation Request", Description = "The client has called requesting documentation for a specific project" },
                    new TicketType { Name = "Training Request", Description = "The client has called requesting training on the software" }                 

                );


            //ADD PROJECTS AND TICKETS WHEN EVERYTHING WORKS!!!
            #endregion

        }
    }
}
