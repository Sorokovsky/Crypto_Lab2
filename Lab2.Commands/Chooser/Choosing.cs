namespace Lab2.Commands.Chooser;

public static class Choosing
{
    public static T FromList<T, TK>(
        List<T> list,
        Func<T, string> nameGetter,
        Func<string, TK> resultGetter,
        Func<T, TK, bool> listGetter,
        string listName,
        bool retry = true
    )
    {
        switch (list.Count)
        {
            case 0:
                throw new ArgumentException("Список для обрання порожній.");
            case 1:
                return list.First();
        }

        var isError = false;

        Console.WriteLine($"Виберіть зі \"{listName}\": ");
        list.ForEach(x => Console.WriteLine($"{nameGetter(x)}"));
        Console.Write(">> ");
        var result = ResultWrapper();
        var item = list.FirstOrDefault(x => listGetter(x, result));
        while (IsNull(item) || isError)
        {
            isError = false;
            if (retry is false) throw new Exception("Відповідь не розпізнано.");
            Console.WriteLine("Відповідь не розпізнано, спробуйте ще.");
            Console.WriteLine($"Виберіть {listName}: ");
            list.ForEach(x => Console.WriteLine($"{nameGetter(x)}"));
            Console.Write(">> ");
            result = ResultWrapper();
            item = list.FirstOrDefault(x => listGetter(x, result));
        }

        return item!;

        TK ResultWrapper()
        {
            var resultWrapper = default(TK)!;
            try
            {
                resultWrapper = resultGetter(Console.ReadLine() ?? string.Empty);
                return resultWrapper;
            }
            catch (Exception)
            {
                isError = true;
                return resultWrapper;
            }
        }
    }

    public static string Text(string name)
    {
        Console.Write($"Введіть {name}: ");
        return Console.ReadLine() ?? string.Empty;
    }

    public static bool Binary(string question)
    {
        const string variants = "(0-ні, 1-так)";
        Console.Write($"{question} {variants}: ");
        var isError = TryReadAndParseForNumber(out var choose);
        while (isError || (choose != 0 && choose != 1))
        {
            Console.Write($"Відповідь не розпізнано, спробуйте ще {variants}: ");
            isError = TryReadAndParseForNumber(out choose);
        }

        return choose == 1;
    }

    public static int Number(string name, int? min, int? max)
    {
        Console.Write($"Введіть {name}: ");
        var isError = TryReadAndParseForNumber(out var value);
        while (isError)
        {
            Console.Write($"Ви ввели {value}(не число), спробуйте ще: ");
            isError = TryReadAndParseForNumber(out value);
            if (min is not null)
                while (value < min)
                {
                    Console.Write($"Число має бути більше за {min}, спробуйте ще: ");
                    isError = TryReadAndParseForNumber(out value);
                }

            if (max is not null)
                while (value > max)
                {
                    Console.Write($"Число має бути меньше за {max}, спробуйте ще: ");
                    isError = TryReadAndParseForNumber(out value);
                }
        }

        return value;
    }

    private static bool IsNull(object? item)
    {
        try
        {
            if (item == null) return true;
            var type = item.GetType();
            var propValue = type.GetProperty("Value");
            var propKey = type.GetProperty("Key");
            if (propValue is not null && propKey is not null)
                return propKey.GetValue(item) == null || propValue.GetValue(item) == null;

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool TryReadAndParseForNumber(out int value)
    {
        var text = Console.ReadLine() ?? string.Empty;
        return int.TryParse(text, out value) || text.Any(x => x is < '0' or > '9');
    }
}