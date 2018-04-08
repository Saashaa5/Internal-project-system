using HRSystemBLL.Contracts;
using HRSystemDAL.DAL;
using HRSystemWeb.Controllers;
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
    public class ProjectControllerTest : UnityContainerTests
    {
        private readonly Mock<IProjectService> _mockProjectService = new Mock<IProjectService>();
        [TestInitialize()]
        public override void InitializeTests()
        {
            UnityContainer = new UnityContainer()
                .RegisterType<ProjectController>()
                .RegisterInstance(typeof(IProjectService), _mockProjectService.Object);
            ServiceLocator = new UnityServiceLocator(UnityContainer);
        }

        [TestMethod()]
        public void GetProjectsTest()
        {
            //Arrange
            var projects = new List<Project>() { new Project{
                ID = 1,
                Name = "das",
                ClientCompanyId = 1,
                ExecutorCompanyId = 1,
                Comment = "Comment",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Priority = 1
            } };
            _mockProjectService.Setup(x => x.GetProjects(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(projects).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(ProjectController)) as ProjectController;

            //Act
            var result = controller.GetProjects(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("jsonModel");

            var model = JsonConvert.DeserializeObject<List<Project>>(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Count == projects.Count);

            _mockProjectService.Verify();
        }

        [TestMethod()]
        public void AddProjectTest()
        {
            //Arrange
            var project = new Project
            {
                ID = 1,
                Name = "ProjectName",
                ClientCompanyId = 2,
                ExecutorCompanyId = 2,
                Comment = "Comments",
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Priority = 1
            };
            _mockProjectService.Setup(x => x.AddProject(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<decimal>()))
                .Returns(project).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(ProjectController)) as ProjectController;

            //Act
            var result = controller.AddProject(project.Name, project.Priority, project.StartDate, project.EndDate, project.ClientCompanyId, project.Comment, project.ExecutorCompanyId) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("jsonModel");

            var model = JsonConvert.DeserializeObject<Project>(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Name == project.Name);

            _mockProjectService.Verify();
        }

        [TestMethod()]
        public void GetEmployeesTest()
        {
            //Arrange
            var employees = new List<int>() { 
                1, 2, 5, 20
             };
            _mockProjectService.Setup(x => x.GetEmployeesToProjects(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(employees).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(ProjectController)) as ProjectController;
            
            //Act
            var result = controller.GetEmployees(It.IsAny<int>(), It.IsAny<bool>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("employees");

            var model = property.GetValue(result.Data, null) as List<int>;
            
            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Count() == employees.Count());

            _mockProjectService.Verify();
        }
        [TestMethod()]
        public void DeleteProjectTest()
        {
            _mockProjectService.Setup(x => x.DeleteProject(It.IsAny<int>()))
                .Returns(true).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(ProjectController)) as ProjectController;

            //Act
            var result = controller.DeleteProject(It.IsAny<int>()) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("success");
            
            var model = Convert.ToBoolean(property.GetValue(result.Data, null).ToString());

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model == true);

            _mockProjectService.Verify();
        }
        [TestMethod()]
        public void UpdateProjectTest()
        {
            var project = new Project
            {
                ID = 1,
                Name = "ProjectName",
                ClientCompanyId = 2,
                ExecutorCompanyId = 2,
                Comment = "Comments",
                StartDate = DateTime.Today,
                EndDate = DateTime.Now,
                Priority = 1
            };
            _mockProjectService.Setup(x => x.UpdateProject(It.IsAny<Project>()))
                .Returns(project).Verifiable();

            var controller = ServiceLocator.GetInstance(typeof(ProjectController)) as ProjectController;

            //Act
            var result = controller.UpdateProject(JsonConvert.SerializeObject(project)) as JsonResult;

            PropertyInfo property = result.Data.GetType().GetProperty("updatedProject");
            
            var model = property.GetValue(result.Data, null) as Project;

            Assert.IsNotNull(result.Data);
            Assert.IsTrue(model.Name == project.Name);
            Assert.IsTrue(model.Priority == project.Priority);
            Assert.IsTrue(model.ClientCompanyId == project.ClientCompanyId);

            _mockProjectService.Verify();
        }
        
    }
}

