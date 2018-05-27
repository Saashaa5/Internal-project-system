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
    public class EmployeeSdk
    {
        HttpClient client = new HttpClient();

        public async Task<List<Employee>> GetEmployees()
        {
            var response = await client.GetAsync("http://localhost:3341/Employee/GetEmployees?filter=&sortColumn=&sortDirection=&page=1&pageSize=100");
            var json = await response.Content.ReadAsStringAsync();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(dictionary["jsonModel"]);

            return employees;
        }

        public async Task<Employee> AddEmployee(string employeeName, string employeeSurname, string employeePatronymic, string employeeEmail, int employeeCompanyId)
        {
            var response = await client.GetAsync($"http://localhost:3341/Employee/AddEmployee?name={employeeName}&surname={employeeSurname}&patronymic={employeePatronymic}&email={employeeEmail}&companyId={employeeCompanyId}");
            var json = await response.Content.ReadAsStringAsync();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var employee = JsonConvert.DeserializeObject<Employee>(dictionary["jsonModel"]);

            return employee;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var response = await client.GetAsync("http://localhost:3341/Employee/DeleteEmployee?id=" + employeeId);
            var json = await response.Content.ReadAsStringAsync();
            var isSuccess = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);

            return isSuccess["success"];
        }
    }
}
