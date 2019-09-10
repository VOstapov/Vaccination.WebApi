using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Vaccination.Common.Attributes
{
    public class GenderValidationAttribute : ValidationAttribute
    {
        private static readonly ConcurrentBag<string> genders = new ConcurrentBag<string>() { "Мужской", "Женский" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gender = (string)value;

            if (!genders.Any(a => a == gender))
            {
                return new ValidationResult($"Undefined gender");
            }

            return ValidationResult.Success;
        }
    }
}
