using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Exception;
using Newtonsoft.Json;
using RestSharp;

namespace Desktop.Data
{
    public static class RestResponseExtensions
    {
        private static Newtonsoft.Json.JsonSerializer serializer = new JsonSerializer();

        public static bool Succeeded(this IRestResponse resp)
        {
            return resp.StatusCode == HttpStatusCode.OK
                   || resp.StatusCode == HttpStatusCode.Created
                   || resp.StatusCode == HttpStatusCode.NoContent
                   || resp.StatusCode == HttpStatusCode.Accepted;
        }

        public static ExceptionResponse ExceptionResponse(this IRestResponse resp)
        {
            if (resp.Succeeded()) return null;
            using (var strReader = new StringReader(resp.Content))
            {
                using (var jsonReader = new JsonTextReader(strReader))
                {
                    return serializer.Deserialize<ExceptionResponse>(jsonReader);
                }
            }
        }
    }
}
