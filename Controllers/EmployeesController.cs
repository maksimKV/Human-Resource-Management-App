using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Human_Resource_Management_App.Services;
using Human_Resource_Management_App.Models;
using Human_Resource_Management_App.Helpers;
using Human_Resource_Management_App.Entities;

namespace Human_Resource_Management_App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IAdminService _adminService;
        private IEmployeeService _employeeService;
        private IEmployeeHelper _employeeHelper;

        public EmployeeController(IAdminService adminService, IEmployeeService employeeService)
        {
            _adminService = adminService;
            _employeeService = employeeService;
            _employeeHelper = new EmployeeHelper(); 
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var admin = await _adminService.Authenticate(model.Email, model.Password);

            if (admin == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(admin);
        }

        [AllowAnonymous]
        [HttpGet("RetrieveEmployees")]
        public IActionResult RetrieveEmployees()
        {
            List<Employee> employees = _employeeService.GetEmployees();

            return Ok(employees);
        }

        [AllowAnonymous]
        [HttpGet("RetrieveEmployee/{id}")]
        public async Task<IActionResult> RetrieveEmployee(int Id)
        {
            bool idCheck = _employeeHelper.CheckEmployeeID(Id);
            if (!idCheck)
                return BadRequest(new { message = "Id value is incorrect" });

            Employee employee = await _employeeService.GetEmployee(Id);

            return Ok(employee);
        }

        [AllowAnonymous]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeBaseModel model)
        {
            Employee newEmployee = _employeeHelper.CreateEmployeeEntity(model.FirstName, model.LastName, model.Department, model.Salary);

            Employee employee = await _employeeService.AddEmployee(newEmployee);
            if (employee == null)
                return BadRequest(new { message = "Could not create new employee" });

            return Ok(employee);
        }

        [AllowAnonymous]
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeModel model)
        {
            Employee currentEmployee = _employeeHelper.CreateEmployeeEntity(model.FirstName, model.LastName, model.Department, model.Salary);

            Employee employee = await _employeeService.UpdateEmployee(model.Id, currentEmployee);
            if (employee == null)
                return BadRequest(new { message = "Could not update employee" });

            return Ok(employee);
        }

        [AllowAnonymous]
        [HttpDelete("RemoveEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody] IDModel model)
        {
            Employee employee = await _employeeService.DeleteEmployee(model.Id);
            if (employee == null)
                return BadRequest(new { message = "Could not remove employee" });

            return Ok(employee);
        }

    }
}
