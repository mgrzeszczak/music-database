using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Authorization;
using Common.Model.Validation.Attributes;
using Newtonsoft.Json;
using NHibernate.Validator.Constraints;


namespace Common.Model
{
    public class User : Entity<long>
    {
        [NotNullNotEmpty]
        public virtual string Login { get; set; }
        public virtual Role Role { get; set; }
        [Email,NotNullNotEmpty]
        public virtual string Email { get; set; }
        [JsonIgnore]
        public virtual string PasswordSalt { get; set; }
        [Password]
        public virtual string Password { get; set; }

        public virtual bool ShouldSerializePassword()
        {
            return false;
        }
    }
}
