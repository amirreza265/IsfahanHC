using IsfahanHC.Data.Repository;
using IsfahanHC.Models;
using IsfahanHC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductsRepository _products;
        private IStorRepository _stors;

        public HomeController(ILogger<HomeController> logger,
            IProductsRepository products,
            IStorRepository stors)
        {
            _logger = logger;
            _products = products;
            _stors = stors;
        }

        public IActionResult Index()
        {
            var pro = _products.GetShowProducts();
            return View(pro);
        }

        [Route("/Search/{filter}")]
        public IActionResult Search(string filter)
        {
            var products = _products.GetShowProductsWhere(p => p.Name.ToLower().Contains(filter.ToLower()) ||
                            p.StorName.ToLower().Contains(filter.ToLower()) ||
                            p.Description.ToLower().Contains(filter.ToLower()));

            if (products == null)
                return NotFound404(404);

            return View("index", products);
        }

        [Route("/NotFound/{code?}")]
        public IActionResult NotFound404(int code)
        {
            if (code < 400 || code > 500) code = 404;
            return View(code);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
