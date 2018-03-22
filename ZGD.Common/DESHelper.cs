#region

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace ZGD.Common
{
    /// <summary>
    /// Des对称加密
    /// </summary>
    public static class DESHelper
    {
        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="input"> 待加密的字符串 </param>
        /// <param name="key"> 密钥（8位） </param>
        /// <param name="isUrl">该加密内容是否在 url中传输，如果是请设置该参数为true，以替换+导致的异常</param>
        /// <returns></returns>
        public static string DesEncrypt(string input, string key, bool isUrl = false)
        {
            try
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = Convert.FromBase64String(key);
                des.Mode = CipherMode.ECB;

                byte[] valBytes = Encoding.Unicode.GetBytes(input);
                ICryptoTransform transform = des.CreateEncryptor();

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cs.Write(valBytes, 0, valBytes.Length);
                cs.FlushFinalBlock();
                byte[] returnBytes = ms.ToArray();
                cs.Close();

                string enStr = Convert.ToBase64String(returnBytes);
                if (isUrl)
                    enStr = enStr.Replace("+", "%2B");
                return enStr;
            }
            catch (Exception ex)
            {
                return input;
            }
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="input"> 待解密的字符串 </param>
        /// <param name="key"> 密钥（8位） </param>
        /// <returns></returns>
        public static string DesDecrypt(string input, string key)
        {
            try
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = Convert.FromBase64String(key);
                des.Mode = CipherMode.ECB;
                byte[] valBytes = Convert.FromBase64String(input);
                ICryptoTransform transform = des.CreateDecryptor();

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cs.Write(valBytes, 0, valBytes.Length);
                cs.FlushFinalBlock();
                byte[] returnBytes = ms.ToArray();
                cs.Close();
                return Encoding.Unicode.GetString(returnBytes);
            }
            catch (Exception ex)
            {
                return input;
            }
        }

        /// <summary>
        /// 输出加密数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEncrypt(this string value, string key)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return DesEncrypt(value, key, false);
            }
            return value;
        }

        /// <summary>
        /// 输出解密数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDecrypt(this string value, string key)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return DesDecrypt(value, key);
            }
            return value;
        }
    }
}