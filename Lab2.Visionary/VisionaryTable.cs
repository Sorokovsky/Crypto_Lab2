using Lab2.Common.Tools;

namespace Lab2.Visionary;

public class VisionaryTable
{
    private readonly List<List<char>> _table;

    private VisionaryTable(List<List<char>> table)
    {
        _table = table;
    }

    public char GetByKey(VisionaryKey key)
    {
        var row = _table.ElementAt(key.Row);
        return row.ElementAt(key.Column);
    }

    public static VisionaryTable Generate(string alphabet = Alphabets.En)
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

        return new VisionaryTable(result);
    }

    public override string ToString()
    {
        var result = string.Empty;
        foreach (var rows in _table)
        {
            foreach (var cell in rows) result += cell;

            result += "\n";
        }

        return result;
    }
}