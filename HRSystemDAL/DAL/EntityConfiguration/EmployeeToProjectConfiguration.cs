using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace DAL.DAL.EntityConfiguration
{
    public class EmployeeToProjectConfiguration : EntityTypeConfiguration<EmployeeToProject>
    {
        public EmployeeToProjectConfiguration()
        {
            HasKey(x => new { x.EmployeeId, x.ProjectId });
            HasRequired(x => x.Employee).WithMany(x => x.EmployeeToProjects).HasForeignKey(x => x.EmployeeId);
            HasRequired(x => x.Project).WithMany(x => x.ProjectToEmployees).HasForeignKey(x => x.ProjectId);
            Property(x => x.IsChief);
            ToTable("EmployeeToProject");
        }
    }
}
