using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using HRSystemBLL.Contracts;

namespace HRSystemWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: Company
        [HttpGet]
        public JsonResult DeleteCompany(int id)
        {
            var result = _companyService.DeleteCompany(id);
            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateCompany(int id, string name)
        {
            var company = _companyService.GetCompany(id);
            company.Name = name;
            _companyService.UpdateCompany(company);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult AddCompany(string name)
        {
            var company = _companyService.AddCompany(name);
            if (company != null)
            {
                var jsonModel = JsonConvert.SerializeObject(company);
                return Json(new { success = true, jsonModel }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jsonModel = "Такая компания уже существует!";
                return Json(new { success = false, jsonModel }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetCompanies(string filter, string sortColumn, string sortDirection, int page, int pageSize)
        {
            var companies = _companyService.GetCompanies(filter, sortColumn, sortDirection, page, pageSize);
            var jsonModel = JsonConvert.SerializeObject(companies, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(new { success = true, jsonModel }, JsonRequestBehavior.AllowGet);



        }
    }
}