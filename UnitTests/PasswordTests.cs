using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Model.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Utils;

namespace UnitTests
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void TestPasswordHash()
        {
            string characters = "ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnoprstuvwxyz0123456789!@#$%^&";
            Random random = new Random();
            StringBuilder password = new StringBuilder();
            for (var i = 0; i < 100; i++)
            {
                var length = random.Next(8, 20);
                for (var j=0;j<length;j++)
                    password.Append(characters[random.Next(characters.Length)]);
                var salt = StringUtils.GenerateSalt();
                var hash = StringUtils.CalculateHashedPassword(password.ToString(), salt);
                Assert.AreEqual(hash,StringUtils.CalculateHashedPassword(password.ToString(),salt));
                password.Clear();
            }
        }

        [TestMethod]
        public void TestPasswordValidation()
        {
            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            Assert.IsTrue(regex.IsMatch("11@@aaAA"));
            Assert.IsFalse(regex.IsMatch("11@@aaaa"));
            Assert.IsFalse(regex.IsMatch("01234567"));
            Assert.IsFalse(regex.IsMatch("@@@@@@@@"));
            Assert.IsFalse(regex.IsMatch("AAAAAAAA"));
            Assert.IsFalse(regex.IsMatch("11@@aaop"));

        }


    }
}
