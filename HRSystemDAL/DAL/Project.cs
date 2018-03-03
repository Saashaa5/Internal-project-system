using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAL;

namespace HRSystemDAL.DAL
{
    public class Project : Base
    {
        public Project()
        {
            ProjectToEmployees = new HashSet<EmployeeToProject>();
        }

        public string Name { get; set; }
        public decimal Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public Company ExecutorCompany { get; set; }
        public Company ClientCompany { get; set; }
        public ICollection<EmployeeToProject> ProjectToEmployees { get; set; }
        public int ExecutorCompanyId { get; set; }
        public int ClientCompanyId { get; set; }
    }
}
