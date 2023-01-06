using EmployeeManagement_AspNetCore.Interface;
using EmployeeManagement_AspNetCore.ViewModel;
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
            HomeDetailsViewModel modelviewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee Details PageTitle"
            };           
            
            return View(modelviewModel);
        }

    }
}
