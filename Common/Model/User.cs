using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Authorization;
using Newtonsoft.Json;


namespace Common.Model
{
    public class User : Entity<long>
    {
        public virtual string Login { get; set; }
        public virtual Role Role { get; set; }
        public virtual string Email { get; set; }
        [JsonIgnore]
        public virtual string PasswordSalt { get; set; }
        public virtual string Password { get; set; }

        
        public virtual bool ShouldSerializePassword()
        {
            return false;
        }
    }
}
