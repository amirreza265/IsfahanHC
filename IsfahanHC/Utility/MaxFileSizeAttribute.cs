using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Utility
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        protected long _maxSize;
        public MaxFileSizeAttribute(long maxSize)
        {
            _maxSize = maxSize;
            ErrorMessage = "حجم مجاز برای {1} دقیقا {0} بایت است";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;

            if (files != null)
                foreach (var file in files)
                {
                    if (file.Length > _maxSize)
                    {
                        return new ValidationResult(GetErrorMessage(file.FileName));
                    }
                }

            return ValidationResult.Success;

            string GetErrorMessage(string fileName)
            {
                var ms = ErrorMessage.Replace("{0}", _maxSize.ToString());
                ms = ms.Replace("{1}", fileName);
                return ms;
            }
        }
    }
}
