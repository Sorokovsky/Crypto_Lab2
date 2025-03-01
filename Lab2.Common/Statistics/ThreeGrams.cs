using System.Collections.Concurrent;

namespace Lab2.Common.Statistics;

public static class ThreeGrams
{
    private static readonly ConcurrentDictionary<string, Dictionary<string, List<int>>> ThreeGramsByText = new();

    public static string GetMostPopularThreeGram(string input)
    {
        var result = FindInText(input);
        return result.First().Key;
    }

    public static List<int> GetDestinations(string input, string threeGram)
    {
        var indexes = CollectIndexes(input, threeGram);
        var firstIndex = indexes.First();
        return indexes.Skip(1).Select(index => index - firstIndex).ToList();
    }

    private static List<int> CollectIndexes(string input, string threeGram)
    {
        var indexes = Collect(input);
        return indexes.First(x => x.Key == threeGram).Value;
    }

    private static Dictionary<string, int> FindInText(string input)
    {
        return Collect(input)
            .ToDictionary(x => x.Key, x => x.Value.Count);
    }

    private static Dictionary<string, List<int>> Collect(string input)
    {
        var result = ThreeGramsByText.GetValueOrDefault(input.ToUpper());
        if (result != null) return result;
        result = new Dictionary<string, List<int>>();
        for (var i = 2; i < input.Length; i++)
        {
            var startIndex = i - 2;
            var centerIndex = i - 1;
            var endIndex = i;
            var threeGram = $"{input[startIndex]}{input[centerIndex]}{input[endIndex]}";
            if (result.TryAdd(threeGram, [startIndex])) continue;
            result.GetValueOrDefault(threeGram)?.Add(startIndex);
        }

        result = result
            .OrderByDescending(x => x.Value.Count)
            .ToDictionary(x => x.Key, x => x.Value);
        ThreeGramsByText.TryAdd(input.ToUpper(), result);
        return result;
    }
}