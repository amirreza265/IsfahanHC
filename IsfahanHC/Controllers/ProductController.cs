using IsfahanHC.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Controllers
{
    public class ProductController : Controller
    {
        IProductsRepository _products;
        public ProductController(IProductsRepository products)
        {
            _products = products;
        }
        [Route("/product/{id}/{name?}")]
        public IActionResult Index(int id, string name)
        {
            return View(_products.GetShowProductById(id));
        }
    }
}
