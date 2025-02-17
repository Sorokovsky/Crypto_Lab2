namespace Lab2.Visionary.Statistics;

public static class ThreeGrams
{
    public static string MostPopularThreeGram(string input)
    {
        var threeGrams = FindInText(input);
        return threeGrams
            .MaxBy(x => x.Value).Key;
    }

    public static List<int> GetDestinations(string input, string threeGram)
    {
        var result = new List<int>();
        var indexes = CollectIndexes(input, threeGram);
        var firstIndex = indexes.First();
        foreach (var index in indexes.Skip(1))
        {
            var dest = index - firstIndex;
            result.Add(dest);
        }
        return result;
    }
    
    private static List<int> CollectIndexes(string input, string threeGram)
    {
        var result = new List<int>();
        for (var i = 2; i < input.Length; i++)
        {
            var startIndex = i - 2;
            var endIndex = i;
            var currentThreeGram = $"{input[startIndex]}{input[endIndex - 1]}{input[endIndex]}";
            if(currentThreeGram == threeGram) result.Add(startIndex);
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
            if(!result.TryAdd(threeGram, 1)) result[threeGram]++;
        }
        return result;
    }
}