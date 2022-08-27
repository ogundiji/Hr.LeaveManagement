using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticationServices _authenticationServices;

        public UsersController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }
        public IActionResult Login(string returnUrl=null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");
                var isLoggedIn = await _authenticationServices.Authenticate(login.Email, login.Password);
                if (isLoggedIn)
                    return LocalRedirect(returnUrl);

            }

            ModelState.AddModelError("", "Log in attempt failed. please try again");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registration)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                var isCreated = await _authenticationServices.Register(registration);
                if (isCreated)
                    return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError("", "Registration attempt failed please try again");
            return View(registration);
        }
    }
}
