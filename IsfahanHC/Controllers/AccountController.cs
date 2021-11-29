using IsfahanHC.Data.Repository;
using IsfahanHC.Models.DataModels;
using IsfahanHC.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IsfahanHC.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository _users;
        public AccountController(IUserRepository users)
        {
            _users = users;
        }

        [Route("/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var userError = _users.RegisterNewUser(register);
            if (userError == UserErrorType.DuplicateUserName)
            {
                ModelState.AddModelError("UserName", "این نام کاربری در سایت موجود است.");
                return View(register);
            }
            else if (userError == UserErrorType.DuplicateEmail)
            {
                ModelState.AddModelError("Email", "این ایمیل در سایت موجود است.");
                return View(register);
            }

            return View("SuccessRegister", register);
        }
        #endregion
        #region Log
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = _users.GetUserByLogin(login.UserName, login.Password);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "اطلاعات وارد شده صحیح نمی باشد");
                return View(login);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Email", user.Email),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
                new Claim("IsSeller", (user.Seller != null).ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }

        public IActionResult Logout()
        {
            User.FindFirstValue("");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        #endregion
    }
}
