using EmployeeManagement_AspNetCore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository) 
        { 
            _employeeRepository = employeeRepository;

        }
        public string Index()
        {
           return _employeeRepository.GetEmployee(1).Name; 
        }
    }
}
