using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement_AspNetCore.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }


        // this is when no route matches 
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404: ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    logger.LogWarning($"404 Error Occured. Path =  {statusCodeResult.OriginalPath}" + $" and Querystring {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error() 
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"This path {exceptionDetails.Path} threw an exception" + $"{exceptionDetails.Error}");
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;

            return View("Error");
        }


    }
}
