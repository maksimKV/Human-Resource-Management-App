using System.ComponentModel.DataAnnotations;

namespace Human_Resource_Management_App.Models
{
    public class EmployeeBaseModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
