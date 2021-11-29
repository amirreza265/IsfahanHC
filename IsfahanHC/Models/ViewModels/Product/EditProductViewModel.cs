using IsfahanHC.Models.DataModels;
using IsfahanHC.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels.Product
{
    public class EditProductViewModel
    {
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تعداد در انبار")]
        public int QuantityInStock { get; set; }
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }
        public int ProductId { get; set; }


        public List<PImage> Images { get; set; }

        [Display(Name = "تصاویر محصول")]
        [MaxFileSize(3 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", "png", ".jpeg" })]
        public List<IFormFile> UploadImages { get; set; }
    }
}
