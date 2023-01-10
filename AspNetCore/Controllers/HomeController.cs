using EmployeeManagement_AspNetCore.Interface;
using EmployeeManagement_AspNetCore.Models;
using EmployeeManagement_AspNetCore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeManagement_AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnviroment;

        public HomeController(IEmployeeRepository employeeRepository , IHostingEnvironment hostingEnviroment  ) 
        { 
            _employeeRepository = employeeRepository;
            this.hostingEnviroment = hostingEnviroment;
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
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            return View();
            string uniqueFileName = null;
            if (model.Photo !=null)
            {
                // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
              string uploadFolder = Path.Combine(hostingEnviroment.WebRootPath, "images");
                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
              string filePath = Path.Combine(uploadFolder, uniqueFileName);
                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create)); 
            }
            Employee newEmployee = new Employee
            {
                Name = model.Name,
                Email = model.Name,
                Department = model.Department,
                // Store the file name in PhotoPath property of the employee object
                // which gets saved to the Employees database table
                PhotoPath = uniqueFileName
            };
            _employeeRepository.Add(newEmployee);
            return RedirectToAction("details", new { id = newEmployee.Id });
            
        }

    }
}
