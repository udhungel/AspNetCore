using EmployeeManagement_AspNetCore.Interface;
using EmployeeManagement_AspNetCore.Models;
using EmployeeManagement_AspNetCore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                Employee = _employeeRepository.GetEmployee(id),
                PageTitle = "Employee Details PageTitle"
            };           
            
            return View(modelviewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {           
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExisitingPhotopath = employee.PhotoPath

            };

            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            Employee employee = _employeeRepository.GetEmployee(model.Id);
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Department = model.Department;
            if (model.Photo !=null)
            {
                if (model.ExisitingPhotopath !=null)
                {
                   string filePath = Path.Combine(hostingEnviroment.WebRootPath, "images", model.ExisitingPhotopath);
                   System.IO.File.Delete(filePath);

                }
                // it will return the unique filename that is saved in the images folder 
                employee.PhotoPath = ProcessUploadedFile(model);
            }        
          
            _employeeRepository.Update(employee);
            return RedirectToAction("index");

        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
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
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
            return View();
            string uniqueFileName = ProcessUploadedFile(model);
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
