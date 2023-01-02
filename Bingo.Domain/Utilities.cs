using System.Text;
using Bingo.Domain.Errors;

namespace Bingo.Domain;

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

    public static T[,] SpanTo2DArray<T>(this ReadOnlySpan<T> span, byte rows, byte columns)
    {
        if (span.Length != columns * rows)
        {
            throw new Invalid2DArrayConversionException("The total elements from input is not the same as the desired output size.");
        }

        var array = new T[rows, columns];

        var listIndex = 0;
        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                array[row, column] = span[listIndex];
                listIndex++;
            }
        }

        return array;
    }

    public static T[,] ListTo2DArray<T>(this IList<T> list, int rows, int columns)
    {
        if (list.Count != columns * rows)
        {
            throw new Invalid2DArrayConversionException("The total elements from input is not the same as the desired output size.");
        }

        var array = new T[rows, columns];

        var listIndex = 0;
        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                array[row, column] = list[listIndex];
                listIndex++;
            }
        }

        return array;
    }

    public static char CharToUpper(this char character)
    {
        return character.ToString().ToUpperInvariant()[0];
    }
}
