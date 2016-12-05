using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NHibernate.Validator.Engine;

namespace Common.Model.Validation.Validators
{
    class PasswordValidator : IValidator
    {
        /*
            At least one upper case english letter, (?=.*?[A-Z])
            At least one lower case english letter, (?=.*?[a-z])
            At least one digit, (?=.*?[0-9])
            At least one special character, (?=.*?[#?!@$%^&*-])
            Minimum 8 in length .{8,} (with the anchors)
        */
        public bool IsValid(object value, IConstraintValidatorContext constraintValidatorContext)
        {
            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (value == null || !(value is string)) return false;
            return regex.IsMatch(value.ToString());
        }
    }
}
