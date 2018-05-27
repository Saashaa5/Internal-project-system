using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using HRSystemBLL.Contracts;
using HRSystemDAL.DAL;

namespace HRSystemWeb.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public JsonResult GetEmployees(string filter, string sortColumn, string sortDirection, int page, int pageSize)
        {
            var employees = _employeeService.GetEmployees(filter, sortColumn, sortDirection, page, pageSize);
            var jsonModel = JsonConvert.SerializeObject(employees, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(new { success = true, jsonModel }, JsonRequestBehavior.AllowGet);



        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult DeleteEmployee(int id)
        {
            var result = _employeeService.DeleteEmployee(id);
            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult UpdateEmployee(string newEmployee)
        {
            Employee employee = JsonConvert.DeserializeObject<Employee>(newEmployee);
            var employeeName = _employeeService.UpdateEmployee(employee);
            return Json(new { success = true, employeeName }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult AddEmployee(string name, string surname, string patronymic, string email, int companyId)
        {
            var employee = _employeeService.AddEmployee(name, surname, patronymic, companyId, email);
            if (employee != null)
            {
                var jsonModel = JsonConvert.SerializeObject(employee, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(new { success = true, jsonModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jsonModel = JsonConvert.SerializeObject("Такой сотрудник уже существует!");
                return Json(new { success = false, jsonModel }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}