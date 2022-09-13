using System.Text;

namespace Bingo.Library;

public static class Utilities
{
    public static string StringFormat(this string toFormat)
    {
        ReadOnlySpan<char> text = toFormat;
        Span<char> span = stackalloc char[toFormat.Length];

        for (var i = 0; i < toFormat.Length; i++)
        {
            span[i] = text[i] switch
            {
                >= 'a' and <= 'z' => (char)(text[i] - 32),
                _ => text[i],
            };
        }

        var builder = new StringBuilder(toFormat.Length);
        builder.Append(span);
        return builder.Replace(" ", "").ToString();
    }
}