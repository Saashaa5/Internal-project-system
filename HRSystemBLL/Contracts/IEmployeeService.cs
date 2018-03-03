using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace HRSystemBLL.Contracts
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees(string filter, string sortColumn, string sortDirection, int page, int pageSize);
        Employee AddEmployee(string name, string surName, string patronymic, int companyId, string email);
        Employee GetEmployee(int id);
        bool DeleteEmployee(int id);
        string UpdateEmployee(Employee employee);
    }
}
