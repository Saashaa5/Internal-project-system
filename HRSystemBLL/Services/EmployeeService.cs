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
    public class EmployeeService : BaseService, IEmployeeService
    {
        public GenericRepository<Employee> EmployeeRepository;
        public GenericRepository<Company> CompanyRepository;
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            EmployeeRepository = UnitOfWork.GetRepository<Employee>();
            CompanyRepository = UnitOfWork.GetRepository<Company>();
        }

        public Employee GetEmployee(int id)
        {
            var employee = EmployeeRepository.GetByID(id);
            return employee;
        }
        public List<Employee> GetEmployees(string filter, string sortColumn, string sortDirection, int page, int pageSize)
        {
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null;
            List<Employee> employees;
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
                employees = EmployeeRepository
               .Get(x => x.Name.Contains(filter), orderBy)
               .Skip((page - 1) * pageSize)
               .ToList();
            }
            else
                employees = EmployeeRepository
              .Get(null, orderBy)
              .Skip((page - 1) * pageSize)
              .ToList();
            return employees;
        }

        public Employee AddEmployee(string name, string surName, string patronymic, int companyId, string email)
        {

            var company = CompanyRepository.GetByID(companyId);
            EmployeeRepository.Insert(new Employee()
            {
                Name = name,
                Company = company,
                Email = email,
                Patronymic = patronymic,
                Surname = surName
            });

            UnitOfWork.Save();
            var employee = EmployeeRepository.Get(x => x.Name == name).FirstOrDefault();
            return employee;
        }

        public bool DeleteEmployee(int id)
        {

            if (!EmployeeRepository.Get(x => x.ID == id).Any())
            {
                return false;
            }
            EmployeeRepository.Delete(id);
            UnitOfWork.Save();
            return true;
        }
        public string UpdateEmployee(Employee employee)
        {
            var company = CompanyRepository.GetByID(employee.CompanyId);
            employee.Company = company;
            EmployeeRepository.Update(employee);
            UnitOfWork.Save();
            return company.Name;
        }

    }
}
