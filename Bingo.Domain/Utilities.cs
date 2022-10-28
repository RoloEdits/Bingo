using System.Text;

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
            // TODO: create new Invalid2DArrayConversionException
            throw new Exception("The total elements from input is not the same as the desired output size.");
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

    public static T[,] ListTo2DArray<T>(this IList<T> list, byte rows, byte columns)
    {
        if (list.Count != columns * rows)
        {
            // TODO: create new Invalid2DArrayConversionException
            throw new Exception("The total elements from input is not the same as the desired output size.");
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

    public static IEnumerable<(T, U, int)> Zip<T, U>(this IList<T> tList, IList<U> uList) {

        var smallest = Math.Min(tList.Count, uList.Count);

        for(var i = 0; i < smallest; i++ ) {

            yield return (tList[i], uList[i], i);

        }
    }

    /// <summary>
    /// Creates an iterator over two collections allowing to foreach over multiple collections at once.
    /// </summary>
    /// <param name="tArray"></param>
    /// <param name="uArray"></param>
    /// <typeparam name="T"> is a 2D array</typeparam>
    /// <typeparam name="U"> is a 2D array</typeparam>
    /// <returns> A sequence of tuples containing the elements in both collections at the current position of the enumerator and the current index.</returns>
    public static IEnumerable<(T, U, (int, int))> Zip<T, U>(this T[,] tArray, U[,] uArray) {

        var rows = Math.Min(tArray.GetUpperBound(0), uArray.GetUpperBound(0));

        var columns = Math.Min(tArray.GetUpperBound(1), uArray.GetUpperBound(1));

        for(var row = 0; row < rows; row++ ) {

            for(var column = 0; column < columns; column++ ) {

                yield return (tArray[row, column], uArray[row, column], (row, column));

            }

        }

    }
}
