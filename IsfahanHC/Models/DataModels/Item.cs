using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName ="money")]
        public decimal Price { get; set; }
        [Required]
        public int QuantityInStock { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
