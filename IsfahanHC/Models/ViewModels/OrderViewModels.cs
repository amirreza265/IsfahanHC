using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? PaidTime { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
