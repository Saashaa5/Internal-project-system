using System.Data.Entity.ModelConfiguration.Conventions;

using DAL.DAL.EntityConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;
using HRSystemDAL.DAL;
using HRSystemWeb.Models;

namespace HRSystemDAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProjectsDbContext : IdentityDbContext<ApplicationUser>
    {
        public ProjectsDbContext()
            : base("name=HRSystemProjectsConnection")
        {
        }
       public static ProjectsDbContext Create()
        {
            return new ProjectsDbContext();
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<EmployeeToProject> EmployeeToProject { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new EmployeeToProjectConfiguration());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}