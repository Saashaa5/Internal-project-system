using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace HRSystemDAL.DAL
{
    public class EmployeeToProject
    {
        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }
        public bool IsChief { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual int EmployeeId { get; set; }
    }
}
