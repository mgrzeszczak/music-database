using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Exception;
using Common.Model;
using Desktop.Data;
using RestSharp;

namespace ConsoleRestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var date1 = DateTime.Now;
            var date2 = DateTime.Now.AddSeconds(300);
            var diff = date2 - date1;
            Console.WriteLine(diff > TimeSpan.FromMinutes(5));

        }  
    }

    public static class RestResponseExtensions
    {
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
            return ((IJsonSerializer)resp.Request.JsonSerializer).Deserialize<ExceptionResponse>(resp);
        }
    }
}
