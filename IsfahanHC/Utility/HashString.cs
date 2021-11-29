using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class HashString
    {
        public static string GetHashString(string str)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(str);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);
            // Bytes to string
            return
                System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(hashBytes), "-", "").ToLower();
        }
    }
}
