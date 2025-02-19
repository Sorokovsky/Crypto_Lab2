namespace Lab2.Common.Statistics;

public static class ThreeGrams
{
    private static readonly Dictionary<string, Dictionary<string, List<int>>> ThreeGramsByText = new();

    public static string GetMostPopularThreeGram(string input)
    {
        return FindInText(input)
            .MaxBy(x => x.Value).Key;
    }

    public static List<int> GetDestinations(string input, string threeGram)
    {
        var indexes = CollectIndexes(input, threeGram);
        var firstIndex = indexes.First();
        return indexes.Skip(1).Select(index => index - firstIndex).ToList();
    }

    private static List<int> CollectIndexes(string input, string threeGram)
    {
        var indexes = Collect(input)
            .First(x => x.Key == threeGram);
        return indexes.Value;
    }

    private static Dictionary<string, int> FindInText(string input)
    {
        return Collect(input)
            .ToDictionary(x => x.Key, x => x.Value.Count);
    }

    private static Dictionary<string, List<int>> Collect(string input)
    {
        var result = ThreeGramsByText.GetValueOrDefault(input);
        if (result != null) return result;
        result = new Dictionary<string, List<int>>();
        ThreeGramsByText.TryAdd(input, result);
        for (var i = 2; i < input.Length; i++)
        {
            var startIndex = i - 2;
            var centerIndex = i - 1;
            var endIndex = i;
            var threeGram = $"{input[startIndex]}{input[centerIndex]}{input[endIndex]}";
            if (result.TryAdd(threeGram, [startIndex])) continue;
            result.GetValueOrDefault(threeGram)?.Add(startIndex);
        }

        return result;
    }
}