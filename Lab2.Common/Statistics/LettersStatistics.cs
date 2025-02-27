namespace Lab2.Common.Statistics;

public static class LettersStatistics
{
    public static double ConcurrencyIndex(string text)
    {
        var letters = text
            .Where(char.IsLetter)
            .Select(char.ToUpper)
            .ToArray();
        var n = letters.Length;
        if (n < 2) return 0;
        return CollectFrequencies(text).Values.Sum(f => f * (f - 1)) / (n * (n - 1));
    }

    public static IReadOnlyDictionary<char, double> CollectFrequencies(string text)
    {
        return CollectCount(text)
            .ToDictionary(x => x.Key, x => x.Value / (double)text.Length);
    }

    private static IReadOnlyDictionary<char, int> CollectCount(string text)
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