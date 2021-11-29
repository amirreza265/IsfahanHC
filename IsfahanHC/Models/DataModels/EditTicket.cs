using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.DataModels
{
    public class EditTicket
    {
        [Key]
        public int TicketId{ get; set; }
        [Required]
        public DateTime ExpirationTime { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }  
        public string? Value { get; set; }
        [Required]
        public string EditCode { get; set; }
    }
}
