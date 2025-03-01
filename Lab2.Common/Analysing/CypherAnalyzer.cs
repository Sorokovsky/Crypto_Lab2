using Lab2.Common.Interfaces;
using Lab2.Common.Math;
using Lab2.Common.Statistics;
using Lab2.Common.Tools;

namespace Lab2.Common.Analysing;

public static class CypherAnalyzer
{
    public static string TryHack(string text, IEncryptor encryptor, Func<string> keyGetter,
        Func<bool> notContinueGetter)
    {
        var keyLength = GetKeyLength(text);
        if (keyLength == 0) throw new Exception("Довжина ключа 0");
        var concurrencyIndex = LettersStatistics.ConcurrencyIndex(text);
        Console.WriteLine($"Індекс збігу: {concurrencyIndex}");
        Console.WriteLine($"Довжина ключа: {keyLength}.");
        Console.WriteLine($"Довжина ключа за Фрідменом: {FreedmanKeyLength(text, encryptor.Alphabet)}.");
        var split = SplitByColumns(text, keyLength);
        Console.WriteLine("По рядкам.");
        Console.WriteLine(split);
        var stats = GetStatistics(split, keyLength);
        var finished = false;
        var output = string.Empty;
        while (finished is false)
        {
            Console.WriteLine("Статистика: ");
            Console.WriteLine(stats);
            var key = keyGetter();
            output = encryptor.Decrypt(text, key);
            Console.WriteLine($"Розшифрований текст: \"{output}\"");
            finished = notContinueGetter();
        }

        return output;
    }

    private static int GetKeyLength(string text)
    {
        text = PrepareText(text);
        var popularThreeGram = ThreeGrams.GetMostPopularThreeGram(text);
        Console.WriteLine($"Найпопулярніша триграма: {popularThreeGram}.");
        var lengthBetweenFirstLetters = ThreeGrams.GetDestinations(text, popularThreeGram);
        if (lengthBetweenFirstLetters.Count == 0) return 0;
        return Number.Gcd(lengthBetweenFirstLetters);
    }

    private static double FreedmanKeyLength(string text, (string Letters, double Frequencies) alphabet)
    {
        var reversedCount = 1 / (double)alphabet.Letters.Length;
        var doubleFrequencies = alphabet.Frequencies;
        Console.WriteLine($"Середня частота появи літер: {doubleFrequencies}.");
        var concurrencyIndex = LettersStatistics.ConcurrencyIndex(text);
        var upper = doubleFrequencies - reversedCount;
        var downerLeft = concurrencyIndex - reversedCount;
        var downerRightTop = doubleFrequencies - concurrencyIndex;
        var downerRight = downerRightTop / text.Length;
        var downer = downerLeft + downerRight;
        return System.Math.Round(upper / downer);
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
        return text
            .Where(char.IsLetter)
            .Aggregate(string.Empty, (current, symbol) => current + char.ToUpper(symbol));
    }

    private static Dictionary<char, int> CollectCounting(List<char> letters)
    {
        var result = new Dictionary<char, int>();
        foreach (var letter in letters.Where(letter => !result.TryAdd(letter, 1)))
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
}