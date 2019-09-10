using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vaccination.Common.Attributes
{
    public class DateValidationAttribute : ValidationAttribute
    {
        public static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        public static readonly DateTime MaxDate = DateTime.Today;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var birthday = (DateTime?)value;

            if (birthday < MinDate || birthday > MaxDate)
            {
                return new ValidationResult($"Date must be in ({MinDate.ToShortDateString()} - {MaxDate.ToShortDateString()}) range");
            }

            return ValidationResult.Success;
        }
    }
}
