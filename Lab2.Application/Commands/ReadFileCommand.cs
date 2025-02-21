using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;
using Lab2.Common.Tools;

namespace Lab2.Application.Commands;

public class ReadFileCommand : BasicCommand
{
    public override string Title { get; } = "Прочитати файл";

    public override void Invoke(IExitable? exitable)
    {
        var files = new FilesService();
        Console.Write("Введіть назву файлу: ");
        var file = $"{Console.ReadLine() ?? string.Empty}.txt";
        var text = files.Read(file);
        Console.WriteLine($"Зміст файлу {file}: ");
        Console.WriteLine(text);
    }
}