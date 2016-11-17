using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApi.Authorization
{
    public class StringUtils
    {

        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty) + "::" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        public static string GenerateSalt()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public static string CalculateHashedPassword(string password, string salt)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(new UTF8Encoding().GetBytes(password + salt)));
        }

    }
}