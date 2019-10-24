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

        public object WebConfiguration { get; private set; }

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
            if (!context.Users.Any(u => u.Email == "rhernandezroberto@Gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "rhernandezroberto@Gmail.com",
                    Email = "rhernandezroberto@Gmail.com",
                    FName = "Roberto",
                    LName = "Hernandez",
                    DisplayName = "DemoAdmin"
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
                    DisplayName = "DemoProjectManager"
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
                    DisplayName = "DemoDeveloper"
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
                    DisplayName = "DemoUser1"
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
                    DisplayName = "DemoUser3"
                }, WebConfigurationManager.AppSettings["User3Password"]);
            }
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
            #endregion

        }
    }
}
