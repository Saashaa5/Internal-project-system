using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRApiSdk;
using NUnit.Framework;

namespace IntegrationTest.DbIntegration
{
    [TestFixture]
    public class EmployeeTest
    {
        private EmployeeSdk employeeSdk;

        public EmployeeTest()
        {
            employeeSdk = new EmployeeSdk();
        }

        [Test]
        public async Task GetEmployeesIntTest()
        {
            var employees = await employeeSdk.GetEmployees();

            Assert.IsTrue(employees.Any(x => x.Name == "Николай"));
        }

        [Test]
        public async Task AddEmployeeIntTest()
        {
            var employeeName = "Наталья";
            var employeeSurname = "Петрова";
            var employeePatronymic = "Евгеньевна";
            var employeeEmail = "nataly@gmail.com";
            var employeeCompanyId = 3;
            var employees = await employeeSdk.GetEmployees();
            if (employees.Any(x => x.Name == employeeName))
            {
                await employeeSdk.DeleteEmployee(employees.FirstOrDefault(x => x.Name == employeeName).ID);
            }
            var employee = await employeeSdk.AddEmployee(employeeName, employeeSurname, employeePatronymic, employeeEmail, employeeCompanyId);

            Assert.IsTrue(employee.ID != 0);
            Assert.AreEqual(employee.Name, employeeName);
        }

        [Test]
        public async Task DeleteEmployeeIntTest()
        {
            var employee = await employeeSdk.AddEmployee("Виктор", "Петров","Петрович","petrov@gmail.com", 2);
            var isSuccess = await employeeSdk.DeleteEmployee(employee.ID);

            Assert.IsTrue(isSuccess);
        }
    }
}
