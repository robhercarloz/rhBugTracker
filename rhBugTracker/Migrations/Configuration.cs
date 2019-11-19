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
                }, "psWord0101! ");
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
            if (!context.Users.Any(u => u.Email == "DemoDev@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDev@Mailinator.com",
                    Email = "DemoDev@Mailinator.com",
                    FName = "Dev",
                    LName = "Eloper",
                    DisplayName = "DemoDeveloper",
                    AvatarPath = "/Avatars/profile_Placeholder.png"

                }, WebConfigurationManager.AppSettings["DeveloperPassword"]);
            }
            //Submitter
            //user1
            if (!context.Users.Any(u => u.Email == "DemoUser1@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoUser1@Mailinator.com",
                    Email = "DemoUser1@Mailinator.com",
                    FName = "Bill",
                    LName = "Nye",
                    DisplayName = "DemoUser1",
                    AvatarPath = "/Avatars/profile_Placeholder.png"

                }, WebConfigurationManager.AppSettings["User1Password"]);
            }
            //user2
            if (!context.Users.Any(u => u.Email == "DemoUser2@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoUser2@Mailinator.com",
                    Email = "DemoUser2@Mailinator.com",
                    FName = "Rich",
                    LName = "Quan",
                    DisplayName = "DemoUser2"
                }, WebConfigurationManager.AppSettings["User2Password"]);
            }
            //user3
            if (!context.Users.Any(u => u.Email == "DemoUser3@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoUser3@Mailinator.com",
                    Email = "DemoUser3@Mailinator.com",
                    FName = "Tech",
                    LName = "Lead",
                    DisplayName = "DemoUser3",
                    AvatarPath = "/Avatars/profile_Placeholder.png"

                }, WebConfigurationManager.AppSettings["User3Password"]);
            }

            //DEMO LOGIN USERS
            //Admin DEMO
            if (!context.Users.Any(u => u.Email == "DemoAdminEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdminEmail@Mailinator.com",
                    Email = "DemoAdminEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "Admin",
                    DisplayName = "DemoAdmin",
                    AvatarPath = "/Avatars/profile_Placeholder.png"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }
            //PM DEMO
            if (!context.Users.Any(u => u.Email == "DemoPMEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoPMEmail@Mailinator.com",
                    Email = "DemoPMEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "PM",
                    DisplayName = "DemoPM",
                    AvatarPath = "~/Avatars/profile_Placeholder.png"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }
            //Developer Demo
            if (!context.Users.Any(u => u.Email == "DemoDeveloperEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloperEmail@Mailinator.com",
                    Email = "DemoDeveloperEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "Developer",
                    DisplayName = "DemoDeveloper",
                    AvatarPath = "~/Avatars/profile_Placeholder.png"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitterEmail@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitterEmail@Mailinator.com",
                    Email = "DemoSubmitterEmail@Mailinator.com",
                    FName = "Demo",
                    LName = "Submitter",
                    DisplayName = "DemoSubmitter",
                    AvatarPath = "~/Avatars/profile_Placeholder.png"
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

            var developerId = userManager.FindByEmail("DemoDev@Mailinator.com").Id;
            userManager.AddToRole(developerId, "Developer");            

            var user1Id = userManager.FindByEmail("DemoUser1@Mailinator.com").Id;
            userManager.AddToRole(user1Id, "Submitter");

            var user2Id = userManager.FindByEmail("DemoUser2@Mailinator.com").Id;
            userManager.AddToRole(user2Id, "Submitter");

            var user3Id = userManager.FindByEmail("DemoUser3@Mailinator.com").Id;
            userManager.AddToRole(user3Id, "Submitter");

            //DEMO
            //Demo Admin
            var dAdminId = userManager.FindByEmail("DemoAdminEmail@Mailinator.com").Id;
            userManager.AddToRole(dAdminId, "Admin");

            //////Demo Project Manager
            var demoProMgrId = userManager.FindByEmail("DemoPMEmail@Mailinator.com").Id;
            userManager.AddToRole(demoProMgrId, "Project Manager");
            //Demo Developer
            var demoDeveloperId = userManager.FindByEmail("DemoDeveloperEmail@Mailinator.com").Id;
            userManager.AddToRole(demoDeveloperId, "Developer");
            //DemoSubmitter
            var DemoUserId = userManager.FindByEmail("DemoSubmitterEmail@Mailinator.com").Id;
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
