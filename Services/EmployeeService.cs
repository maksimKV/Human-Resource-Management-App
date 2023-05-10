using Human_Resource_Management_App.Context;
using Human_Resource_Management_App.Entities;

namespace Human_Resource_Management_App.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Task<Employee> GetEmployee(int Id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(int employeeId, Employee employee);
        Task<Employee> DeleteEmployee(int Id);
    }
    public class EmployeeService : IEmployeeService
    {
        private DataContext _context;
        private List<Employee> _employees;

        public EmployeeService(DataContext context)
        {
            _context = context;
            _employees = context.Employees.ToList();
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            Employee employee = await Task.Run(() => _employees.SingleOrDefault(x => x.Id == Id));

            // return null if employee not found
            if (employee == null)
                return null;

            return employee;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            Employee newEmployee = await Task.Run(() => {
                _context.Employees.Add(employee);

                _context.SaveChanges();

                return _context.Employees.First(x => x.Id == employee.Id);
            });

            // return null if newEmployee not found
            if (newEmployee == null)
                return null;

            _employees.Add(newEmployee);

            return newEmployee;
        }

        public async Task<Employee> UpdateEmployee(int employeeId, Employee employee)
        {
            Employee updatedEmployee = await Task.Run(() => {
                Employee currentEmployee = _context.Employees.First(x => x.Id == employeeId);

                // return null if newEmployee not found
                if (currentEmployee == null)
                    return null;

                bool employeeToBeUpdated = false;

                switch (true)
                {
                    case true when (currentEmployee.FirstName != employee.FirstName):
                        currentEmployee.FirstName = employee.FirstName;
                        employeeToBeUpdated = true;
                        break;

                    case true when (currentEmployee.LastName != employee.LastName):
                        currentEmployee.LastName = employee.LastName;
                        employeeToBeUpdated = true;
                        break;

                    case true when (currentEmployee.Department != employee.Department):
                        currentEmployee.Department = employee.Department;
                        employeeToBeUpdated = true;
                        break;

                    case true when (currentEmployee.Salary != employee.Salary):
                        currentEmployee.Salary = employee.Salary;
                        employeeToBeUpdated = true;
                        break;

                    default:
                        break;
                }

                if(employeeToBeUpdated)
                {
                    _context.SaveChanges();

                    Employee updatedEmployee = _context.Employees.First(x => x.Id == employee.Id);

                    return updatedEmployee;
                }

                // return null if there was nothing to update
                return null;
            });

            // return null if employee not found
            if (updatedEmployee == null)
                return null;

            return updatedEmployee;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            Employee deletedEmployee = await Task.Run(() => {
                Employee employToDelete = _context.Employees.First<Employee>(x => x.Id == Id);

                // return null if employToDelete not found
                if (employToDelete == null)
                    return null;

                _context.Employees.Remove(employToDelete);

                _context.SaveChanges();

                return employToDelete;
            });

            // return null if deletedEmployee not found
            if (deletedEmployee == null)
                return null;

            return deletedEmployee;
        }
    }
}
