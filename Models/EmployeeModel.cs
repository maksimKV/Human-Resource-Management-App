using System.ComponentModel.DataAnnotations;

namespace Human_Resource_Management_App.Models
{
    public class EmployeeModel : EmployeeBaseModel
    {
        [Required]
        public int Id { get; set; }
    }
}
