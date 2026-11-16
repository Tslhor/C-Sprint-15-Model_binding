using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ProductsValidation.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DoubleRangeAttribute : ValidationAttribute
    {
        private readonly double minimum;
        private readonly double maximum;

        public DoubleRangeAttribute(double minimum, double maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var converted = Convert.ToDouble(value, CultureInfo.CurrentCulture);
            return converted >= minimum && converted <= maximum;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrWhiteSpace(ErrorMessage)
                ? $"The field {name} must be between {minimum} and {maximum}."
                : base.FormatErrorMessage(name);
        }
    }
}
