using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DAL.DAL;


namespace HRSystemDAL.DAL
{
    public class Employee:Base
    {
        public Employee()
        {
            EmployeeToProjects=new HashSet<EmployeeToProject>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public ICollection<EmployeeToProject> EmployeeToProjects { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }

    }
}
