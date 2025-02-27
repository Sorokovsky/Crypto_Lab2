namespace Lab2.Visionary;

public class VisionaryTable
{
    private readonly string _alphabet;
    private readonly List<List<char>> _table;

    private VisionaryTable(List<List<char>> table, string alphabet)
    {
        _table = table;
        _alphabet = alphabet;
    }

    public char GetByKey(VisionaryKey key)
    {
        var rowIndex = _alphabet.IndexOf(char.ToUpper(key.Row));
        var columnIndex = _alphabet.IndexOf(char.ToUpper(key.Column));
        var row = _table.ElementAt(rowIndex);
        return row.ElementAt(columnIndex);
    }

    public static VisionaryTable Generate(string alphabet)
    {
        var result = new List<List<char>>();
        for (var i = 0; i < alphabet.Length; i++)
        {
            var row = new List<char>();
            for (var j = 0; j < alphabet.Length; j++)
            {
                var letter = alphabet[(i + j + alphabet.Length) % alphabet.Length];
                row.Add(letter);
            }

            result.Add(row);
        }

        return new VisionaryTable(result, alphabet);
    }

    public override string ToString()
    {
        var result = string.Empty;
        foreach (var rows in _table)
        {
            result = rows.Aggregate(result, (current, cell) => current + cell);

            result += "\n";
        }

        return result;
    }
}