using System;
using System.Security.Cryptography;
using System.Text;
using Reppertum.Core;

namespace Reppertum.Crypto 
{
    public static class Cryptography
    {
        public static string CalculateHash(Config config, string data)
        {
            switch (config.HashType)
            {
                case ("sha256"):
                    return Sha256(data);
                default:
                    return Sha256(data);
            }
        }

        private static string Sha256(string data) 
        {
            SHA256Managed crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            Byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(data));
            foreach (Byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}