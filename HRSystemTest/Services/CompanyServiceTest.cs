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
    public class CompanyServiceTest : UnityContainerTests
    {
        private readonly Mock<IGenericRepository<Company>> _mockCompanyRepository = new Mock<IGenericRepository<Company>>();
        private readonly Mock<IGenericRepository<Employee>> _mockEmployeeRepository = new Mock<IGenericRepository<Employee>>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        [TestInitialize()]
        public override void InitializeTests()
        {
            _mockUnitOfWork.Setup(x => x.GetRepository<Employee>())
                .Returns(_mockEmployeeRepository.Object).Verifiable("Method should be called");
            _mockUnitOfWork.Setup(x => x.GetRepository<Company>())
                .Returns(_mockCompanyRepository.Object).Verifiable("Method should be called");
            
            UnityContainer = new UnityContainer()
                .RegisterType<ICompanyService, CompanyService>()
                .RegisterInstance(typeof(IUnitOfWork), _mockUnitOfWork.Object)
                .RegisterInstance(typeof(IGenericRepository<Employee>), _mockEmployeeRepository.Object)
                .RegisterInstance(typeof(IGenericRepository<Company>), _mockCompanyRepository.Object);
            ServiceLocator = new UnityServiceLocator(UnityContainer);
        }

        [TestMethod()]
        public void GetCompany()
        {
            var company = new Company()
            {
                ID = 1,
                Name = "Name"
            };
            //Arrange
            _mockCompanyRepository.Setup(
                x => x.GetByID(It.IsAny<Int32>()))
                .Returns(company)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(ICompanyService)) as CompanyService;

            //Act
            if (service != null)
            {
                var result = service.GetCompany(0);

                //Assert
                Assert.AreSame(company, result);
            }
            else
            {
                throw new Exception();
            }
            _mockCompanyRepository.Verify();
        }

        [TestMethod()]
        public void GetCompanies()
        {
            var companies = new List<Company>() { new Company{
                ID = 1,
                Name = "Name"
            } };
            //Arrange
            _mockCompanyRepository.Setup(
                x => x.Get(null, null, ""))
                .Returns(companies)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(ICompanyService)) as CompanyService;

            //Act
            if (service != null)
            {
                var result = service.GetCompanies("","","",1,1);
                //Assert
                Assert.AreSame(companies.First(), result.First());
            }
            else
            {
                throw new Exception();
            }
            _mockCompanyRepository.Verify();
        }

        [TestMethod()]
        public void AddCompany()
        {
            var companies = new List<Company>() { new Company{
                ID = 1,
                Name = "Name"
            } };

            var company = new Company() {
                ID = 1,
                Name = "Name"
            };
            //Arrange
            _mockCompanyRepository.SetupSequence(
                x => x.Get(null, null, ""))
                .Returns(companies);
            _mockCompanyRepository.Setup(
                x => x.Insert(It.IsAny<Company>()))
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(ICompanyService)) as CompanyService;

            //Act
            if (service != null)
            {
                var result = service.AddCompany("");
                //Assert
                Assert.AreSame(null, result);
            }
            else
            {
                throw new Exception();
            }
            _mockCompanyRepository.Verify();
        }

        [TestMethod()]
        public void DeleteCompany()
        {            
            //Arrange
            _mockEmployeeRepository.Setup(
                x => x.Get(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<Func<IQueryable<Employee>, IOrderedQueryable<Employee>>>(), It.IsAny<string>()))
                .Returns(new List<Employee>())
                .Verifiable("Method should be called");
            _mockCompanyRepository.Setup(
                x => x.Delete(It.IsAny<int>()))
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(ICompanyService)) as CompanyService;

            //Act
            if (service != null)
            {
                var result = service.DeleteCompany(It.IsAny<int>());
                //Assert
                Assert.IsTrue(result);
            }
            else
            {
                throw new Exception();
            }
            _mockCompanyRepository.Verify();
        }

        [TestMethod()]
        public void DeleteUnExistingCompany()
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
            var service = ServiceLocator.GetInstance(typeof(ICompanyService)) as CompanyService;

            //Act
            if (service != null)
            {
                var result = service.DeleteCompany(It.IsAny<int>());
                //Assert
                Assert.IsFalse(result);
            }
            else
            {
                throw new Exception();
            }
            _mockEmployeeRepository.Verify();
        }

        [TestMethod()]
        public void UpdateCompany()
        {
            var company = new Company()
            {
                ID = 1,
                Name = "Name"
            };
            
            //Arrange
            _mockCompanyRepository.Setup(
                x => x.Update(It.IsAny<Company>()))
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(ICompanyService)) as CompanyService;

            //Act
            if (service != null)
            {
                service.UpdateCompany(company);
            }
            else
            {
                throw new Exception();
            }
            _mockCompanyRepository.Verify();
        }
    }
}
