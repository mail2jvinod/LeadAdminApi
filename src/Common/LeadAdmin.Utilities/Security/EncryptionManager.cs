using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace LeadAdmin.Utilities.Security
{
    public static class EncryptionManager
    {
        #region Constants
        private const string passPhrase = "isurePro@7flagship";        // can be any string
        private const string saltValue = "isuite@77salt";        // can be any string
        private const string hashAlgorithm = "SHA1";             // can be "MD5"
        private const int passwordIterations = 1;                  // can be any number
        private const string initVector = "@1i2s3u4i5t6e7v7"; // must be 16 bytes
        private const int keySize = 128;                // can be 192 or 128 
        #endregion

        #region Encryption & Decryption methods
        public static string ComputeHashString(string plainText)
        {
            var sha = new SHA1CryptoServiceProvider();
            var hashedValue = new StringBuilder();
            var hashedData = sha.ComputeHash(Encoding.Unicode.GetBytes(plainText));
            foreach (var item in hashedData)
            {
                hashedValue.Append(String.Format("{0,2:X2}", item));
            }
            return hashedValue.ToString();
        }

        public static string Encrypt(string plainText)
        {
            return RijndaelHelper.Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }

        public static string Decrypt(string cipherText)
        {
            return RijndaelHelper.Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }
        #endregion

        public static string MacAddress
        {
            get
            {
                var networkInterface = GetNetworkInterface();
                return null != networkInterface ? networkInterface.Id : string.Empty;
            }
        }

        public static string IPAddress
        {
            get { return Dns.GetHostAddresses(Dns.GetHostName())[0].ToString(); }
        }

        public static string MachineName
        {
            get { return Dns.GetHostName(); }
        }

        private static NetworkInterface GetNetworkInterface()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            if (computerProperties == null)
                return null;

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
                return null;

            NetworkInterface best = null;
            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback || adapter.NetworkInterfaceType == NetworkInterfaceType.Unknown)
                    continue;
                if (!adapter.Supports(NetworkInterfaceComponent.IPv4))
                    continue;
                if (best == null)
                    best = adapter;
                if (adapter.OperationalStatus != OperationalStatus.Up)
                    continue;

                // A computer could have several adapters (more than one network card)
                // here but just return the first one for now...
                return adapter;
            }
            return best;
        }
    }
}
