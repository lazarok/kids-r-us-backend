using System.Text;

namespace KidsRUs.Application.Extensions;

public static class StringExtension
{
    public static string EncodeToBase64(this string toEncode, Encoding encoding = null)
    {
        return encoding == null
            ? Convert.ToBase64String(Encoding.UTF8.GetBytes(toEncode))
            : Convert.ToBase64String(encoding.GetBytes(toEncode));
    }

    public static string DecodeFromBase64(this string encodeData, Encoding encoding = null)
    {
        return encoding?.GetString(Convert.FromBase64String(encodeData))
               ?? Encoding.UTF8.GetString(Convert.FromBase64String(encodeData));
    }
}