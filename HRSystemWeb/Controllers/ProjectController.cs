using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using HRSystemBLL.Contracts;
using HRSystemDAL.DAL;

namespace HRSystemWeb.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        private IProjectService projectService;

        public ProjectController(IProjectService _projectService)
        {
            projectService = _projectService;
        }

        [HttpGet]
        public JsonResult GetProjects(string filter, string sortColumn, string sortDirection, int page, int pageSize)
        {
            var projects = projectService.GetProjects(filter, sortColumn, sortDirection, page, pageSize);
            var jsonModel = JsonConvert.SerializeObject(projects, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(new { success = true, jsonModel }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult AddEmployees(int id, int[] employees, int chief)
        {
            if (employees.Contains(chief))
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            projectService.AddEmployees(id, employees, chief);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetEmployees(int projectId, bool isChief)
        {
            var employees = projectService.GetEmployeesToProjects(projectId, isChief);
            return Json(new { success = true, employees }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateProject(string projectstr)
        {
            Project project = JsonConvert.DeserializeObject<Project>(projectstr);
            var updatedProject = projectService.UpdateProject(project);
            return Json(new { success = true, updatedProject }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult DeleteProject(int id)
        {
            var result = projectService.DeleteProject(id);
            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AddProject(string name, decimal priority, DateTime startDate, DateTime endDate, int clientCompanyId, string comment, int executorCompanyId)
        {
            var project = projectService.AddProject(clientCompanyId, executorCompanyId, name, comment, startDate, endDate, priority);
            if (project != null)
            {
                var jsonModel = JsonConvert.SerializeObject(project, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(new { success = true, jsonModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jsonModel = JsonConvert.SerializeObject("Такой проект уже существует!");
                return Json(new { success = false, jsonModel }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}