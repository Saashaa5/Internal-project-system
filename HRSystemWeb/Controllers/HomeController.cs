using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using HRSystemBLL.Contracts;
using HRSystemDAL.DAL;
using HRSystemDAL.UnitOfWork;

namespace HRSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult EmployeeModalDialog()
        {
           

            return View("EditEmployeeTemplate");
        }
        public ActionResult ProjectModalDialog()
        {


            return View("EditProjectTemplate");
        }

        public ActionResult AddProjectEmployees()
        {


            return View("ManageProjectToEmployeesTemplate");
        }

    }
}