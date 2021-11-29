using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(300)]
        [MinLength(4)]
        public string Email { get; set; }
        [Required]
        [MaxLength(300)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public DateTime RegisterTime { get; set; }

        public bool IsDeleted { get; set; }
        
        public DateTime? DeleteTime { get; set; }

        public bool IsAdmin { get; set; }


        public Seller Seller { get; set; }

        public List<Order> Orders{ get; set; }

    }
}
