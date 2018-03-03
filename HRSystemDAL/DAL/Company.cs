using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystemDAL.DAL
{
    public class Company:Base
    {
        public Company()
        {
            ProjectsForClients=new HashSet<Project>();
            ProjectsForExecutors=new HashSet<Project>();
            Employees=new HashSet<Employee>();
        }
        public string Name { get; set; }
        public ICollection<Project> ProjectsForClients { get; set; }
        public ICollection<Project> ProjectsForExecutors { get; set; }
        public ICollection<Employee> Employees { get; set; }
        
    }
}
