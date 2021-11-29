using IsfahanHC.Data.Repository;
using IsfahanHC.Models.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IsfahanHC.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        IUserRepository _userRepository;
        ICartRepository _cartRepository;
        public HomeController(IUserRepository userRepository, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public IActionResult Index()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userRepository.GetUser(userName);
            var orders = _cartRepository.GetOrders(userId);

            var userProf = new UserProfileViewModel
            {
                User = user,
                Orders = orders
            };

            return View(userProf);
        }
    }
}
