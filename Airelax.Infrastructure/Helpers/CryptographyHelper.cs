using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Lazcat.Infrastructure.Extensions;
using BCryptHelper = BCrypt.Net.BCrypt;

namespace Airelax.Infrastructure.Helpers
{
    public static class CryptographyHelper
    {
        public static string Hash(string text, out string salt) //(要被加密的字串,亂數產生的字串)
        {
            salt = BCryptHelper.GenerateSalt();
            return BCryptHelper.HashPassword(text, salt);
        }

        public static bool VerifyHash(string text, string hashedText) //密碼是否相符 VerifyHash(輸入的字串，亂數加密後的密碼)
        {
            return BCryptHelper.Verify(text, hashedText);
        }

        /// <summary>
        /// AES加密，使用CBC, 128bit, PKC27
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="shouldUrlEncode">明文是否需經過url encode， 預設否</param>
        /// <returns>密文</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string AesEncrypt(string plaintext, string key, string iv, bool shouldUrlEncode = false)
        {
            if (key.IsNullOrEmpty()) throw new ArgumentNullException(nameof(key));
            if (iv.IsNullOrEmpty()) throw new ArgumentNullException(nameof(iv));
            if (plaintext.IsNullOrEmpty()) throw new ArgumentNullException(nameof(plaintext));

            if (shouldUrlEncode) plaintext = WebUtility.UrlEncode(plaintext);
            try
            {
                var aes = new AesCryptoServiceProvider
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 128,
                    Key = Encoding.UTF8.GetBytes(key),
                    IV = Encoding.UTF8.GetBytes(iv)
                };
                var dataByteArray = Encoding.UTF8.GetBytes(plaintext);
                using var ms = new MemoryStream();
                using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                var encrypt = Convert.ToBase64String(ms.ToArray());
                return encrypt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// AES解密，使用CBC, 128bit, PKC27
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="shouldUrlDecode">是否需要url decode 預設否</param>
        /// <returns>明文</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string AesDecrypt(string ciphertext, string key, string iv, bool shouldUrlDecode = false)
        {
            if (key.IsNullOrEmpty()) throw new ArgumentNullException(nameof(key));
            if (iv.IsNullOrEmpty()) throw new ArgumentNullException(nameof(iv));
            if (ciphertext.IsNullOrEmpty()) throw new ArgumentNullException(nameof(ciphertext));

            try
            {
                using var aes = new AesCryptoServiceProvider
                {
                    KeySize = 128,
                    IV = Encoding.UTF8.GetBytes(iv),
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };
                var cipherText = Convert.FromBase64String(ciphertext);

                using var ms = new MemoryStream(cipherText);
                using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using var reader = new StreamReader(cs);
                var plaintext = reader.ReadToEnd();
                if (shouldUrlDecode) plaintext = WebUtility.UrlDecode(plaintext);
                return plaintext;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}