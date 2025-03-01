using System.Text;
using Lab2.Commands.Chooser;
using Lab2.Common.Tools;

namespace Lab2.Application.Helpers;

public static class TextHelper
{
    public delegate string InputDelegate();

    public delegate void OutputDelegate(string text);

    public static InputDelegate ChooseInput(string name)
    {
        InputDelegate action;
        var isFromFile = Choosing.Binary($"Читати З файлу {name}?");
        if (isFromFile)
        {
            var filesService = new FilesService();
            var fileName = Choosing.Text("назву файлу");
            action = () => filesService.Read($"{fileName}.txt");
        }
        else
        {
            action = () => Choosing.Text(name);
        }

        return action;
    }

    public static OutputDelegate ChooseOutput(string name)
    {
        OutputDelegate action;
        var isFromFile = Choosing.Binary($"Писати у файл {name}?");
        if (isFromFile)
        {
            var filesService = new FilesService();
            var fileName = Choosing.Text("назву файлу");
            action = text => filesService.Create($"{fileName}.txt", text);
        }
        else
        {
            var builder = new StringBuilder(name);
            builder[0] = char.ToUpper(builder[0]);
            name = builder.ToString();
            action = x => Console.WriteLine($"{name}: {x}");
        }

        return action;
    }
}