using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string ssn)
        {
            var user = await _userService.GetAllowedUser(ssn);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }

            HttpContext.Session.SetString("username", "12");


            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Ssn));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

            //foreach (var role in user.Roles)
            //{
            //    // identity.AddClaim(new Claim(ClaimTypes.Role, role.Role));
            //}

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "CodeBankViewModels");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }

    public interface IUserService
    {
        Task<User> GetAllowedUser(string ssn);
    }

    public class UserService : IUserService  {

        public Task<User> GetAllowedUser(string ssn)
        {
            return Task.FromResult(new User(){FirstName = "zawar", LastName = "akram", Ssn = "123"});

        }
    }

    public class User
    {
        public string Ssn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
    }
}
