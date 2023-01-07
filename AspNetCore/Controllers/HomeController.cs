using EmployeeManagement_AspNetCore.Interface;
using EmployeeManagement_AspNetCore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagement_AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository; 

        public HomeController(IEmployeeRepository employeeRepository) 
        { 
            _employeeRepository = employeeRepository;

        }
        public ViewResult Index()
        {
            var result =  _employeeRepository.GetAllEmployee();
            return View(result);
            
        }
        public ViewResult Details(int id)
        {
            HomeDetailsViewModel modelviewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id),
                PageTitle = "Employee Details PageTitle"
            };           
            
            return View(modelviewModel);
        }

    }
}
