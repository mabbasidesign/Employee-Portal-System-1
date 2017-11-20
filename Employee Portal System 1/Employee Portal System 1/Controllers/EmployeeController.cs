using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee_Portal_System_1.Models;

namespace Employee_Portal_System_1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(EmployeeViewModel model)
        {
            var db = new EmEntities();
            var depList = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(depList, "DepartmentId", "DepartmentName");

            var em = new Employee();
            //em.EmployeeId = model.EmployeeId;
            em.Name = model.Name;
            em.Address = model.Address;
            em.DepartmentId = model.DepartmentId;
            db.Employees.Add(em);
            //db.SaveChanges();

            var latestEmpId = em.EmployeeId;
            
            var site = new Site();
            site.SiteName = model.SiteName;
            db.Sites.Add(site);
            //db.SaveChanges();
            site.EmployeeId = latestEmpId;

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

            return View(model);
        }

        public ActionResult DeleteEmployee()
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
            if(em != null)
            {
                em.IsDeleted = true;
                result = true;
                db.SaveChanges();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}