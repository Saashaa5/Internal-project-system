using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;

namespace HRSystemBLL.Contracts
{
    public interface IProjectService
    {
        Project AddProject(int clientCompanyId, int executorCompanyId, string name,
            string comment, DateTime startDate, DateTime endDate, decimal priority);

        bool DeleteProject(int id);
        Project UpdateProject(Project project);
        Project GetProject(int id);
        void AddEmployees(int id,int[] employees, int chief);
        List<Project> GetProjects(string filter, string sortColumn, string sortDirection, int page, int pageSize);
        IEnumerable<int> GetEmployeesToProjects(int id, bool isChief);

    }
}
