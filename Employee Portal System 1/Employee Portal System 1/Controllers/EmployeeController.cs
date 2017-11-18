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
            em.EmployeeId = model.EmployeeId;
            em.Name = model.Name;
            em.Address = model.Address;
            em.DepartmentId = model.DepartmentId;

            var emList = db.Employees.Add(em);
            db.SaveChanges();



            return View();
        }
    }
}