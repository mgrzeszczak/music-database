using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Json;
using Newtonsoft.Json;

namespace Common.Authorization
{
    [JsonConverter(typeof(CustomStringEnumConverter))]
    public enum Role
    {
        USER= 0,ADMIN=1
    }
}
