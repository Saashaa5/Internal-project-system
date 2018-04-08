using HRSystemBLL.Contracts;
using HRSystemDAL.DAL;
using HRSystemWeb.Controllers;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tests.Common;

namespace HRSystemTest.Controllers
{
    [TestClass()]
    public class EmployeeControllerTest : UnityContainerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService = new Mock<IEmployeeService>();
        [TestInitialize()]
        public override void InitializeTests()
        {
            UnityContainer = new UnityContainer()
                .RegisterType<EmployeeController>()
                .RegisterInstance(typeof(IEmployeeService), _mockEmployeeService.Object);
            ServiceLocator = new UnityServiceLocator(UnityContainer);
        }

        [TestMethod()]
        public void AddEmployeesTest()
        {
            //Arrange
            var employee = new Employee
            {
                Name = "EmployeeName",
                Surname = "EmployeeSurName",
                Patronymic = "EmployeePatronimic",
                Email = "EmployeeEmail",
                CompanyId = 1
            };
            _mockEmployeeService.Setup(x => x.AddEmployee(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(employee).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(EmployeeController)) as EmployeeController;

            //Act
            var result = controller.AddEmployee(employee.Name, employee.Surname, employee.Patronymic, employee.Email, employee.CompanyId) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("jsonModel");

            var model = JsonConvert.DeserializeObject<Employee>(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Name == employee.Name);
            Assert.IsTrue(model.Surname == employee.Surname);
            Assert.IsTrue(model.Patronymic == employee.Patronymic);
            Assert.IsTrue(model.Email == employee.Email);
            Assert.IsTrue(model.CompanyId == employee.CompanyId);

            _mockEmployeeService.Verify();
        }

        [TestMethod()]
        public void GetEmployeesTest()
        {
            //Arrange
            var employees = new List<Employee>() { new Employee
            {
                Name = "EmployeeName",
                Surname = "EmployeeSurName",
                Patronymic = "EmployeePatronimic",
                Email = "EmployeeEmail",
                CompanyId = 1
            },
            new Employee {
                Name = "EmployeeName1",
                Surname = "EmployeeSurName1",
                Patronymic = "EmployeePatronimic1",
                Email = "EmployeeEmail1",
                CompanyId = 2
                }
            };
            _mockEmployeeService.Setup(x => x.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(employees).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(EmployeeController)) as EmployeeController;

            //Act
            var result = controller.GetEmployees(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("jsonModel");

            var model = JsonConvert.DeserializeObject <List<Employee>>(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Count == employees.Count);

            _mockEmployeeService.Verify();
        }
        [TestMethod()]
        public void DeleteEmployeeTest()
        {
            _mockEmployeeService.Setup(x => x.DeleteEmployee(It.IsAny<int>()))
                .Returns(true).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(EmployeeController)) as EmployeeController;

            //Act
            var result = controller.DeleteEmployee(It.IsAny<int>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("success");

            var model = Convert.ToBoolean(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model == true);

            _mockEmployeeService.Verify();
        }
        [TestMethod()]
        public void UpdateEmployeeTest()
        {
            var employee = new Employee
            {
                Name = "EmployeeName1",
                Surname = "EmployeeSurName1",
                Patronymic = "EmployeePatronimic1",
                Email = "EmployeeEmail1",
                CompanyId = 2
            };
            _mockEmployeeService.Setup(x => x.UpdateEmployee(It.IsAny<Employee>()))
                .Returns(employee.Name).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(EmployeeController)) as EmployeeController;

            //Act
            var result = controller.UpdateEmployee(JsonConvert.SerializeObject(employee)) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("employeeName");

            var model = property.GetValue(result.Data, null);

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model == employee.Name);

            _mockEmployeeService.Verify();
        }
    }
}
