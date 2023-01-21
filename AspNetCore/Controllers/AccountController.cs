using EmployeeManagement_AspNetCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
           
          

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,loginViewModel.Password
                                                                 ,loginViewModel.RememberMe,false); // CreateAsync will hash the password of the user

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");

            }
           
            ModelState.AddModelError(string.Empty, "Invalid log in Attempt"); //These are displayed in the validation error in view 

            
            return View(loginViewModel);
        }
    }
}
