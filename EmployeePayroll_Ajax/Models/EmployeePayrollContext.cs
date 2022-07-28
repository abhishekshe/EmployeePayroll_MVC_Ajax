using Microsoft.EntityFrameworkCore;

namespace EmployeePayroll_Ajax.Models
{
    public class EmployeePayrollContext : DbContext
    {


        public EmployeePayrollContext(DbContextOptions<EmployeePayrollContext> options) : base(options)
        {

        }


        public DbSet<EmployeeModel> Employee { get; set; }


    }
}
