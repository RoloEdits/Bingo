namespace Bingo.Domain.Errors;

[Serializable]
public class Invalid2DArrayConversionException : Exception
{
    public Invalid2DArrayConversionException()
    {
    }

    public Invalid2DArrayConversionException(string message) : base(message)
    {
    }
}
