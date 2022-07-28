using System.ComponentModel.DataAnnotations;

namespace EmployeePayroll_Ajax.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Emp_Id { get; set; }

        [Required(ErrorMessage = "Name Field is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender Field is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Department Filed is Required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Notes Filed is Required")]
        public string Notes { get; set; }

    }
}