namespace Bingo.Spreadsheet;

public sealed record SpreadsheetData(ushort Row, string Name, string Guess)
{
    public bool Equals(SpreadsheetData? other)
    {
       return Name.Equals(other?.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
