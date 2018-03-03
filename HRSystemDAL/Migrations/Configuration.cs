using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HRSystemWeb.Models;
using HRSystemWeb;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<HRSystemDAL.ProjectsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HRSystemDAL.ProjectsDbContext context)
        {
            if (!(context.Users.Any(u => u.UserName == "Admin")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var roleStore = new RoleStore<ApplicationRole>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleManager = new RoleManager<ApplicationRole>(roleStore);
                var userToInsert = new ApplicationUser { UserName = "Admin" };
                var roleToInsert = new ApplicationRole() { Name = "Admin" };
                userManager.Create(userToInsert, "12qw!@QW");
                roleManager.Create(roleToInsert);
                userManager.AddToRole(userToInsert.Id, roleToInsert.Name);
            }

       }
    }
}
