using System.Security.Cryptography;
using System.Text;

namespace hjs_encomiendas_servidor.Common
{
    public static class Utils
    {
        public static string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPass = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPass);
        }
    }
}
