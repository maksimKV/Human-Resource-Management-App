using Human_Resource_Management_App.Entities;

namespace Human_Resource_Management_App.Helpers
{
    public interface IEmployeeHelper
    {
        public Employee CreateEmployeeEntity(string firstName, string lastName, string department, decimal salary);
        public bool CheckEmployeeID(int id);
    }
    public class EmployeeHelper : IEmployeeHelper
    {
        public Employee CreateEmployeeEntity(string firstName, string lastName, string department, decimal salary)
        {
            if(firstName == null ||  lastName == null || department == null || salary == 0)
                return null;

            Employee newEmployee = new Employee();

            newEmployee.FirstName = firstName; 
            newEmployee.LastName = lastName;
            newEmployee.Department = department;
            newEmployee.Salary = salary;

            return newEmployee;
        }

        public bool CheckEmployeeID(int id)
        {
            if(id == 0)
            {
                return false;
            }

            return true;
        }
    }
}
