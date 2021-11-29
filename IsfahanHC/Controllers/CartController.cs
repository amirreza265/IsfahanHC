using IsfahanHC.Data.Repository;
using IsfahanHC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZarinpalSandbox;

namespace IsfahanHC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        ICartRepository _carts;
        IProductsRepository _products;
        int _userId;

        public CartController(ICartRepository carts, IProductsRepository products)
        {
            _carts = carts;
            _products = products;
        }
        /// <summary>
        /// show all carts
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// get order by id
        /// </summary>
        /// <param name="id">order id</param>
        public IActionResult Order(int id)
        {
            return View();
        }


        public IActionResult Current()
        {
            _userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = _carts.GetCurrentOrder(_userId);

            if (order == null)
                order = new OrderViewModel()
                {
                    OrderItems = new List<OrderItemViewModel>()
                };

            return View(order);
        }

        public IActionResult AddToCart(int productId)
        {
            var pro = _products.GetShowProductById(productId);
            var cartItemVm = new OrderItemViewModel()
            {
                Price = pro.Price,
                ProductId = productId
            };
            _userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _carts.AddToOrder(_userId, cartItemVm);

            return RedirectToAction("Current");
        }

        public IActionResult Remove(int productId)
        {
            var pro = _products.GetShowProductById(productId);
            var cartItemVm = new OrderItemViewModel()
            {
                Price = pro.Price,
                ProductId = productId
            };
            _userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _carts.RemoveFromOrder(_userId, cartItemVm);

            return RedirectToAction("Current");
        }

        public IActionResult Payment()
        {
            //get order and create a requst to zarinpal
            _userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var order = _carts.GetCurrentOrder(_userId);

            if (order == null)
                return NotFound();
            string callbackUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host +
                Url.Action("OnlinePayment", "Cart", new { orderId = order.OrderId });

            var payment = new Payment((int)order.TotalPrice);
            var result = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                callbackUrl, User.FindFirstValue("Email"));

            if (result.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }
            else
            {
                return RedirectToAction("current");
            }
        }

        [Route("/OnlinePay/{orderId}")]
        public IActionResult OnlinePayment(int orderId)
        {
            if (HttpContext.Request.Query["Authority"] != "" &&
                HttpContext.Request.Query["Status"] != "")
                if (HttpContext.Request.Query["Status"].ToString().ToLower() == "ok")
                {
                    _userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var authority = HttpContext.Request.Query["Authority"].ToString();
                    var order = _carts.GetCurrentOrder(_userId);
                    var paymrnt = new Payment((int)order.TotalPrice);
                    var result = paymrnt.Verification(authority).Result;
                    ViewBag.status = result.Status;
                    if (result.Status == 100)
                    {
                        _carts.SetOrderPaid(_userId);
                        ViewBag.code = result.RefId;
                        ViewBag.message = "پرداخت با موفقیت انجام شد";
                    }
                    else
                    {
                        _carts.SetOrderPaid(_userId);
                        ViewBag.message = "پرداخت با موفقیت انجام نشد";
                    }
                    return View();
                }
                else if (HttpContext.Request.Query["Status"].ToString().ToLower() == "nok")
                {
                    _carts.SetOrderPaid(_userId);
                    ViewBag.status = 102;
                    ViewBag.message = "پرداخت با موفقیت انجام نشد";
                    return View();
                }
            return NotFound();
        }
    }
}
