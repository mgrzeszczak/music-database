using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop.Validation
{
    class YoutubeLinkValidationRule : ValidationRule
    {
        private Regex regex = new Regex(@"^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string txt = value as string;
                Match match = regex.Match(txt);
                if (match.Success) return new ValidationResult(true, null);
                else return new ValidationResult(false, "Invalid youtube link.");
            } catch (Exception)
            {
                return new ValidationResult(false, "Invalid youtube link.");
            }
        }
    }
}
