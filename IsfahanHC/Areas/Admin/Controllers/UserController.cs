using IsfahanHC.Data.Repository;
using IsfahanHC.Models.ViewModels;
using IsfahanHC.Models.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        IUserRepository _userRepository;
        IEditTicketRepository _editTicket;
        public UserController(IUserRepository user , IEditTicketRepository editTicket)
        {
            _userRepository = user;
            _editTicket = editTicket;
        }
        [Route("[area]/Users")]
        public IActionResult Index()
        {
            var users = _userRepository.GetUsers();
            if (users == null)
                users = new List<UserViewModel>();

            return View(users);
        }

        [Route("[area]/Users/{filter}")]
        public IActionResult Filter(string filter)
        {
            if(filter.ToLower() == "admins")
            {
                return View("index", _userRepository.GetUsersWhere(u => u.IsAdmin));
            }else if(filter.ToLower()== "sellers")
            {
                return View("index", _userRepository.GetUsersWhere(u => u.IsSeller));
            }else if(filter.ToLower() == "users")
            {
                return View("index", _userRepository.GetUsersWhere(u => !u.IsSeller && !u.IsAdmin));
            }

            return View("index", _userRepository
                .GetUsersWhere(u => u.Email.Contains(filter)|| u.UserName.Contains(filter)));
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            var error = _userRepository.RegisterNewUser(register);
            if (error != UserErrorType.non)
            {
                SetUserError(error);

                return View(register);
            }

            return RedirectToAction("index");
        }

        [Route("[area]/RemoveUserDetail/{userName?}")]
        public IActionResult RemoveUser(string userName)
        {
            var user = _userRepository.GetUser(userName);
            return View(user);
        }

        
        public IActionResult Remove(string userName)
        {
            _userRepository.RemoveUser(userName);
            return RedirectToAction("index");
        }

        public IActionResult Edit(string userName)
        {
            var user = _userRepository.GetUser(userName);
            if (user == null)
                return NotFound();

            var reg = new RegisterViewModel
            {
                Email = user.Email,
                UserName = user.UserName,
                UserCode = _editTicket.CreateTicket(5d,_userRepository.GetUserId(userName).ToString())
            };

            return View(reg);
        }

        [HttpPost]
        public IActionResult Edit(RegisterViewModel register)
        {
            if(!ModelState.IsValid)
            {
                return View(register);
            }
            int userId = 0;
            try
            {
                 userId = int.Parse(_editTicket.VarifyTicket(register.UserCode));
            }
            catch (Exception)
            {
                ModelState.AddModelError("UserName", "درخواست نامعتبر...! \n احتمالا زمان شما برای ایجاد تغییرات به پایان رسیده لطفا دوباره تلاش کنید");
                return View(register);
            }

            var error = _userRepository.EditUser(userId, register);
            if (error != UserErrorType.non)
            {
                SetUserError(error);

                return View(register);
            }

            return RedirectToAction("Index");
        }

        private void SetUserError(UserErrorType error)
        {
            if (error == UserErrorType.DuplicateEmail)
            {
                ModelState.AddModelError("Email", "ایمیل داده شده در سایت ثبت شده است...");
            }
            else if (error == UserErrorType.DuplicateUserName)
            {
                ModelState.AddModelError("UserName", "این نام کاربری قبلا در سایت ثبت نام کرده");
            }else if (error == UserErrorType.NotFound)
            {
                ModelState.AddModelError("UserName", "این کاربر پیدا نشد ...");
            }
        }
    }
}
