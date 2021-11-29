using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name ="پست الکترونیکی")]
        public string Email { get; set; }
        [Display(Name ="ادمین")]
        public bool IsAdmin { get; set; }
        [Display(Name = "فروشنده")]
        public bool IsSeller { get; set; }
        [Display(Name = "نام فروشگاه")]
        public string StorName { get; set; }
        [Display(Name ="تاریخ ثبت نام")]
        public DateTime RegisterTime { get; set; }
    }
}
