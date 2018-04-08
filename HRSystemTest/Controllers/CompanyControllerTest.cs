using HRSystemBLL.Contracts;
using HRSystemDAL.DAL;
using HRSystemWeb.Controllers;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using Tests.Common;
using Newtonsoft.Json;
using Microsoft.CSharp.RuntimeBinder;
using System.Reflection;

namespace HRSystemTest.Controllers
{
    [TestClass()]
    public class CompanyControllerTest : UnityContainerTests
    {
        private readonly Mock<ICompanyService> _mockCompanyService = new Mock<ICompanyService>();
        [TestInitialize()]
        public override void InitializeTests()
        {
            UnityContainer = new UnityContainer()
                .RegisterType<CompanyController>()
                .RegisterInstance(typeof(ICompanyService), _mockCompanyService.Object);
            ServiceLocator = new UnityServiceLocator(UnityContainer);
        }

        [TestMethod()]
        public void GetCompaniesTest()
        {
            //Arrange
            var companies = new List<Company>() { new Company{
                ID = 1,
                Name = "das",
                Employees = new List<Employee>(),
                ProjectsForClients = new List<Project>(),
                ProjectsForExecutors = new List<Project>()
            } };
            _mockCompanyService.Setup(x => x.GetCompanies(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(companies).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(CompanyController)) as CompanyController;

            //Act
            var result = controller.GetCompanies(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("jsonModel");

            var model =  JsonConvert.DeserializeObject<List<Company>>(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Count == companies.Count);

            _mockCompanyService.Verify();
        }

        [TestMethod()]
        public void AddCompaniesTest()
        {
            //Arrange
            var company = new Company{
                ID = 1,
                Name = "CompanyName",
                Employees = new List<Employee>(),
                ProjectsForClients = new List<Project>(),
                ProjectsForExecutors = new List<Project>()
            };
            _mockCompanyService.Setup(x => x.AddCompany(It.IsAny<string>()))
                .Returns(company).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(CompanyController)) as CompanyController;

            //Act
            var result = controller.AddCompany(company.Name) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("jsonModel");

            var model = JsonConvert.DeserializeObject<Company>(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Name == company.Name);

            _mockCompanyService.Verify();
        }
        [TestMethod()]
        public void DeleteCompanyTest()
        {
            _mockCompanyService.Setup(x => x.DeleteCompany(It.IsAny<int>()))
                .Returns(true).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(CompanyController)) as CompanyController;

            //Act
            var result = controller.DeleteCompany(It.IsAny<int>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("success");

            var model = Convert.ToBoolean(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model == true);

            _mockCompanyService.Verify();
        }
        [TestMethod()]
        public void UpdateCompanyTest()
        {
            var company = new Company
            {
                ID = 1,
                Name = "CompanyName",
                Employees = new List<Employee>(),
                ProjectsForClients = new List<Project>(),
                ProjectsForExecutors = new List<Project>()
            };
            _mockCompanyService.Setup(x => x.UpdateCompany(It.IsAny<Company>()))
                .Verifiable();

            _mockCompanyService.Setup(x => x.GetCompany(It.IsAny<int>()))
                .Returns(company).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(CompanyController)) as CompanyController;

            //Act
            var result = controller.UpdateCompany(It.IsAny<int>(), It.IsAny<string>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("success");

            var model = Convert.ToBoolean(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model == true);

            _mockCompanyService.Verify();
        }
    }
}
