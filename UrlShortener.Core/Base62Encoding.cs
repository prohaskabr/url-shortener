using System.Text;

namespace UrlShortener.Core;

public static class Base62Encoding
{
    public static string Encode(long number)
    {
        const string Alphanumeric = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var result = new StringBuilder();
        do
        {
            result.Insert(0, Alphanumeric[(int)number % 62]);
            number /= 62;
        } while (number > 0);
        return result.ToString();

    }
}
