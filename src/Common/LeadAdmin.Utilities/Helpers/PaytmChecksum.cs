using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LeadAdmin.Utilities.Helpers
{
    public class PaytmChecksum
    {
        public PaytmChecksum()
        {
        }

        public static string decrypt(string input, string key)
        {
            string str;
            try
            {
                byte[] numArray = Convert.FromBase64String(input);
                MemoryStream memoryStream = new MemoryStream();
                Rijndael bytes = Rijndael.Create();
                bytes.Key = Encoding.ASCII.GetBytes(key);
                bytes.IV = new byte[] { 64, 64, 64, 64, 38, 38, 38, 38, 35, 35, 35, 35, 36, 36, 36, 36 };
                bytes.Mode = CipherMode.CBC;
                bytes.Padding = PaddingMode.PKCS7;
                CryptoStream cryptoStream = new CryptoStream(memoryStream, bytes.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(numArray, 0, (int)numArray.Length);
                cryptoStream.Close();
                str = Encoding.ASCII.GetString(memoryStream.ToArray());
            }
            catch (Exception exception)
            {
                PaytmChecksum.ShowException(exception);
                str = null;
            }
            return str;
        }

        public static string encrypt(string input, string key)
        {
            string base64String;
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input);
                MemoryStream memoryStream = new MemoryStream();
                Rijndael rijndael = Rijndael.Create();
                rijndael.Key = Encoding.ASCII.GetBytes(key);
                rijndael.IV = new byte[] { 64, 64, 64, 64, 38, 38, 38, 38, 35, 35, 35, 35, 36, 36, 36, 36 };
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;
                CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, (int)bytes.Length);
                cryptoStream.Close();
                base64String = Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception exception)
            {
                PaytmChecksum.ShowException(exception);
                base64String = null;
            }
            return base64String;
        }

        private static string generateRandomString(int length)
        {
            string str;
            if (length > 0)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                StringBuilder stringBuilder = new StringBuilder("");
                for (int i = 0; i < length; i++)
                {
                    int num = random.Next("@#!abcdefghijklmonpqrstuvwxyz#@01234567890123456789#@ABCDEFGHIJKLMNOPQRSTUVWXYZ#@".Length);
                    stringBuilder.Append("@#!abcdefghijklmonpqrstuvwxyz#@01234567890123456789#@ABCDEFGHIJKLMNOPQRSTUVWXYZ#@".Substring(num, 1));
                }
                str = stringBuilder.ToString();
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static string generateSignature(Dictionary<string, string> input, string key)
        {
            return PaytmChecksum.generateSignature(PaytmChecksum.getStringByParams(input), key);
        }

        public static string generateSignature(string input, string key)
        {
            string str;
            try
            {
                PaytmChecksum.validateGenerateCheckSumInput(key);
                StringBuilder stringBuilder = new StringBuilder(input);
                stringBuilder.Append("|");
                string str1 = PaytmChecksum.generateRandomString(4);
                stringBuilder.Append(str1);
                string hashedString = PaytmChecksum.getHashedString(stringBuilder.ToString());
                hashedString = string.Concat(hashedString, str1);
                str = PaytmChecksum.encrypt(hashedString, key);
            }
            catch (Exception exception)
            {
                PaytmChecksum.ShowException(exception);
                str = null;
            }
            return str;
        }

        private static string getHashedString(string inputValue)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputValue);
            byte[] numArray = (new SHA256Managed()).ComputeHash(bytes);
            string lower = BitConverter.ToString(numArray).Replace("-", "").ToLower();
            return lower;
        }

        private static string getStringByParams(Dictionary<string, string> parameters)
        {
            string str;
            if (parameters != null)
            {
                SortedDictionary<string, string> strs = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
                StringBuilder stringBuilder = new StringBuilder("");
                foreach (KeyValuePair<string, string> keyValuePair in strs)
                {
                    string value = keyValuePair.Value ?? "";
                    stringBuilder.Append(value).Append("|");
                }
                str = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
            }
            else
            {
                str = "";
            }
            return str;
        }

        private static void ShowException(Exception ex)
        {
            Console.WriteLine(string.Concat(new string[] { "Message : ", ex.Message, Environment.NewLine, "StackTrace : ", ex.StackTrace }));
        }

        private static void validateGenerateCheckSumInput(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Parameter cannot be null", "Specified key");
            }
        }

        private static void validateVerifyCheckSumInput(string checkSum, string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Parameter cannot be null", "Specified key");
            }
            if (checkSum == null)
            {
                throw new ArgumentNullException("Parameter cannot be null", "Specified checkSum");
            }
        }

        public static bool verifySignature(Dictionary<string, string> input, string key, string CheckSum)
        {
            bool flag = PaytmChecksum.verifySignature(PaytmChecksum.getStringByParams(input), key, CheckSum);
            return flag;
        }

        public static bool verifySignature(string input, string key, string CheckSum)
        {
            bool flag;
            try
            {
                PaytmChecksum.validateVerifyCheckSumInput(CheckSum, key);
                string str = PaytmChecksum.decrypt(CheckSum, key);
                if ((str == null ? false : str.Length >= 4))
                {
                    string str1 = str.Substring(str.Length - 4, 4);
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(input);
                    stringBuilder.Append("|");
                    stringBuilder.Append(str1);
                    flag = str.Equals(string.Concat(PaytmChecksum.getHashedString(stringBuilder.ToString()), str1));
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                PaytmChecksum.ShowException(exception);
                flag = false;
            }
            return flag;
        }
    }
}
