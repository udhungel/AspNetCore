using EmployeeManagement_AspNetCore.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EmployeeManagement_AspNetCore.Controllers
{   
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            //if valid create new identity user object 
            var user = new IdentityUser { UserName = registerViewModel.Email, Email = registerViewModel.Email };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password); // CreateAsync will hash the password of the user

            if (result.Succeeded) 
            { 
                await _signInManager.SignInAsync(user, isPersistent: false); // second IsPersistent false is session cookie which is lost when browser is closed
                return RedirectToAction("Index", "Home");
            }
            // if succeeded is false we want to loop through each error
            foreach (var error in result.Errors)
            {
               ModelState.AddModelError(string.Empty, error.Description); //These are displayed in the validation error in view 

            }
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
