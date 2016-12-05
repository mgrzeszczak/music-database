using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Common.Model.Enum
{
    [JsonConverter(typeof(CustomStringEnumConverter))]
    public enum Genre
    {
        [Description("Rock")]
        ROCK,
        [Description("Pop")]
        POP,
        [Description("Hip-Hop")]
        HIP_HOP,
        [Description("Metal")]
        METAL,
        [Description("Rap")]
        RAP,
        [Description("Country")]
        COUNTRY,
        [Description("R&B")]
        R_B,
        [Description("Jazz")]
        JAZZ,
        [Description("Classical")]
        CLASSICAL,
        [Description("Alternative")]
        ALTERNATIVE,
        [Description("Techno")]
        TECHNO,
        [Description("Electronic")]
        ELECTRONIC,
        [Description("Blues")]
        BLUES,
        [Description("Disco")]
        DISCO,
        [Description("Folk")]
        FOLK,
        [Description("House")]
        HOUSE,
    }
    
}
