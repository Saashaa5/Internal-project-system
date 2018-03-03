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
   public class CompanyService:BaseService, ICompanyService
   {
       public GenericRepository<Company> Repository; 
       public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
       {
           Repository = UnitOfWork.GetRepository<Company>(); 
       }

        public bool DeleteCompany(int id)
        {
            
            if (UnitOfWork.GetRepository<Employee>().Get(x=>x.CompanyId==id).Any())
            {
                return false;
            }
            Repository.Delete(id);
            UnitOfWork.Save();
            return true;
        }
        public void UpdateCompany(Company company)
        {
            Repository.Update(company);
            UnitOfWork.Save();
        }

        public Company AddCompany(string name)
       {
          
           if (Repository.Get(x => x.Name == name).Any())
           {
               return null;
           }
            Repository.Insert(new Company()
            {
                Name = name
            });
            UnitOfWork.Save();
            var company = Repository.Get(x => x.Name == name).FirstOrDefault();
           return company;
       }

       public Company GetCompany(int id)
       {
            
          return Repository.GetByID(id);
        }
       public List<Company> GetCompanies(string filter, string sortColumn, string sortDirection, int page, int pageSize)//метод для получения списка компаний внешней пагинации, фильтрации, или сортировки
       {
            Func<IQueryable<Company>, IOrderedQueryable<Company>> orderBy = null;
            List<Company> companies;
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
                companies = Repository
               .Get(x => x.Name.Contains(filter), orderBy)
               .Skip((page - 1) * pageSize)
               .ToList();
            }
            else
                companies = Repository
              .Get(null, orderBy)
              .Skip((page - 1) * pageSize)
              .ToList();
           return companies;
       }
    }
}
