using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public DateTime? PaidTime { get; set; }
        public bool IsPaid { get; set; }
        [Column(TypeName = "money")]
        public decimal? TotalPrice { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
