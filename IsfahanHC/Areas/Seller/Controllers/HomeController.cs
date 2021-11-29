using IsfahanHC.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IsfahanHC.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize]
    public class HomeController : Controller
    {
        IProductsRepository _products;
        IUserRepository _userRepository;
        public HomeController(IProductsRepository products,
            IUserRepository userRepository)
        {
            _products = products;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(_products.GetShowProductsBySeller(userId));
        }
    }
}
