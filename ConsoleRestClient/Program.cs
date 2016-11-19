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
            IRestClient client = new RestClient("http://localhost:55059/api");

            var jsonSerializer = NewtonsoftJsonSerializer.Default;
            client.AddHandler("application/json", jsonSerializer);
            client.AddHandler("text/json", jsonSerializer);
            client.AddHandler("text/x-json", jsonSerializer);
            client.AddHandler("text/javascript", jsonSerializer);
            client.AddHandler("*+json", jsonSerializer);

            IRestRequestFactory factory = new RestRequestFactory();

            User newUser = new User();
            newUser.Login = "Maciek";
            newUser.Password = "trudne_haslo";
            newUser.Email = "maciejgrzeszczak@gmail.com";

            var resp = client.Execute<User>(factory.RegisterRequest(newUser));

            Console.WriteLine("Success: " + resp.Succeeded());
            if (!resp.Succeeded())
            {
                var exResp = ((IJsonSerializer) resp.Request.JsonSerializer).Deserialize<ExceptionResponse>(resp);
                Console.WriteLine(exResp.ErrorCode);
                Console.WriteLine(exResp.Message);
                Console.WriteLine(exResp.TimeStamp);
            }
            else
            {
                Console.WriteLine(resp.Data);
            }
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
