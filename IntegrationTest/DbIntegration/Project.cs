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
    public class ProjectTest
    {
        private CompanySdk companySdk;
        public ProjectTest()
        {
            companySdk = new CompanySdk();
        }

        [Test]
        public async Task GetEmployeesIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
        [Test]
        public async Task DeleteEmployeesIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
        [Test]
        public async Task UpdateEmployeesIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
        [Test]
        public async Task AddEmployeesIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
        [Test]
        public async Task GetProjectIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
        [Test]
        public async Task AddProjectIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
        [Test]
        public async Task DeleteProjectIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
        [Test]
        public async Task UpdateProjectIntTest()
        {
            var companies = await companySdk.GetCompanies();

            Assert.IsTrue(companies.Any(x => x.Name == "yandex"));
        }
    }
}
