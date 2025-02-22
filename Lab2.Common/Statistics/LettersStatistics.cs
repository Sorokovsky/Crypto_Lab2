namespace Lab2.Common.Statistics;

public static class LettersStatistics
{
    public static double ConcurrencyIndex(string text)
    {
        var stats = CollectCount(text);
        double upper = stats.Select(x => x.Value * (x.Value - 1)).Sum();
        double downer = text.Length * (text.Length - 1);
        return upper / downer;
    }

    public static IReadOnlyDictionary<char, double> CollectFrequencies(string text)
    {
        var stats = CollectCount(text);
        return stats
            .ToDictionary(x => x.Key, x => (double)x.Value / text.Length);
    }

    private static IReadOnlyDictionary<char, int> CollectCount(string text)
    {
        Dictionary<char, int> result = new();
        foreach (var symbol in text
                     .Where(char.IsLetter)
                     .Where(symbol => result.TryAdd(char.ToUpper(symbol), 1) is false)
                )
            result[char.ToUpper(symbol)]++;

        return result;
    }
}