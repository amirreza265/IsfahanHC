using IsfahanHC.Models.DataModels;
using IsfahanHC.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Data.Repository
{
    public interface ICartRepository
    {
        List<OrderViewModel> GetOrders(int UserId);
        OrderViewModel GetCurrentOrder(int userId);
        void SetOrderPaid(int userId);
        void AddToOrder(int userId, OrderItemViewModel orderItem);
        void RemoveFromOrder(int userId, OrderItemViewModel orderItem);
    }

    public class CartRepository : ICartRepository
    {
        IsfahanHCDbContext _cotext;
        public CartRepository(IsfahanHCDbContext context)
        {
            _cotext = context;
        }

        public void AddToOrder(int userId, OrderItemViewModel orderItemvm)
        {
            var order = _cotext.Orders
                .Where(o => !o.IsPaid && o.UserId == userId)
                .FirstOrDefault();
            if (order != null)
            {
               var orderItem = _cotext.OrderItems
                    .Where(ot => ot.OrderId == order.OrderId && ot.ProductId == orderItemvm.ProductId)
                    .FirstOrDefault();
                if (orderItem != null)
                {
                    orderItem.Quantity += 1;
                }else
                {
                    orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        Price = orderItemvm.Price,
                        ProductId = orderItemvm.ProductId,
                        Quantity = 1
                    };
                    _cotext.Add(orderItem);
                }
            }else
            {
                order = new Order
                {
                    CreateTime = DateTime.Now,
                    IsPaid = false,
                    UserId = userId
                };
                _cotext.Add(order);
                _cotext.SaveChanges();
                var orderItem = new OrderItem
                {
                    Quantity = 1,
                    OrderId = order.OrderId,
                    Price = orderItemvm.Price,
                    ProductId = orderItemvm.ProductId,
                };
                _cotext.Add(orderItem);
            }

            _cotext.SaveChanges();
        }

        public OrderViewModel GetCurrentOrder(int userId)
        {
            return _cotext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(ot => ot.Product)
                .ThenInclude(p => p.Item)
                .Where(o => !o.IsPaid && o.UserId == userId)
                .Select(o => new OrderViewModel()
                {
                    IsPaid = o.IsPaid,
                    OrderItems = o.OrderItems
                    .Select(ot => new OrderItemViewModel()
                    {
                        ProductName = ot.Product.Name,
                        Price = ot.Product.Item.Price,
                        Quantity = ot.Quantity,
                        ProductId = ot.Product.ProductId
                    }),
                    OrderId = o.OrderId,
                    TotalPrice = o.OrderItems.Sum(ot => ot.Price * ot.Quantity),
                    UserName = o.User.UserName
                }).FirstOrDefault();
        }

        public List<OrderViewModel> GetOrders(int userId)
        {
            return _cotext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(ot => ot.Product)
                .ThenInclude(p => p.Item)
                .Where(o => o.UserId == userId)
                .Select(o => new OrderViewModel()
                {
                    IsPaid = o.IsPaid,
                    OrderItems = o.OrderItems
                    .Select(ot => new OrderItemViewModel()
                    {
                        ProductName = ot.Product.Name,
                        Price = ot.Product.Item.Price,
                        Quantity = ot.Quantity,
                        ProductId = ot.Product.ProductId
                    }),
                    OrderId = o.OrderId,
                    TotalPrice = o.OrderItems.Sum(ot => ot.Price),
                    UserName = o.User.UserName,
                    CreateTime = o.CreateTime,
                    PaidTime = o.PaidTime
                }).ToList();
        }

        public void RemoveFromOrder(int userId, OrderItemViewModel orderItemvm)
        {
            var order = _cotext.Orders
                .Where(o => !o.IsPaid && o.UserId == userId)
                .FirstOrDefault();
            if (order != null)
            {
                var orderItem = _cotext.OrderItems
                     .Where(ot => ot.OrderId == order.OrderId && ot.ProductId == orderItemvm.ProductId)
                     .FirstOrDefault();

                if (orderItem?.Quantity > 1)
                {
                    orderItem.Quantity -= 1;
                }else if (orderItem != null)
                {
                    _cotext.Remove(orderItem);
                }

                _cotext.SaveChanges();
            }
        }
        public void SetOrderPaid(int userId)
        {
            var order = _cotext.Orders
                .Where(o => !o.IsPaid && o.UserId == userId)
                .FirstOrDefault();
            if (order != null)
            {
                order.IsPaid = true;
                order.PaidTime = DateTime.Now;
            }
            _cotext.SaveChanges();
        }
    }
}
