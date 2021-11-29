using IsfahanHC.Models.DataModels;
using IsfahanHC.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels
{
    public class AddProductViewModel
    {
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تعداد در انبار")]
        public int QuantityInStock { get; set; }
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }

        public int UserId { get; set; }

        [MaxFileSize(3*1024*1024)]
        [AllowedExtensions(new string[] { ".jpg",".jpeg",".png" })]
        [Display(Name = "تصاویر")]
        public List<IFormFile> Images { get; set; }

    }
}
