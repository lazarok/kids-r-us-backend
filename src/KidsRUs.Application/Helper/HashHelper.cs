using HashidsNet;

namespace KidsRUs.Application.Helper;

public static class HashHelper
{
    public const string HashIdsSalt = "kids_r_us_s3cret_s4lt";

    public static string ToHashId(this int number) =>
        GetHasher().Encode(number);

    public static int FromHashId(this string encoded)
    {
        try
        {
            return GetHasher().Decode(encoded).FirstOrDefault();
        }
        catch
        {
            return -1;
        }
    }

    private static Hashids GetHasher() => new(HashIdsSalt, 8);
}