using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace DAL.DAL.EntityConfiguration
{
    public class ProjectConfiguration : BaseConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            Property(x => x.Name).IsRequired();
            Property(x => x.Comment);
            Property(x => x.EndDate).IsOptional().HasColumnType("datetime2");
            Property(x => x.StartDate).IsOptional().HasColumnType("datetime2");
            Property(x => x.Priority).IsRequired();
            HasRequired(x => x.ExecutorCompany).WithMany(x => x.ProjectsForExecutors).HasForeignKey(x => x.ExecutorCompanyId);
            HasRequired(x => x.ClientCompany).WithMany(x => x.ProjectsForClients).HasForeignKey(x => x.ClientCompanyId);
            HasMany(x => x.ProjectToEmployees).WithRequired(x => x.Project).HasForeignKey(x => x.ProjectId);
            ToTable("Projects");
        }
    }
}
