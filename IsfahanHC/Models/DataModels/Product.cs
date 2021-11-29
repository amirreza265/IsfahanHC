using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }


        [Required]
        public int StorId { get; set; }

        [ForeignKey("StorId")]
        public Stor Stor { get; set; }

        public Item Item { get; set; }
        public List<PImage> Images { get; set; }
    }
}
