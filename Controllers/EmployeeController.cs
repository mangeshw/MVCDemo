using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;
using System.Data.SqlClient;
using System.Data;

namespace MVCDemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult GetCountries()
        {
            ViewBag.Countries = new List<string>() { "India", "USA", "Australia", "Germany" };
            return View();
        }

        public ActionResult GetDetails()
        {
            EmployeeContext context = new EmployeeContext();

            return View(context.employees);
        }

        public ActionResult GetEmployeeList(string departmentID)
        {
            int deptID = string.IsNullOrEmpty(departmentID) ? 1 : Convert.ToInt32(departmentID);
            EmployeeContext context = new EmployeeContext();
            List<Employee> employees = context.employees.Where(emp => emp.DepartmentID == deptID).ToList();
            return View(employees);
        }

        public ActionResult GetEmployeeDetail(string id)
        {
            int empID = string.IsNullOrEmpty(id) ? 1 : Convert.ToInt32(id);
            Employee employee = new Employee();
            EmployeeContext context = new EmployeeContext();
            employee = context.employees.FirstOrDefault(x => x.EmployeeID == empID);

            return View(employee);
        }

        [HttpGet]
        public ActionResult AddEmployee(string EmployeeID)
        {
            int empID = string.IsNullOrEmpty(EmployeeID) ? 0 : Convert.ToInt32(EmployeeID);
            EmployeeContext context = new EmployeeContext();
            Employee emp = context.employees.FirstOrDefault(x => x.EmployeeID == empID);
            
            return View(emp);
        }

        [HttpPost]
        [ActionName("AddEmployee")]
        public ActionResult AddEmployee_Post(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Name = Request.Form["txtName"];                
                EmployeeContext context = new EmployeeContext();
                employee.EmployeeID = context.employees.Max(x => x.EmployeeID) + 1;
                context.employees.Add(employee);
                return RedirectToAction("GetDepartments", "Department");
            }
            
            return View();
        }

        public ActionResult ViewAllEmployee()
        {
            EmployeeContext context = new EmployeeContext();
            List<Employee> employees = context.employees.ToList();
            return View("GetDetails", employees);
        }
    }
}


#region Commented Code
/*
string sql = @"INSERT INTO dbo.employees (name, gender, DepartmentID, City) VALUES (@pname, @pgender, @pDepartmentID, @pCity)";
    SqlParameter[] parameters = new SqlParameter[4];
    parameters[0] = new SqlParameter();
    parameters[0].DbType = DbType.String;
    parameters[0].ParameterName = "pname";
    parameters[0].Value = employee.Name;

    parameters[1] = new SqlParameter();
    parameters[1].DbType = DbType.String;
    parameters[1].ParameterName = "pgender";
    parameters[1].Value = employee.Gender;

    parameters[2] = new SqlParameter();
    parameters[2].DbType = DbType.Int32;
    parameters[2].ParameterName = "pDepartmentID";
    parameters[2].Value = employee.DepartmentID;

    parameters[3] = new SqlParameter();
    parameters[3].DbType = DbType.String;
    parameters[3].ParameterName = "pCity";
    parameters[3].Value = employee.City;

    context.Database.ExecuteSqlCommand(sql, parameters);
    return View();
*/
#endregion Commented Code
