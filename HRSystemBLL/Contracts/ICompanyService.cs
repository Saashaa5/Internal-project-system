using HRSystemDAL.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystemBLL.Contracts
{
    public interface ICompanyService
    {
        Company AddCompany(string name);
        List<Company> GetCompanies(string filter, string sortColumn, string sortDirection, int page, int pageSize);
        bool DeleteCompany(int id);
        void UpdateCompany(Company company);
        Company GetCompany(int id);
    }
}
