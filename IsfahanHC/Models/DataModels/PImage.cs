using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class PImage
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string FileName { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
