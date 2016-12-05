using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.Validation.Validators;
using NHibernate.Validator.Engine;

namespace Common.Model.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [ValidatorClass(typeof(PasswordValidator))]
    class Password : Attribute,IRuleArgs
    {
        private string message = "Password must have at least 1 upper- and lowercase character, at least 1 digit, at least 1 special character and minimum 8 characters";
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
