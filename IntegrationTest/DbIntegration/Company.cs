using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HRApiSdk;
using HRSystemDAL.DAL;
using Newtonsoft.Json;
using NUnit.Framework;
using Console = System.Console;

namespace IntegrationTest.DbIntegration
{
    [TestFixture]
    public class CompanyTest
    {
        private CompanySdk companySdk;
        
        public CompanyTest()
        {
            companySdk = new CompanySdk();
        }

        [Test]
        public async Task GetCompaniesIntTest()
        {
            var companies = await companySdk.GetCompanies();
            
            Assert.IsTrue(companies.Any(x => x.Name == "yandex1"));
        }
        [Test]
        public async Task AddCompanyIntTest()
        {
            var companyName = "Apple";
            var companies = await companySdk.GetCompanies();
            if (companies.Any(x => x.Name == companyName))
            {
                await companySdk.DeleteCompany(companies.FirstOrDefault(x => x.Name == companyName).ID);
            }
            var company = await companySdk.AddCompany(companyName);

            Assert.IsTrue(company.ID != 0);
            Assert.AreEqual(company.Name, companyName);
        }

        [Test]
        public async Task DeleteCompanyIntTest()
        {
            var company = await companySdk.AddCompany("Xiaomi");
            var isSuccess = await companySdk.DeleteCompany(company.ID);

            Assert.IsTrue(isSuccess);
        }
        [Test]
        public async Task UpdateCompaniesIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
    }
}
