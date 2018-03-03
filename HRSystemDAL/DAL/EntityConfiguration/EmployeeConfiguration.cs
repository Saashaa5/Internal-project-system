using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace DAL.DAL.EntityConfiguration
{
    public class EmployeeConfiguration : BaseConfiguration<Employee>
    {

        public EmployeeConfiguration()
        {
            Property(x => x.Email);
            Property(x => x.Name).IsRequired();
            Property(x => x.Patronymic);
            Property(x => x.Surname).IsRequired();
            HasMany(x => x.EmployeeToProjects).WithRequired(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            HasRequired(x => x.Company).WithMany(x => x.Employees).HasForeignKey(x => x.CompanyId);
            ToTable("Employees");
        }
    }
}
