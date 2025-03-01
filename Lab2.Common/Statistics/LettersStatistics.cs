namespace Lab2.Common.Statistics;

public static class LettersStatistics
{
    public static double ConcurrencyIndex(IEnumerable<char> text)
    {
        var letters = text
            .Where(char.IsLetter)
            .Select(char.ToUpper)
            .ToArray();
        var n = letters!
            .Length;
        if (n < 2) return 0;
        var counters = CollectCount(letters)
            .OrderByDescending(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);
        var frequencies = CollectCount(letters)
            .OrderByDescending(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);
        return frequencies.Values.Sum(f => f * (f - 1)) / (double)(n * (n - 1));
    }

    public static Dictionary<char, double> CollectFrequencies(IEnumerable<char> text)
    {
        var letters = text.Where(char.IsLetter).ToArray();
        return CollectCount(letters)
            .ToDictionary(x => x.Key, x => x.Value / (double)letters.Length);
    }

    public static IReadOnlyDictionary<char, int> CollectCount(IEnumerable<char> text)
    {
        var letters = text
            .Where(char.IsLetter)
            .Select(char.ToUpper)
            .ToArray();
        return letters
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
    }
}