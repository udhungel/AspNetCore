using EmployeeManagement_AspNetCore.Interface;
using System.Collections.Generic;
using System.Linq;


namespace EmployeeManagement_AspNetCore.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                    new Employee()    { Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@gmail.com" },
                    new Employee()    { Id = 2, Name = "John", Department = Dept.Payroll, Email = "John@gmail.com" },
                    new Employee()    { Id = 3, Name = "Sam", Department = Dept.IT, Email = "Sam@gmail.com" },
                    new Employee()    { Id = 4, Name = "Joe", Department = Dept.HR, Email = "Joe@gmail.com" },
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(x => x.Id) + 1;
           _employeeList.Add(employee);
            return employee;
        }       

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.Where(x=>x.Id==id).FirstOrDefault();
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Department = employeeChanges.Department;
                    employee.Email = employeeChanges.Email;           

            }
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(x=>x.Id==id);
            if (employee !=null)
            {
                _employeeList.Remove(employee); 

            }
            return employee;
        }

    }
}
