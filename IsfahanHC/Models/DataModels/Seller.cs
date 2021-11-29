using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class Seller
    {
        [Key]
        public int SellerId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int StorId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("StorId")]
        public Stor Stor { get; set; }
    }
}
