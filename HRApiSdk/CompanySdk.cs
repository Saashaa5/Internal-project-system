using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HRSystemDAL.DAL;
using Newtonsoft.Json;

namespace HRApiSdk
{
    public class CompanySdk
    {
        HttpClient client = new HttpClient();

        public async Task<List<Company>> GetCompanies()
        {
            var response = await client.GetAsync("http://localhost:3341/Company/GetCompanies?filter=&sortColumn=&sortDirection=&page=1&pageSize=100");
            var json = await response.Content.ReadAsStringAsync();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var companies = JsonConvert.DeserializeObject<List<Company>>(dictionary["jsonModel"]);

            return companies;
        }

        public async Task<Company> AddCompany(string companyName)
        {
            var response = await client.GetAsync("http://localhost:3341/Company/AddCompany?name=" + companyName);
            var json = await response.Content.ReadAsStringAsync();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var company = JsonConvert.DeserializeObject<Company>(dictionary["jsonModel"]);

            return company;
        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            var response = await client.GetAsync("http://localhost:3341/Company/DeleteCompany?id=" + companyId);
            var json = await response.Content.ReadAsStringAsync();
            var isSuccess = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);

            return isSuccess["success"];
        }
    }
}
