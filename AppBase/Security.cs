using NLog;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EIS.AppBase
{
    public class Security
    {
        private static Logger logger_0;

        private static string CommonKey;

        static Security()
        {
            Security.logger_0 = LogManager.GetCurrentClassLogger();
            Security.CommonKey = "mytech";
        }

        public Security()
        {
        }

        public static string Decrypt(string strText, string userName)
        {
            string str;
            byte[] numArray = new byte[8];
            byte[] numArray1 = GetIV();
            byte[] numArray2 = new byte[strText.Length];
            string str1 = "";
            string str2 = "";
            try
            {
                str2 = Security.GetKey(userName);
                byte[] bytes = Encoding.UTF8.GetBytes(str2.Substring(0, 8));
                Array.Copy(bytes, numArray, 8);
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                numArray2 = Convert.FromBase64String(strText.Replace(".", "+").Replace("_", "/"));
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(numArray, numArray1), CryptoStreamMode.Write);
                try
                {
                    try
                    {
                        cryptoStream.Write(numArray2, 0, (int)numArray2.Length);
                        cryptoStream.FlushFinalBlock();
                        str1 = (new UTF8Encoding()).GetString(memoryStream.ToArray());
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                finally
                {
                    cryptoStream.Close();
                }
                str = str1;
            }
            catch (Exception exception2)
            {
                Exception exception1 = exception2;
                Security.logger_0.Error<string, string, string>("密文：{0}，钥匙：{1}，解密错误：{2}", strText, userName, exception1.Message);
                throw exception1;
            }
            return str;
        }

        public static string Decrypt(string strText)
        {
            return Security.Decrypt(strText, Security.CommonKey);
        }

        public static string Encrypt(string string_1)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            string base64String = Convert.ToBase64String(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(string_1)));
            return base64String;
        }

        public static string EncryptStr(string strText, string userName)
        {
            string str;
            if (userName.Length >= 32)
            {
                throw new Exception("钥匙长度不能超过32");
            }
            byte[] numArray = new byte[8];
            byte[] numArray1 = GetIV();
            string str1 = "";
            try
            {
                str1 = Security.GetKey(userName);
                byte[] bytes = Encoding.UTF8.GetBytes(str1.Substring(0, 8));
                Array.Copy(bytes, numArray, 8);
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                byte[] bytes1 = Encoding.UTF8.GetBytes(strText);
                string str2 = "";
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(numArray, numArray1), CryptoStreamMode.Write);
                try
                {
                    try
                    {
                        cryptoStream.Write(bytes1, 0, (int)bytes1.Length);
                        cryptoStream.FlushFinalBlock();
                        str2 = Convert.ToBase64String(memoryStream.ToArray()).Replace("+", ".").Replace("/", "_");
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                finally
                {
                    cryptoStream.Close();
                }
                str = str2;
            }
            catch (Exception exception2)
            {
                Exception exception1 = exception2;
                Security.logger_0.Error<string, string, string>("明文：{0}，钥匙：{1}，加密错误：{2}", strText, userName, exception1.Message);
                throw exception1;
            }
            return str;
        }

        public static string EncryptStr(string strText)
        {
            return Security.EncryptStr(strText, Security.CommonKey);
        }

        public static string GetUrlPara(string paraStr, string paraName)
        {
            string str;
            paraStr = paraStr.Replace("&nbsp;", " ");
            if (paraStr.StartsWith("&"))
            {
                paraStr = paraStr.Remove(0, 1);
            }
            if (paraStr.EndsWith("&"))
            {
                paraStr = paraStr.Remove(paraStr.Length - 1, 1);
            }
            string[] strArrays = paraStr.Split("&".ToCharArray());
            int num = 0;
            while (true)
            {
                if (num < (int)strArrays.Length)
                {
                    if (strArrays[num].Length != 0)
                    {
                        int num1 = strArrays[num].IndexOf("=", 0);
                        if (num1 > -1 && strArrays[num].Substring(0, num1).Trim().ToLower() == paraName.Trim().ToLower())
                        {
                            str = strArrays[num].Substring(num1 + 1, strArrays[num].Length - num1 - 1);
                            break;
                        }
                    }
                    num++;
                }
                else
                {
                    str = "";
                    break;
                }
            }
            return str;
        }

        /// <summary>
        /// 密钥=32位动态字符串=固定的字符串 + 登录用户账户
        /// </summary>
        /// <param name="string_1">登录用户账户</param>
        /// <returns></returns>
        private static string GetKey(string string_1)
        {
            //为了安全起见，每个下载源代码的朋友，修改一下自己的KV,IV
            string str = "c9d3c297b748450f9617c831d6d1ea43";
            string str1 = string.Concat(string_1, str.Substring(string_1.Length, 32 - string_1.Length));
            return str1;
        }


        /// <summary>   
        /// 获得初始向量IV 
        /// </summary>   
        /// <returns>初试向量IV</returns>   
        private static byte[] GetIV()
        {
            //为了安全起见，每个下载源代码的朋友，修改一下自己的KV,IV
            string sTemp = "bf77a4d607854dcca01cd9ca2b835053";
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }


    }
}