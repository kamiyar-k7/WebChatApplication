
using System.Security.Cryptography;
using System.Text;

namespace Application.Passhelper;

public class PassHelper
{
    public static string EncodePasswordMd5(string Pass)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] originalBytes = Encoding.UTF8.GetBytes(Pass);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
        }
    }
}
