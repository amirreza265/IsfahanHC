using IsfahanHC.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Models.ViewModels
{
    public class ProductImageViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "تصاویر محصول")]
        [MaxFileSize(3 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", "png", ".jpeg" })]
        public List<IFormFile> UploadImages { get; set; }
    }
}
