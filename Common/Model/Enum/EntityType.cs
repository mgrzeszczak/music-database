using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Json;
using Newtonsoft.Json;

namespace Common.Model.Enum
{
    [JsonConverter(typeof(CustomStringEnumConverter))]
    public enum EntityType
    {
        ARTIST,ALBUM,SONG
    }
}
