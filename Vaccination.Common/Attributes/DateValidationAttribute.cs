using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vaccination.Common.Attributes
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public static readonly DateTime MinDate = new DateTime(1900, 1, 1);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var birthday = (DateTime?)value;

            if (birthday < MinDate || birthday > DateTime.Today)
            {
                return new ValidationResult($"Date must be in ({MinDate.ToShortDateString()} - {DateTime.Today.ToShortDateString()}) range");
            }

            return ValidationResult.Success;
        }
    }
}
