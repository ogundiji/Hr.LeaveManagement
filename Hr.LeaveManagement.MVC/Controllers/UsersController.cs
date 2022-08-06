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
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authenticationServices.Authenticate(login.Email, login.Password);
            if (isLoggedIn)
                return LocalRedirect(returnUrl);

            ModelState.AddModelError("", "Log in attempt failed. please try again");
            return View(login);
        }
    }
}
