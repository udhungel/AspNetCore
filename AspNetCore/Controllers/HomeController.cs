using EmployeeManagement_AspNetCore.Interface;
using EmployeeManagement_AspNetCore.Models;
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
                Employee = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee Details PageTitle"
            };           
            
            return View(modelviewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(Employee employee)
        {
           Employee newEmployee =  _employeeRepository.Add(employee);
            return RedirectToAction("details", new { id = newEmployee.Id });           
        }

    }
}
