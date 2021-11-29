using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class SImage
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        public string FileName { get; set; }

        public Stor Stor { get; set; }
    }
}
