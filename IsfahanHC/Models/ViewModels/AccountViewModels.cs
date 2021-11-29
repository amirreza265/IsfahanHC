using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "حداکثر تعداد کاراکتر {0} باید {1} تا باشد")]
        [MinLength(3, ErrorMessage = "حداقل تعداد کاراکتر {0} باید {1} تا باشد")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        public string UserCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "حداکثر تعداد کاراکتر {0} باید {1} تا باشد")]
        [MinLength(4, ErrorMessage = "حداقل تعداد کاراکتر {0} باید {1} تا باشد")]
        [EmailAddress(ErrorMessage ="پست الکترونیکی شما صحیح نیست")]
        [Display(Name = "پست الکترونیکی")]
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50 ,ErrorMessage ="حداکثر تعداد کاراکتر {0} باید {1} تا باشد")]
        [MinLength(8, ErrorMessage = "حداقل تعداد کاراکتر {0} باید {1} تا باشد")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "حداکثر تعداد کاراکتر {0} باید {1} تا باشد")]
        [MinLength(8, ErrorMessage = "حداقل تعداد کاراکتر {0} باید {1} تا باشد")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Compare("Password", ErrorMessage = "{0} کلمه عبور با {1} برابر نیست")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
