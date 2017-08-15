using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult GetDepartments()
        {
            Department dept = new Department();
            EmployeeContext context = new EmployeeContext();
            List<Department> departments = context.departments.ToList();
            return View(departments);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
// Department -> Employee List -> Employee Detail
// Employee List - > Add Employee
// Employee Detail -> Update

//View to display all Employees
//Images for top 3 emp of the month with a marquee.