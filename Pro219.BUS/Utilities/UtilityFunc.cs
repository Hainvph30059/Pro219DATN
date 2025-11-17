using System.Security.Cryptography;
using System.Text;

namespace Pro219.API.Utilities
{
    public class UtilityFunc
    {
        public string HashPassword(string password)
        {
            //4297F44B13955235245B2497399D7A93 
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            md5.Clear();
            return sb.ToString();

        }
        public string GenerateRandomString(int count)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, count)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
