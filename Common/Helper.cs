using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SampleProject.Common
{
    public static class Helper
    {



        /// <summary>
        /// Search string with full text and ignore case
        /// </summary>
        /// <returns></returns>
        public static bool ContainsFullText(this string source, string toCheck)
        {
            if (string.IsNullOrWhiteSpace(toCheck))
                return false;

            var listWords = toCheck.Trim().Split(' ');

            foreach (var word in listWords)
            {
                if (source?.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }

            return false;
        }

        public static DateTime FromUnixTimestamp(this double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }

        public static double ToUnixTimestamp(this DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static byte[] GenerateJwtSigningKey(int keySizeInBits)
        {
            using (var hmac = new HMACSHA256())
            {
                return hmac.Key;
            }
        }

    }

}
