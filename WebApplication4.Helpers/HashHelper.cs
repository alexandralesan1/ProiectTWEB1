using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication4.Helpers
{
     public static class HashHelper
     {
          public static string GenerateHash(string password) 
          {
               using(var md5 = MD5.Create()) 
               {
                    var originalBytes = Encoding.Default.GetBytes(password + "saltingPart");
                    var encodedBytes = md5.ComputeHash(originalBytes);
                    return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
               }
          }
     }
}
