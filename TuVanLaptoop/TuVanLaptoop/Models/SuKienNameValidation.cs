using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuVanLaptoop.Models
{
  
        public class SuKienNameValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value == null)
                {
                    return new ValidationResult("Tên sự kiện là bắt buộc");
                }
                return ValidationResult.Success;
            }
        }
    
}