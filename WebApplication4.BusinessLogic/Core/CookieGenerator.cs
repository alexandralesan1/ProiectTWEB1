using System;
using System.Text;
using System.Security.Cryptography;

public static class CookieGenerator
{
     private const string SecretKey = "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP";

     public static string Create(string value)

     {
          return EncryptStringAes(value, SecretKey);
     }

     public static string Validate(string value)
     {
          return DecryptStringAes(value, SecretKey);
     }

     private static string EncryptStringAes(string plainText, string key)
     {
          using (var aes = Aes.Create())
          {
               aes.Key = Encoding.ASCII.GetBytes(key.Substring(0, 32));
               aes.IV = new byte[16];

               using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
               {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);
                    return Convert.ToBase64String(encryptedBytes);
               }
          }
     }

     private static string DecryptStringAes(string encryptedText, string key)
     {
          using (var aes = Aes.Create())
          {
               aes.Key = Encoding.ASCII.GetBytes(key.Substring(0, 32));
               aes.IV = new byte[16];

               using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
               {
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
               }
          }
     }
}
