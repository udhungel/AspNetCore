using EmployeeManagement_AspNetCore.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; 

        public HomeController(IEmployeeRepository employeeRepository) 
        { 
            _employeeRepository = employeeRepository;

        }
        public string Index()
        {
           return _employeeRepository.GetEmployee(1).Name; 
        }
        public ViewResult Details()
        {
            var model = _employeeRepository.GetEmployee(1);
            return View(model);
        }
    }
}
