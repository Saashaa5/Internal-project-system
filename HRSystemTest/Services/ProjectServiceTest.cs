using HRSystemBLL.Contracts;
using HRSystemBLL.Services;
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
    public class ProjectServiceTest : UnityContainerTests
    {
        private readonly Mock<IGenericRepository<Project>> _mockProjectRepository = new Mock<IGenericRepository<Project>>();
        private readonly Mock<IGenericRepository<Employee>> _mockEmployeeRepository = new Mock<IGenericRepository<Employee>>();
        private readonly Mock<IGenericRepository<Company>> _mockCompanyRepository = new Mock<IGenericRepository<Company>>();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();

        [TestInitialize()]
        public override void InitializeTests()
        {
            _mockUnitOfWork.Setup(x => x.GetRepository<Project>())
                .Returns(_mockProjectRepository.Object).Verifiable("Method should be called");
            _mockUnitOfWork.Setup(x => x.GetRepository<Company>())
                .Returns(_mockCompanyRepository.Object).Verifiable("Method should be called");

            UnityContainer = new UnityContainer()
                .RegisterType<IProjectService, ProjectService>()
                .RegisterInstance(typeof(IUnitOfWork), _mockUnitOfWork.Object)
                .RegisterInstance(typeof(IGenericRepository<Project>), _mockProjectRepository.Object)
                .RegisterInstance(typeof(IGenericRepository<Employee>), _mockEmployeeRepository.Object)
                .RegisterInstance(typeof(IGenericRepository<Company>), _mockCompanyRepository.Object); ;
            ServiceLocator = new UnityServiceLocator(UnityContainer);
        }

        [TestMethod()]
        public void GetProject()
        {
            var project = new Project()
            {
                ID = 1,
                Name = "Name",                
            };
            //Arrange
            _mockProjectRepository.Setup(
                x => x.GetByID(It.IsAny<Int32>()))
                .Returns(project)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IProjectService)) as ProjectService;

            //Act
            if (service != null)
            {
                var result = service.GetProject(0);
                //Assert
                Assert.AreSame(project, result);
            }
            else
            {
                throw new Exception();
            }
            _mockProjectRepository.Verify();
        }

        [TestMethod()]
        public void GetProjects()
        {
            var projects = new List<Project>() { new Project{
                ID = 1,
                Name = "Name"
            } };
            //Arrange
            _mockProjectRepository.Setup(
                x => x.Get(It.IsAny<Expression<Func<Project, bool>>>(), It.IsAny<Func<IQueryable<Project>, IOrderedQueryable<Project>>>(), It.IsAny<string>()))
                .Returns(projects)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IProjectService)) as ProjectService;

            //Act
            if (service != null)
            {
                var result = service.GetProjects("", "", "", 1, 1);
                //Assert
                Assert.AreSame(projects.First(), result.First());
            }
            else
            {
                throw new Exception();
            }
            _mockProjectRepository.Verify();
        }

        [TestMethod()]
        public void AddProject()
        {
            var project = new Project()
            {
                ClientCompanyId = 1,
                ExecutorCompanyId = 2,
                Name = "Name",
                Comment = "Comment",
                StartDate = DateTime.Now,
                EndDate = DateTime.Today,
                Priority = 1
            };
            var company = new Company()
            {
                ID = 1,
                Name = "Name"
            };
            
            //Arrange
            _mockProjectRepository.Setup(
                x => x.Insert(It.IsAny<Project>()))
                .Verifiable("Method should be called");
            _mockCompanyRepository.Setup(
                x => x.GetByID(It.IsAny<int>()))
                .Returns(company)
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IProjectService)) as ProjectService;

            //Act
            if (service != null)
            {
                var result = service.AddProject(project.ClientCompanyId, project.ExecutorCompanyId, project.Name, project.Comment, project.StartDate, project.EndDate, project.Priority);
                //Assert
                Assert.AreEqual(project.Name, result.Name);
                Assert.AreEqual(project.Comment, result.Comment);
                Assert.AreEqual(project.StartDate, result.StartDate);
                Assert.AreEqual(project.EndDate, result.EndDate);
                Assert.AreEqual(project.Priority, result.Priority);
            }
            else
            {
                throw new Exception();
            }
            _mockProjectRepository.Verify();
            _mockCompanyRepository.Verify();
        }

        [TestMethod()]
        public void DeleteProject()
        {
            var project = new Project()
            {
                ID = 1,
                ClientCompanyId = 1,
                ExecutorCompanyId = 2,
                Name = "Name",
                Comment = "Comment",
                StartDate = DateTime.Now,
                EndDate = DateTime.Today,
                Priority = 1
            };
            //Arrange
            _mockProjectRepository.Setup(
                x => x.GetByID(It.IsAny<int>()))
                .Returns(project)
                .Verifiable("Method should be called");
            _mockProjectRepository.Setup(
                x => x.Delete(It.IsAny<int>()))
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IProjectService)) as ProjectService;

            //Act
            if (service != null)
            {
                var result = service.DeleteProject(It.IsAny<int>());
                //Assert
                Assert.IsTrue(result);
            }
            else
            {
                throw new Exception();
            }
            _mockProjectRepository.Verify();
        }

        [TestMethod()]
        public void UpdateProject()
        {
            var company = new Company()
            {
                ID = 1,
                Name = "Name"
            };

            var project = new Project()
            {
                ID = 1,
                Name = "Name"
            };

            //Arrange
            _mockCompanyRepository.Setup(
                x => x.GetByID(It.IsAny<int>()))
                .Returns(company)
                .Verifiable("Method should be called");
            _mockProjectRepository.Setup(
                x => x.Update(It.IsAny<Project>()))
                .Verifiable("Method should be called");
            var service = ServiceLocator.GetInstance(typeof(IProjectService)) as ProjectService;

            //Act
            if (service != null)
            {
                var result = service.UpdateProject(project);
                //Assert
                Assert.AreEqual(project, result);
            }
            else
            {
                throw new Exception();
            }
            _mockProjectRepository.Verify();
            _mockCompanyRepository.Verify();
        }
    }
}
