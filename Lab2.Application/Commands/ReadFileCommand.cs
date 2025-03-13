using Lab2.Commands.Chooser;
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
        var file = $"{Choosing.Text("назву файлу")}";
        var text = files.Read($"{file}.txt");
        Console.WriteLine($"Зміст файлу {file}: ");
        Console.WriteLine(text);
    }
}