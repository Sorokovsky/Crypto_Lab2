using Lab2.Common.Interfaces;
using Lab2.Common.Math;
using Lab2.Common.Statistics;
using Lab2.Common.Tools;

namespace Lab2.Common.Analysing;

public static class CypherAnalyzer
{
    private static int GetKeyLength(string text)
    {
        text = PrepareText(text);
        var popularThreeGram = ThreeGrams.GetMostPopularThreeGram(text);
        Console.WriteLine($"Найпопулярніша триграма: {popularThreeGram}.");
        var lengthBetweenFirstLetters = ThreeGrams.GetDestinations(text, popularThreeGram);
        return Number.Gcd(lengthBetweenFirstLetters);
    }

    private static TextTable SplitByColumns(string text, int keyLength)
    {
        text = PrepareText(text);
        List<List<char>> result = [];
        var step = 0;
        foreach (var letter in text)
        {
            if (step == keyLength) step = 0;

            var list = result.ElementAtOrDefault(step);
            if (list is null)
            {
                list = [];
                result.Add(list);
            }

            list.Add(letter);
            step++;
        }

        return new TextTable(result);
    }

    private static string PrepareText(string text)
    {
        var result = string.Empty;
        foreach (var symbol in text)
            if (char.IsLetter(symbol))
                result += char.ToUpper(symbol);

        return result;
    }

    private static Dictionary<char, int> CollectCounting(List<char> letters)
    {
        var result = new Dictionary<char, int>();
        foreach (var letter in letters)
            if (!result.TryAdd(letter, 1))
                result[letter]++;

        return result;
    }

    private static StatisticsBox GetStatistics(TextTable table, int keyLength)
    {
        var result = new Dictionary<int, Dictionary<char, int>>();
        for (var i = 0; i < keyLength; i++)
        {
            var row = table.GetRow(i);
            result.Add(i, CollectCounting(row)
                .OrderByDescending(x => x.Value)
                .Take(keyLength)
                .ToDictionary(x => x.Key, x => x.Value));
        }

        return new StatisticsBox(result);
    }

    public static string TryHack(string text, IEncryptor encryptor, Func<IEncryptor, string, string> choosingKey)
    {
        var keyLength = GetKeyLength(text);
        Console.WriteLine($"Довжина ключа: {keyLength}.");
        var split = SplitByColumns(text, keyLength);
        Console.WriteLine("По рядкам.");
        Console.WriteLine(split);
        var stats = GetStatistics(split, keyLength);
        Console.WriteLine("Статистика: ");
        Console.WriteLine(stats);
        return choosingKey(encryptor, text);
    }
}