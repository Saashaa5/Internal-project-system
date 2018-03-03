using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSystemBLL.Contracts;
using HRSystemDAL.DAL;
using HRSystemDAL.Repository;
using HRSystemDAL.UnitOfWork;

namespace HRSystemBLL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public GenericRepository<Project> ProjectRepository;
        public GenericRepository<EmployeeToProject> EmployeeToProjectRepository;
        public GenericRepository<Company> CompanyRepository;
        public ProjectService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ProjectRepository = UnitOfWork.GetRepository<Project>();
            EmployeeToProjectRepository = UnitOfWork.GetRepository<EmployeeToProject>();
            CompanyRepository = UnitOfWork.GetRepository<Company>();
        }

        public List<Project> GetProjects(string filter, string sortColumn, string sortDirection, int page, int pageSize)
        {
            Func<IQueryable<Project>, IOrderedQueryable<Project>> orderBy = null;
            List<Project> projects;
            if (sortColumn != "")
            {
                if (sortDirection == "ASC")
                {
                    orderBy = m => m.OrderBy(x => sortColumn);
                }
                else
                {
                    orderBy = m => m.OrderByDescending(x => sortColumn);
                }
            }
            if (filter != "")
            {
                projects = ProjectRepository
               .Get(x => x.Name.Contains(filter), orderBy)
               .Skip((page - 1) * pageSize)
               .ToList();
            }
            else
                projects = ProjectRepository
              .Get(null, orderBy)
              .Skip((page - 1) * pageSize)
              .ToList();
            return projects;
        }

        public void AddEmployees(int id, int[] employees, int chief)
        {

            var employeesToProjects = EmployeeToProjectRepository.Get(x => x.ProjectId == id);
            foreach (var employeeToProject in employeesToProjects)
            {
                EmployeeToProjectRepository.Delete(employeeToProject);
            }
            UnitOfWork.Save();
            foreach (var employee in employees)
            {

                EmployeeToProjectRepository.Insert(new EmployeeToProject()
                {
                    EmployeeId = employee,
                    IsChief = false,
                    ProjectId = id
                });
            }
            EmployeeToProjectRepository.Insert(new EmployeeToProject()
            {
                EmployeeId = chief,
                IsChief = true,
                ProjectId = id
            });
            UnitOfWork.Save();
        }

        public Project AddProject(int clientCompanyId, int executorCompanyId, string name,
            string comment, DateTime startDate, DateTime endDate, decimal priority)
        {
            var clientCompany = CompanyRepository.GetByID(clientCompanyId);
            var executorCompany = CompanyRepository.GetByID(executorCompanyId);
            var project = new Project()
            {
                ClientCompany = clientCompany,
                ExecutorCompany = executorCompany,
                Name = name,
                Comment = comment,
                StartDate = startDate,
                EndDate = endDate,
                Priority = priority
            };
            ProjectRepository.Insert(project);
            UnitOfWork.Save();
            return project;

        }

        public IEnumerable<int> GetEmployeesToProjects(int id, bool isChief)
        {
            var employeesToProject = EmployeeToProjectRepository.Get(x => x.ProjectId == id&&x.IsChief==isChief);
            return employeesToProject.Select(x => x.EmployeeId);
        }

        public Project GetProject(int id)
        {
            return ProjectRepository.GetByID(id);
        }

        public Project UpdateProject(Project project)
        {

            var clientCompany = CompanyRepository.GetByID(project.ClientCompanyId);
            var executorCompany = CompanyRepository.GetByID(project.ExecutorCompanyId);
            project.ClientCompany = clientCompany;
            project.ExecutorCompany = executorCompany;
            ProjectRepository.Update(project);
            UnitOfWork.Save();
            return project;
        }

        public bool DeleteProject(int id)
        {
            if (ProjectRepository.GetByID(id).ProjectToEmployees.Any(x => x.IsChief != true))
            {
                return false;
            }
            ProjectRepository.Delete(id);
            UnitOfWork.Save();
            return true;
        }



    }
}
