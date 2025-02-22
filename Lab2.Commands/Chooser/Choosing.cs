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

        Console.WriteLine($"Виберіть зі \"{listName}\": ");
        list.ForEach(x => Console.WriteLine($"{nameGetter(x)}"));
        Console.Write(">> ");
        var result = resultGetter(Console.ReadLine() ?? string.Empty);
        var item = list.FirstOrDefault(x => listGetter(x, result));
        while (item is null)
        {
            if (retry is false) throw new Exception("Відповідь не розпізнано.");
            Console.WriteLine("Відповідь не розпізнано, спробуйте ще.");
            Console.WriteLine($"Виберіть {listName}: ");
            list.ForEach(x => Console.WriteLine($"{nameGetter(x)}"));
            Console.Write(">> ");
            result = resultGetter(Console.ReadLine() ?? string.Empty);
            item = list.FirstOrDefault(x => listGetter(x, result));
        }

        return item;
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
        var choose = int.Parse(Console.ReadLine() ?? string.Empty);
        while (choose != 0 && choose != 1)
        {
            Console.Write($"Відповідь не розпізнано, спробуйте ще {variants}: ");
            choose = int.Parse(Console.ReadLine() ?? string.Empty);
        }

        return choose == 1;
    }
}