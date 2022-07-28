using EmployeePayroll_Ajax.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePayroll_Ajax.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeePayrollContext employeeContext;
        public EmployeeController(EmployeePayrollContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.employeeContext.Employee.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> AddEmployee(int id = 0)
        {
            if (id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = await this.employeeContext.Employee.FindAsync(id);
                if (emp == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(emp);
                }
            }
        }  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(int Id, [Bind("Emp_Id,Name,Gender,Department,Notes")] EmployeeModel emps)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (Id == 0)
                {
                    employeeContext.Employee.Add(emps);
                    await employeeContext.SaveChangesAsync();
                }
                // Update
                else
                {
                    try
                    {
                        employeeContext.Employee.Update(emps);
                        await employeeContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeModelExists(emps.Emp_Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;


                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", this.employeeContext.Employee.ToListAsync()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", emps) });
        }
        private bool EmployeeModelExists(int id)
        {
            return this.employeeContext.Employee.Any(x => x.Emp_Id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await this.employeeContext.Employee.FirstOrDefaultAsync(x => x.Emp_Id == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await employeeContext.Employee.FindAsync(id);
            employeeContext.Employee.Remove(emp);
            await employeeContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", employeeContext.Employee.ToList()) });
        }


        public async Task<IActionResult> UpdateEmployee(int? id)
        {
            var emp = await employeeContext.Employee.FindAsync(id);
            return View(emp);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee([Bind("Emp_Id,Name,Gender,Department,Notes")] EmployeeModel emps)
        {
            if (ModelState.IsValid)
            {
                employeeContext.Employee.Update(emps);
                await employeeContext.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", employeeContext.Employee.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "UpdateEmployee", emps) });

        }

    }

}
