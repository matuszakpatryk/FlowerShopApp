using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Flower.Extensions.Validators
{
    public class EmployeeNumberCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }

            var phone = value.ToString();
            if (Regex.IsMatch(phone, "^[A-D]{1}[/][0-9]{2}[/][0-9]{2}$"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid employee number format (use format: XXX/XX/XX).");
        }
    }
}
