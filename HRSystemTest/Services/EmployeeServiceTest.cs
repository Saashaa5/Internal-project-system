using HRSystemBLL.Contracts;
using HRSystemBLL.Services;
using HRSystemDAL;
using HRSystemDAL.DAL;
using HRSystemDAL.Repository;
using HRSystemDAL.UnitOfWork;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;

namespace HRSystemTest.Services
{
    [TestClass()]
    public class EmployeeServiceTest : UnityContainerTests
    {
        private readonly Mock<IGenericRepository<Employee>> _mockEmployeeRepository = new Mock<IGenericRepository<Employee>>();
        private readonly Mock<IGenericRepository<Company>> _mockCompanyRepository = new Mock<IGenericRepository<Company>>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        [TestInitialize()]
        public override void InitializeTests()
        {
            _mockUnitOfWork.Setup(x => x.GetRepository<Employee>())
                .Returns(_mockEmployeeRepository.Object).Verifiable("Method should be called");
            _mockUnitOfWork.Setup(x => x.GetRepository<Company>())
                .Returns(_mockCompanyRepository.Object).Verifiable("Method should be called");

            UnityContainer = new UnityContainer()
                .RegisterType<IEmployeeService, EmployeeService>()
                .RegisterInstance(typeof(IUnitOfWork), _mockUnitOfWork.Object)
                .RegisterInstance(typeof(IGenericRepository<Employee>), _mockEmployeeRepository.Object)
                .RegisterInstance(typeof(IGenericRepository<Company>), _mockCompanyRepository.Object);
            ServiceLocator = new UnityServiceLocator(UnityContainer);
        }

        [TestMethod()]
        public void GetEmployee()
        {
            var employee = new Employee()
            {
                ID = 1,
                Name = "Name"
            };
            //Arrange
            _mockEmployeeRepository.Setup(
                x => x.GetByID(It.IsAny<Int32>()))
                .Returns(employee)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IEmployeeService)) as EmployeeService;

            //Act
            if (service != null)
            {
                var result = service.GetEmployee(0);

                //Assert
                Assert.AreSame(employee, result);
            }
            else
            {
                throw new Exception();
            }
            _mockEmployeeRepository.Verify();
        }

        [TestMethod()]
        public void GetEmployees()
        {
            var employees = new List<Employee>() { new Employee{
                ID = 1,
                Name = "Name"
            } };
            //Arrange
            _mockEmployeeRepository.Setup(
                x => x.Get(null, null, ""))
                .Returns(employees)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IEmployeeService)) as EmployeeService;

            //Act
            if (service != null)
            {
                var result = service.GetEmployees("", "", "", 1, 1);
                //Assert
                Assert.AreSame(employees.First(), result.First());
            }
            else
            {
                throw new Exception();
            }
            _mockEmployeeRepository.Verify();
        }

        [TestMethod()]
        public void AddEmployee()
        {
            var company = new Company()
            {
                ID = 1,
                Name = "Name"
            };

            var employee = new Employee()
            {
                ID = 1,
                Name = "Name",
                Surname = "Surname",
                Patronymic = "Patronimic",
                CompanyId = 1,
                Email = "Email",
                Company = company
            };

            var employees = new List<Employee>() { employee };
            //Arrange
            _mockEmployeeRepository.Setup(
                x => x.Get(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<Func<IQueryable<Employee>, IOrderedQueryable<Employee>>>(), It.IsAny<string>()))
                .Returns(employees)
                .Verifiable("Method should be called");
            _mockEmployeeRepository.Setup(
                x => x.Insert(It.IsAny<Employee>()))
                .Verifiable("Method should be called");
            _mockCompanyRepository.Setup(
                x => x.GetByID(It.IsAny<int>()))
                .Returns(company)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IEmployeeService)) as EmployeeService;

            //Act
            if (service != null)
            {
                var result = service.AddEmployee(employee.Name, employee.Surname, employee.Patronymic, employee.CompanyId, employee.Email);
                //Assert
                Assert.AreSame(employee, result);
            }
            else
            {
                throw new Exception();
            }
            _mockEmployeeRepository.Verify();
            _mockCompanyRepository.Verify();
        }

        [TestMethod()]
        public void DeleteEmployee()
        {
            var employee = new Employee()
            {
                ID = 1,
                Name = "Name",
                Surname = "Surname",
                Patronymic = "Patronimic",
                CompanyId = 1,
                Email = "Email"
            };

            var employees = new List<Employee>() { employee };
            //Arrange
            _mockEmployeeRepository.Setup(
                x => x.Get(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<Func<IQueryable<Employee>, IOrderedQueryable<Employee>>>(), It.IsAny<string>()))
                .Returns(employees)
                .Verifiable("Method should be called");
            _mockEmployeeRepository.Setup(
                x => x.Delete(It.IsAny<int>()))
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IEmployeeService)) as EmployeeService;

            //Act
            if (service != null)
            {
                var result = service.DeleteEmployee(It.IsAny<int>());
                //Assert
                Assert.IsTrue(result);
            }
            else
            {
                throw new Exception();
            }
            _mockEmployeeRepository.Verify();
        }

        [TestMethod()]
        public void UpdateEmployee()
        {
            var company = new Company()
            {
                ID = 1,
                Name = "Name"
            };

            var employee = new Employee()
            {
                ID = 1,
                Name = "Name",
                Surname = "Surname",
                Patronymic = "Patronimic",
                CompanyId = 1,
                Email = "Email",
                Company = new Company()
                {
                    ID = 2,
                    Name = "NewName"                
                }
            };

            //Arrange
            _mockCompanyRepository.Setup(
                x => x.GetByID(It.IsAny<int>()))
                .Returns(company)
                .Verifiable("Method should be called");
            _mockEmployeeRepository.Setup(
                x => x.Update(It.IsAny<Employee>()))
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IEmployeeService)) as EmployeeService;

            //Act
            if (service != null)
            {
                var result = service.UpdateEmployee(employee);
                //Assert
                Assert.AreEqual(employee.Name, result);
            }
            else
            {
                throw new Exception();
            }
            _mockEmployeeRepository.Verify();
            _mockCompanyRepository.Verify();
        }
    }
}
