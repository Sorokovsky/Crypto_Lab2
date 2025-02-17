namespace Lab2.Common.Statistics;

public static class ThreeGrams
{
    public static string GetMostPopularThreeGram(string input)
    {
        var threeGrams = FindInText(input);
        return threeGrams
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
        var result = new List<int>();
        for (var i = 2; i < input.Length; i++)
        {
            var startIndex = i - 2;
            var endIndex = i;
            var currentThreeGram = $"{input[startIndex]}{input[endIndex - 1]}{input[endIndex]}";
            if (currentThreeGram == threeGram) result.Add(startIndex);
        }

        return result;
    }

    private static Dictionary<string, int> FindInText(string input)
    {
        var result = new Dictionary<string, int>();
        for (var i = 2; i < input.Length; i++)
        {
            var startIndex = i - 2;
            var threeGram = $"{input[startIndex]}{input[i - 1]}{input[i]}";
            if (!result.TryAdd(threeGram, 1)) result[threeGram]++;
        }

        return result;
    }
}