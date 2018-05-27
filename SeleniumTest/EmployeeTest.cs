using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTest.PageObject;

namespace SeleniumTest
{
    public class EmployeeTest
    {
        private IWebDriver driver;

        public EmployeeTest()
        {
        }

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost:8080";
        }

        [Test]
        public void AddEmployeeTest()
        {
            var name = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
            var surname = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
            var patronymic = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
            var email = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5) + "@gmail.com";

            var loginPage = new Login(driver);
            loginPage.LoginAs("asd", "qwerty");

            AddEmployee(name, surname, patronymic, email);
        }

        [Test]
        public void AddAndDeleteEmployee()
        {
            var loginPage = new Login(driver);
            loginPage.LoginAs("asd", "qwerty");

            var name = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
            var surname = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
            var patronymic = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5);
            var email = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 5) + "@gmail.com";

            var employee = AddEmployee(name, surname, patronymic, email);
            employee.DeleteEmployee(email);
            Thread.Sleep(2000);
            employee.ConfirmSuccess();
            Thread.Sleep(2000);
            employee.ConfirmSuccess();
            Thread.Sleep(1000);

            var employeeEmails = employee.GetEmployeeEmails();

            var addedEmployee = employeeEmails.FirstOrDefault(x => x.Text == email);

            Assert.IsNull(addedEmployee);
        }

        private Employee AddEmployee(string name, string surname, string patronymic, string email)
        {
            var addEmployee = new Employee(driver);
            addEmployee.NavigateToEmployeeTab();

            Thread.Sleep(2000);
            addEmployee.AddEmployee(name, surname, patronymic, email);
            addEmployee.ConfirmSuccess();

            var employeeEmails = addEmployee.GetEmployeeEmails();

            var addedEmployee = employeeEmails.FirstOrDefault(x => x.Text == email);

            Assert.IsNotNull(addedEmployee);

            return addEmployee;
        }

        [TearDown]
        public void Close()
        {
            driver.Close();
            driver.Dispose();
        }
    }
}
