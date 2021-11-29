using IsfahanHC.Data.Repository;
using IsfahanHC.Models.ViewModels;
using IsfahanHC.Models.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ProductController : Controller
    {
        IProductsRepository _productsRepository;
        public ProductController(IProductsRepository products)
        {
            _productsRepository = products;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddProductViewModel addProduct)
        {
            if (!ModelState.IsValid)
                return View(addProduct);

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            addProduct.UserId = userId;
            var id = _productsRepository.AddProduct(addProduct);
            return RedirectToAction("edit", new { productId = id });
            // return RedirectToAction("AddImageToProduct");
        }

        public IActionResult Edit(int productId)
        {
            var pro = _productsRepository.GetProductById(productId);
            if (pro == null)
                Redirect("/seller");

            var editPro = new EditProductViewModel
            {
                Name = pro.Name,
                QuantityInStock = pro.Item.QuantityInStock,
                Description = pro.Description,
                Images = pro.Images,
                Price = pro.Item.Price,
                ProductId = pro.ProductId,
            };

            return View(editPro);
        }
        [HttpPost]
        public IActionResult Edit(EditProductViewModel editProduct)
        {
            if (!ModelState.IsValid)
                return View(editProduct);

            _productsRepository.EditProduct(editProduct);

            return RedirectToAction("Edit", new { productId = editProduct.ProductId });
        }

        public IActionResult AddImage(EditProductViewModel addProductimage)
        {
            if (!ModelState.IsValid)
            {
                var pro = _productsRepository.GetProductById(addProductimage.ProductId);
                addProductimage.Images = pro.Images;
                addProductimage.Name = pro.Name;
                addProductimage.Description = pro.Description;
                addProductimage.Price = pro.Item.Price;
                addProductimage.QuantityInStock = pro.Item.QuantityInStock;

                return View("edit", addProductimage);
            }
            var pivm = new ProductImageViewModel
            {
                ProductId = addProductimage.ProductId,
                UploadImages = addProductimage.UploadImages
            };
            _productsRepository.AddProductImage(pivm);
            return RedirectToAction("Edit", new { productId = addProductimage.ProductId });
        }

        public IActionResult RemoveImage(int imageId)
        {
            var pId = _productsRepository.GetProductIdByImage(imageId);
            _productsRepository.RemoveProductImageById(imageId);
            return RedirectToAction("Edit", new { productId = pId });
        }

        public IActionResult Remove(int productId)
        {
            _productsRepository.RemoveProductById(productId);
            return Redirect("/seller");
        }
    }
}
