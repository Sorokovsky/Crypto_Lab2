namespace Lab2.Common.Statistics;

public class StatisticsBox
{
    private readonly Dictionary<int, Dictionary<char, int>> _stats;

    public StatisticsBox(Dictionary<int, Dictionary<char, int>> stats)
    {
        _stats = stats;
    }

    public IDictionary<char, int> GetBy(int row)
    {
        return _stats.GetValueOrDefault(row) ?? [];
    }

    public override string ToString()
    {
        var result = string.Empty;
        foreach (var row in _stats)
        {
            Console.Write($"Рядок {row.Key + 1}:");
            foreach (var stat in row.Value) Console.Write($" {stat.Key}({stat.Value})");

            Console.WriteLine();
        }

        return result;
    }
}