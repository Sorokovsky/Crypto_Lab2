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
        Console.WriteLine($"Виберіть {listName}: ");
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
}