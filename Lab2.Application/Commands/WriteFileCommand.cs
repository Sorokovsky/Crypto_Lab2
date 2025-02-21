using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;
using Lab2.Common.Tools;

namespace Lab2.Application.Commands;

public class WriteFileCommand : BasicCommand
{
    public override string Title { get; } = "Записати у файл";

    public override void Invoke(IExitable? exitable)
    {
        var files = new FilesService();
        var action = files.Create;
        Console.Write("Введіть назву файлу: ");
        var name = Console.ReadLine() ?? string.Empty;
        var file = $"{name}.txt";
        Console.Write("Введіть текст для запису: ");
        var text = Console.ReadLine() ?? string.Empty;
        if (files.Exists(file))
        {
            Console.Write("Файл існує пересаписати (0-ні, 1-так): ");
            var choose = int.Parse(Console.ReadLine() ?? string.Empty);
            while (choose != 0 && choose != 1)
            {
                Console.Write("Відповідь не розпізнана, спробуйте ще (0-ні, 1-так): ");
                choose = int.Parse(Console.ReadLine() ?? string.Empty);
            }

            if (choose == 0) return;
            action = files.Rewrite;
        }

        action.Invoke(file, text);
    }
}