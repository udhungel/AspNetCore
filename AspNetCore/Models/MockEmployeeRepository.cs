using EmployeeManagement_AspNetCore.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Xml.Linq;

namespace EmployeeManagement_AspNetCore.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                    new Employee()    { Id = 1, Name = "Mary", Department = "HR", Email = "mary@gmail.com" },
                    new Employee()    { Id = 2, Name = "John", Department = "HR", Email = "John@gmail.com" },
                    new Employee()    { Id = 3, Name = "Sam", Department = "HR", Email = "Sam@gmail.com" },
                    new Employee()    { Id = 4, Name = "Joe", Department = "HR", Email = "Joe@gmail.com" },
            };
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;


        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.Where(x=>x.Id==id).FirstOrDefault();
        }
    }
}
