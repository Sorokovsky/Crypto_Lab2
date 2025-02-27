using Lab2.Application.Helpers;
using Lab2.Commands.Chooser;
using Lab2.Commands.Commands;
using Lab2.Commands.Interfaces;
using Lab2.Common.Analysing;
using Lab2.Visionary;

namespace Lab2.Application.Commands;

public class TryHackCommand : BasicCommand
{
    public override string Title { get; } = "Спробувати взломати шифр";

    public override void Invoke(IExitable? exitable)
    {
        var text = Choosing.Text("текст");
        var alphabet = CommandsHelper.ChooseAlphabets();
        CypherAnalyzer.TryHack(text, new VisionaryEncryptor(alphabet), EnterKey, EnterIsNotContinue);
    }

    private static string EnterKey()
    {
        return Choosing.Text("ключ, який складається з кожного літери з рядка");
    }

    private static bool EnterIsNotContinue()
    {
        return Choosing.Binary("Задоволені ключем");
    }
}