using System;
using System.Security.Cryptography;
using System.Text;

public static class CookieGenerator
{
   
    private const string SaltData = "QADLz4qk3rVgBSGjDfAH3XWV" +
                                   "qKKagMXezBPv7TmXvwnXDDR" +
                                   "pHaLBv4JnTGRwLg9tzbmV77g" +
                                   "8DUEAEa6JPv66hy7SwHBL4z4" +
                                   "FbGdh2MVs4kq9RcaZEAszuP5" +
                                   "ccLsEfqCpwdSvWt479DCZrw" +
                                   "jSHrJVwaja9WQWAmEY9NsPv" +
                                   "EHKnFwHTGAVPXpjpCxkbedYq" +
                                   "uEauLvZLphwmJLUteZ4QAXU6" +
                                   "Z4F3PDmh3wsQXvSctQBHvNWf";

    private static readonly byte[] Salt = Encoding.ASCII.GetBytes(SaltData); 


    private const string EncryptionKey = "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKFkGbZEeDGFDvsGYNHpUXFUUUUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP";


    public static string Create(string value)
    {
        return EncryptStringAes(value, EncryptionKey);
    }


    public static string Validate(string value)
    {
        return DecryptStringAes(value, EncryptionKey);
    }


    private static string EncryptStringAes(string plainText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Salt;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (System.IO.StreamWriter swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

   
    private static string DecryptStringAes(string cipherText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = Salt;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
