using Lab2.Commands.Chooser;
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
        var name = Choosing.Text("назву файлу");
        var file = $"{name}.txt";
        var text = Choosing.Text("текст для запису");
        if (files.Exists(file))
        {
            if (Choosing.Binary("Файл існує пересаписати") is false) return;
            action = files.Rewrite;
        }

        action.Invoke(file, text);
    }
}