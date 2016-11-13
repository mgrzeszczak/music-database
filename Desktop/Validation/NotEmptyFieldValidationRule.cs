using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop.Validation
{
    class NonEmptyFieldValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            if (string.IsNullOrWhiteSpace(text))
                return new ValidationResult(false, "Field cannot be empty");

            return new ValidationResult(true, null);
        }
    }
}
