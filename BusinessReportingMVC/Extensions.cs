using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace BusinessReportingMVC
{
    public static class Extensions
    {
        public static string StringToBytesToHashed(this string saltedPassword)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);

            using (var sha = SHA256.Create())
            {
                byte[] hashedBytes = sha.ComputeHash(bytes);
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                
                return hashedPassword;
            }
        }
    }
}