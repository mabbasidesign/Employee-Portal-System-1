using Employee_Portal_System_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_Portal_System_1.Controllers
{
    public class DeleteEmployeeController : Controller
    {

        public ActionResult Index()
        {
            var db = new EmEntities();

            var empList = db.Employees
                .Where(e => e.IsDeleted == false)
                .Select(x => new EmployeeViewModel
                {
                    EmployeeId = x.EmployeeId,
                    Name = x.Name,
                    Address = x.Address,
                    DepartmentName = x.Department.DepartmentName,
                    IsDeleted = x.IsDeleted,
                }).ToList();

            ViewBag.EmployeeList = empList;

            return View();
        }

        public JsonResult DeleteEmployee(int id)
        {
            var result = false;
            var db = new EmEntities();

            var em = db.Employees.SingleOrDefault(e => e.EmployeeId == id && e.IsDeleted == false);
            if (em != null)
            {
                em.IsDeleted = true;
                result = true;
                db.SaveChanges();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayEmployees()
        {
            var db = new EmEntities();

            var empList = db.Employees
                .Where(e => e.IsDeleted == false)
                .Select(x => new EmployeeViewModel
                {
                    EmployeeId = x.EmployeeId,
                    Name = x.Name,
                    Address = x.Address,
                    DepartmentName = x.Department.DepartmentName,
                    IsDeleted = x.IsDeleted,
                }).ToList();

            ViewBag.EmployeeList = empList;

            return PartialView("DisplayPartial");
        }

    }
}