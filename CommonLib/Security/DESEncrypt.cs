using System;
using System.Security.Cryptography;
using System.Text;

namespace CommonLib.Security
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DesEncrypt
    {
        #region ========加密========

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string text, string sKey)
        {
            var inputArray = Encoding.UTF8.GetBytes(text);
            var tripleDes = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(sKey);
            var allKey = new byte[24];
            Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDes.Key = allKey;
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;
            var cTransform = tripleDes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        #endregion

        #region ========解密========
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string sKey)
        {
            var inputArray = Convert.FromBase64String(text);
            var tripleDes = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(sKey);
            var allKey = new byte[24];
            Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDes.Key = allKey;
            tripleDes.Mode = CipherMode.ECB;
            tripleDes.Padding = PaddingMode.PKCS7;
            var cTransform = tripleDes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        #endregion

    }
}
