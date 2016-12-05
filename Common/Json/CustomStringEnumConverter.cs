using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Json
{
    public class CustomStringEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type type = value.GetType() as Type;

            if (!type.IsEnum) throw new InvalidOperationException("Only type Enum is supported");
            foreach (var field in type.GetFields())
            {
                if (field.Name == value.ToString())
                {
                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    writer.WriteValue(attribute != null ? attribute.Description : field.Name);

                    return;
                }
            }

            throw new ArgumentException("Enum not found");
        }
    }
}
