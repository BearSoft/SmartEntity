using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace SmartEntity.Basic
{
    public class HashHelper
    {
        public static string ComputeSha256Hash(string input)
        {
            byte[] hashBuffer = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            var stringBuilder = new StringBuilder(hashBuffer.Length * 2);
            foreach (byte num in hashBuffer)
            {
                stringBuilder.Append(num.ToString("X2", CultureInfo.InvariantCulture));
            }
            return stringBuilder.ToString();
        }
    }
}
