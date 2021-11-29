using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Utility
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
            ErrorMessage = "پسوند فایل های آپلودی باید {0} باشد ";
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;

            if (files != null)
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!_extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            var ex = "[";
            foreach (var exten in _extensions)
            {
                ex += " " + exten + " ,";
            }

            return ErrorMessage.Replace("{0}", ex.Substring(0, ex.Length) + "]");
        }
    }
}
