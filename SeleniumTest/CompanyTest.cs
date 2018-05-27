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
    public class CompanyTest
    {
        private IWebDriver driver;

        public CompanyTest()
        {
        }

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost:8080";
        }

        [Test]
        public void AddCompanyTest()
        {
            var loginPage = new Login(driver);
            loginPage.LoginAs("asd", "qwerty");

            var name = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);

            AddCompany(name);
        }

        [Test]
        public void AddAndDeleteCompany()
        {
            var loginPage = new Login(driver);
            loginPage.LoginAs("asd", "qwerty");

            var name = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);

            var company = AddCompany(name);
            company.DeleteCompany(name);
            Thread.Sleep(2000);
            company.ConfirmSuccess();
            Thread.Sleep(2000);
            company.ConfirmSuccess();
            Thread.Sleep(1000);

            var companyNames = company.GetCompanies();

            var addedCompany = companyNames.FirstOrDefault(x => x.Text == name);

            Assert.IsNull(addedCompany);
        }

        private Company AddCompany(string name)
        {
            var addCompany = new Company(driver);
            Thread.Sleep(2000);
            addCompany.AddCompany(name);
            addCompany.ConfirmSuccess();

            var companyNames = addCompany.GetCompanies();

            var addedCompany = companyNames.FirstOrDefault(x => x.Text == name);

            Assert.IsNotNull(addedCompany);

            return addCompany;
        }
        
        [TearDown]
        public void Close()
        {
            driver.Close();
            driver.Dispose();
        }
    }
    
}
