using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class Stor
    {
        [Key]
        public int StorId { get; set; }
        [Required]
        public int ImageId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Seller> Sellers { get; set; }
        public List<Product> Products { get; set; }

        [ForeignKey("ImageId")]
        public SImage Image { get; set; }

    }
}
