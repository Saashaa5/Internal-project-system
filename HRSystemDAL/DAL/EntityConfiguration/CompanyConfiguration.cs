using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace DAL.DAL.EntityConfiguration
{
    public class CompanyConfiguration:  BaseConfiguration<Company>
    {
        public CompanyConfiguration()
        {
           Property(x => x.Name).IsRequired();
            HasMany(x => x.ProjectsForClients).WithRequired(x => x.ClientCompany).HasForeignKey(x=>x.ClientCompanyId);
            HasMany(x => x.ProjectsForExecutors).WithRequired(x => x.ExecutorCompany).HasForeignKey(x=>x.ExecutorCompanyId);
            HasMany(x => x.Employees).WithRequired(x => x.Company).HasForeignKey(x => x.CompanyId);
            ToTable("Companies");
        }
    }
}
