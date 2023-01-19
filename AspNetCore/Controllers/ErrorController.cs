using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_AspNetCore.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404: ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found"; 
                    break;
            }
            return View("NotFound");
        }
    }
}
