using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Desktop.Data
{
    public interface IJsonSerializer : ISerializer, IDeserializer
    {

    }
}
