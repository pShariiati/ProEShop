using System.Security.Cryptography;
using System.Text;

namespace ProEShop.Common.Helpers;

public static class SecurityHelpers
{
    public static string ToMd5(this string input)
    {
        var inputInBytes = Encoding.UTF8.GetBytes(input);

        using var md5 = MD5.Create();

        var hashedText = md5.ComputeHash(inputInBytes);

        return Convert.ToBase64String(hashedText);
    }
}