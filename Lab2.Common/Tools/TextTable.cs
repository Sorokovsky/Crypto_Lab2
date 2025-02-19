namespace Lab2.Common.Tools;

public class TextTable
{
    private readonly List<List<char>> _table;

    public TextTable(List<List<char>> table)
    {
        _table = table;
    }

    public char GetBy(int row, int column)
    {
        var columns = _table.ElementAt(row);
        return columns.ElementAt(column);
    }

    public override string ToString()
    {
        var result = string.Empty;
        foreach (var cols in _table)
        {
            foreach (var cell in cols) Console.Write(cell);

            Console.WriteLine();
        }

        return result;
    }

    public List<char> GetRow(int row)
    {
        return _table.ElementAtOrDefault(row) ?? [];
    }

    public List<char> GetCol(int col)
    {
        var result = new List<char>();
        foreach (var row in _table)
        {
            var item = row.ElementAtOrDefault(col);
            result.Add(item);
        }

        return result;
    }
}